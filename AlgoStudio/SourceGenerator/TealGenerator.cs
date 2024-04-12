using AlgoStudio.Compiler;
using AlgoStudio.Compiler.Exceptions;
using AlgoStudio.Optimisers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AlgoStudio.SourceGenerator
{
    [Generator]
    public class TealGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
       //         Debugger.Launch();
            }
#endif 
            context.RegisterForSyntaxNotifications(() => new CodeFilesReceiver());
        }

        private const string OptimiserLocationSetting = "build_property.optimiserslocation";
        private string optimisersLocation = String.Empty;

        private List<IOptimiser> LoadOptimisers(GeneratorExecutionContext context)
        {
            List<IOptimiser> optimisers = new List<IOptimiser>();

            var globalOptions = context.AnalyzerConfigOptions?.GlobalOptions;
            if (globalOptions != null)
            {
                globalOptions.TryGetValue(OptimiserLocationSetting, out optimisersLocation);
            }

            try
            {
                var assemblies = Directory.GetFiles(optimisersLocation).Where(file => file.EndsWith(".dll")).ToList();
                foreach (var assembly in assemblies)
                {

                    try
                    {
                        var a = Assembly.LoadFrom(assembly);

                        optimisers.AddRange(a
                            .GetExportedTypes()
                            .Where(t => typeof(IOptimiser).IsAssignableFrom(t))
                            .Select(t => (IOptimiser)Activator.CreateInstance(t)));


                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Optimiser assembly could not be loaded: {assembly}");
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to load optimisers: {ex.Message}");
            }

            return optimisers;
        }

        public void Execute(GeneratorExecutionContext context)
        {

            var optimisers = LoadOptimisers(context);

            CodeFilesReceiver receiver = (CodeFilesReceiver)context.SyntaxReceiver;

            var validLibraryUnits = receiver
               .ClassesToProcess
               .Select(unit => (root: unit.SyntaxTree.GetRoot(), classSyntax: unit))
               .Where(u => u.root != null)
               .Select(u => (semanticModel: context.Compilation.GetSemanticModel(u.root.SyntaxTree), classSyntax: u.classSyntax, root: u.root))
               .Where(u => u.semanticModel != null)
               .Select(u => (classSymbol: u.semanticModel.GetDeclaredSymbol(u.classSyntax) as INamedTypeSymbol, root: u.root))
               .Where(symb => symb.classSymbol != null && Utilities.IsSmartContractLibrary(symb.classSymbol))
               .Select(symb => symb.root)
               .Distinct();

            var validClassUnits = receiver
                .ClassesToProcess
                .Select(unit => (root: unit.SyntaxTree.GetRoot(), classSyntax: unit))
                .Where(u => u.root != null)
                .Select(u => (semanticModel: context.Compilation.GetSemanticModel(u.root.SyntaxTree), classSyntax: u.classSyntax, root: u.root))
                .Where(u => u.semanticModel != null)
                .Select(u => (classSymbol: u.semanticModel.GetDeclaredSymbol(u.classSyntax) as INamedTypeSymbol, root: u.root))
                .Where(symb => symb.classSymbol != null && (Utilities.IsSmartContract(symb.classSymbol) || Utilities.IsSmartSignature(symb.classSymbol)))
                .Select(symb => symb.root)
                .Distinct();

            var validStructUnits = receiver
                .StructsToProcess
                .Select(unit => (root: unit.SyntaxTree.GetRoot(), structSyntax: unit))
                .Where(u => u.root != null)
                .Select(u => (semanticModel: context.Compilation.GetSemanticModel(u.root.SyntaxTree), classSyntax: u.structSyntax, root: u.root))
                .Where(u => u.semanticModel != null)
                .Select(u => (structSymbol: u.semanticModel.GetDeclaredSymbol(u.classSyntax) as INamedTypeSymbol, root: u.root))
                .Where(symb => symb.structSymbol != null && Utilities.IsAbiStruct(symb.structSymbol))
                .Select(symb => symb.root)
                .Distinct();


            var validCompilationUnits = validStructUnits.Concat(validLibraryUnits).Concat(validClassUnits); //order is important
            var compilationGroup = new CompilationGroup();
            Dictionary<string, string> hints = new Dictionary<string, string>();

           
            //Compile all code files, building up the CompilationGroup's information on dependent libraries:
            List<(TealSharpSyntaxWalker walker, string filePath)> compilations = new List<(TealSharpSyntaxWalker,string)>();
            foreach (var compilationUnit in validCompilationUnits)
            {

                var semanticModel = context.Compilation.GetSemanticModel(compilationUnit.SyntaxTree);
                var walker = new TealSharpSyntaxWalker(compilationGroup, (diagnostic) => context.ReportDiagnostic(diagnostic), semanticModel, compilationUnit.GetLocation());
                walker.Visit(compilationUnit);
                var scope = Path.GetFileNameWithoutExtension(compilationUnit.SyntaxTree.FilePath);
                compilations.Add((walker,scope));
            }

            //now optimise library dependencies:
            foreach (var libraryMethod in compilationGroup.LibraryMethods)
            {
                try
                {
                    libraryMethod.OptimiseCode(optimisers);
                }
                catch
                {
                    throw new ErrorDiagnosticException("E045");
                }
            }
            
            //generate output sources
            foreach (var compilation in compilations)
            { 
                try
                {
                    foreach (var contractDeclaration in compilation.walker.GetSmartContracts(optimisers))
                    {
                        var scope = compilation.filePath;
                        string hint = scope + "." + contractDeclaration.Name;

                        if (hints.TryGetValue(hint, out string suffix))
                        {
                            hints[hint] = suffix + "_";
                            hint = hint + suffix;
                        }
                        else
                        {
                            hints[hint] = "_";
                        }

                        context.AddSource(hint, contractDeclaration.ToCSharp(scope));

                    }

                    foreach (var sigDeclaration in compilation.walker.GetSmartSignatures(optimisers))
                    {
                        var scope = compilation.filePath;
                        string hint = $"Signatures.{scope}.{sigDeclaration.Name}";

                        if (hints.TryGetValue(hint, out string suffix))
                        {
                            hints[hint] = suffix + "_";
                            hint = hint + suffix;
                        }
                        else
                        {
                            hints[hint] = "_";
                        }

                        context.AddSource(hint, sigDeclaration.ToCSharp(scope));

                    }

                }
                catch
                {

                }
                
            }






            


        }


    }

    internal class CodeFilesReceiver : ISyntaxReceiver
    {
        internal IImmutableList<ClassDeclarationSyntax> ClassesToProcess = ImmutableList.Create<ClassDeclarationSyntax>();
        internal IImmutableList<StructDeclarationSyntax> StructsToProcess = ImmutableList.Create<StructDeclarationSyntax>();


        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax cld)
                ClassesToProcess = ClassesToProcess.Add(cld);

            if (syntaxNode is StructDeclarationSyntax str)
                StructsToProcess = StructsToProcess.Add(str);
        }


    }
}
