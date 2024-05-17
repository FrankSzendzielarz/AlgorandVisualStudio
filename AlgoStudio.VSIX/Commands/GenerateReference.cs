using AlgoStudio.ABI.ARC32;
using AlgoStudio.Clients;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.Win32;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Task = System.Threading.Tasks.Task;

namespace AlgoStudio.VSIX
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class GenerateReference
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0101;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("ADAFB608-2F6A-4DD6-89A7-B2BD6576ACC6");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractJson"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private GenerateReference(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            menuItem.BeforeQueryStatus += new EventHandler(MenuItem_BeforeQueryStatus);
            commandService.AddCommand(menuItem);
        }

        private async void MenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            var myCommand = sender as OleMenuCommand;

            Microsoft.VisualStudio.Text.Editor.IWpfTextView textView = await GetTextViewAsync();
            if (textView != null)
            {
                Microsoft.VisualStudio.Text.SnapshotPoint caretPosition = textView.Caret.Position.BufferPosition;
                Microsoft.CodeAnalysis.Document document = caretPosition.Snapshot.GetOpenDocumentInCurrentContextWithChanges();
                if (document != null)
                {
                    var semanticModel = await document.GetSemanticModelAsync();
                    if (semanticModel != null)
                    {
                        try
                        {
                            Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax classDeclaration =
                                document.GetSyntaxRootAsync().Result.FindToken(caretPosition).Parent.AncestorsAndSelf().
                                    OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>().FirstOrDefault();

                            if (classDeclaration != null)
                            {
                                var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration);
                                myCommand.Visible = AlgoStudio.Compiler.Utilities.IsSmartContract(classSymbol as INamedTypeSymbol);
                            }
                            else
                                myCommand.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            //swallow
                        }
                    }
                }


            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static GenerateReference Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in ExportContractJson's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new GenerateReference(package, commandService);
        }

        private async Task<Microsoft.VisualStudio.Editor.IVsEditorAdaptersFactoryService> GetEditorAdaptersFactoryServiceAsync()
        {
            Microsoft.VisualStudio.ComponentModelHost.IComponentModel componentModel =
                (Microsoft.VisualStudio.ComponentModelHost.IComponentModel)(await ServiceProvider.GetServiceAsync(
                    typeof(Microsoft.VisualStudio.ComponentModelHost.SComponentModel)));
            return componentModel.GetService<Microsoft.VisualStudio.Editor.IVsEditorAdaptersFactoryService>();
        }


        private async Task<Microsoft.VisualStudio.Text.Editor.IWpfTextView> GetTextViewAsync()
        {
            Microsoft.VisualStudio.TextManager.Interop.IVsTextManager textManager =
                (Microsoft.VisualStudio.TextManager.Interop.IVsTextManager)(await ServiceProvider.GetServiceAsync(
                    typeof(Microsoft.VisualStudio.TextManager.Interop.SVsTextManager)));
            Microsoft.VisualStudio.TextManager.Interop.IVsTextView textView;
            textManager.GetActiveView(1, null, out textView);
            if (textView is null) return null;
            return (await GetEditorAdaptersFactoryServiceAsync()).GetWpfTextView(textView);
        }


        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);


         

            //return;

          
            Microsoft.VisualStudio.Text.Editor.IWpfTextView textView = await GetTextViewAsync();
            if (textView != null)
            {
                Microsoft.VisualStudio.Text.SnapshotPoint caretPosition = textView.Caret.Position.BufferPosition;
                Microsoft.CodeAnalysis.Document document = caretPosition.Snapshot.GetOpenDocumentInCurrentContextWithChanges();

                var semanticModel = await document.GetSemanticModelAsync();

                Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax classDeclaration =
                    document.GetSyntaxRootAsync().Result.FindToken(caretPosition).Parent.AncestorsAndSelf().
                        OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>().FirstOrDefault();
                if (classDeclaration != null)
                {
                    try
                    {
                        
                        var appModel = AppDescription.GenerateContractDescription(semanticModel, classDeclaration);
                        string refCode="";
                        if (appModel != null)
                        {
                            refCode = appModel.ToSmartContractReference("", "");
                        }

                        if (!String.IsNullOrWhiteSpace(refCode))
                        {
                            SaveFileDialog sd = new SaveFileDialog();
                            sd.Title = "Save SmartContractReference";
                            sd.Filter = "C# file (.cs)|*.cs";
                            sd.DefaultExt = ".cs";
                            sd.FileName = "smartreference.cs";
                            if (sd.ShowDialog() ?? false)
                            {
                                File.WriteAllText(sd.FileName,refCode);
                            }
                        }
                        else
                        {
                            VsShellUtilities.ShowMessageBox(
                             this.package,
                             $"The source file was not recognised as a smart contract.",
                             "Error",
                             OLEMSGICON.OLEMSGICON_CRITICAL,
                             OLEMSGBUTTON.OLEMSGBUTTON_OK,
                             OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                        }
                    }
                    catch (Exception ex)
                    {
                        VsShellUtilities.ShowMessageBox(
                               this.package,
                               $"An error occurred trying to parse the source contract {ex.Message}.",
                               "Error",
                               OLEMSGICON.OLEMSGICON_CRITICAL,
                               OLEMSGBUTTON.OLEMSGBUTTON_OK,
                               OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                    }

                }
            }


        }
    }
}
