using AlgoStudio.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace AlgoStudio
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AlgoStudioAnalyzer : DiagnosticAnalyzer
    {
        public static ImmutableArray<string> SupportedIds => DiagnosticDescriptors.SupportedDiagnostics.Select(r => r.Id).ToImmutableArray();
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return DiagnosticDescriptors.SupportedDiagnostics; } }

        public override void Initialize(AnalysisContext context)
        {

            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            //context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);

            context.RegisterSemanticModelAction(AnalyzeSemanticModel);
        }


        private static void AnalyzeSemanticModel(SemanticModelAnalysisContext context)
        {
            var root = context.SemanticModel.SyntaxTree.GetRoot();

            var existingDiagnostics = context.SemanticModel.GetDiagnostics();

            if (existingDiagnostics.Any(d => d.Severity == DiagnosticSeverity.Error))
            {
                var diagnostic = DiagnosticDescriptors.Create("E002", existingDiagnostics.First().Location);// root.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
            else
            {
                var walker = new TealSharpSyntaxWalker(new CompilationGroup(),(diagnostic) => context.ReportDiagnostic(diagnostic), context.SemanticModel, context.SemanticModel.SyntaxTree.GetRoot().GetLocation());
         
            }



        }


    }


  
}
