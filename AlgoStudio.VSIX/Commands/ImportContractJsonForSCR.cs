using AlgoStudio.ABI.ARC32;
using AlgoStudio.VSIX.Controls;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace AlgoStudio.VSIX
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ImportContractJSONForSCR
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0102;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("96e9f106-1660-4324-9dd4-272334d83080");

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
        private ImportContractJSONForSCR(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ImportContractJSONForSCR Instance
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
            Instance = new ImportContractJSONForSCR(package, commandService);
        }


        private async Task<(string, string)> GetSolutionExplorerSelectedItemFilePath()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var _applicationObject = await ServiceProvider.GetServiceAsync(typeof(SDTE)) as EnvDTE80.DTE2;
            UIHierarchy uih = _applicationObject.ToolWindows.SolutionExplorer;
            Array selectedItems = (Array)uih.SelectedItems;
            string filePath = String.Empty;
            string nameSpace = String.Empty;
            if (null != selectedItems)
            {
                foreach (UIHierarchyItem selItem in selectedItems)
                {
                    ProjectItem prjItem = selItem.Object as ProjectItem;
                    if (prjItem != null)
                    {
                        filePath = prjItem.Properties.Item("FullPath").Value.ToString();
                        var prj = prjItem.ContainingProject;
                        if (prj != null)
                        {
                            nameSpace = prj.Properties.Item("DefaultNamespace").Value.ToString() + "." + prjItem.Name;
                        }

                    }
                    else
                    {
                        EnvDTE.Project prj = selItem.Object as EnvDTE.Project;
                        if (prj != null)
                        {
                            filePath = prj.Properties.Item("FullPath").Value.ToString();
                            nameSpace = prj.Properties.Item("DefaultNamespace").Value.ToString();
                        }

                    }
                }
            }

            return (filePath, nameSpace);
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

            (string filePath, string nameSpace) = await GetSolutionExplorerSelectedItemFilePath();


            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Open Algorand app.json for Reference generation";
            fd.Filter = "Json documents (.json)|*.json";
            fd.DefaultExt = ".json";

            AppDescription cd = null;
            if (fd.ShowDialog() ?? false)
            {
                cd = AppDescription.LoadFromFile(fd.FileName);
                if (cd == null)
                {
                    VsShellUtilities.ShowMessageBox(
                    this.package,
                    "Invalid app.json or file not found.",
                    "Error",
                    OLEMSGICON.OLEMSGICON_CRITICAL,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                }


                var previewDialog = new ContractJsonToSCR(cd, nameSpace);
                if (previewDialog.ShowDialog() ?? false)
                {
                    if (!String.IsNullOrWhiteSpace(filePath))
                    {
                        filePath = Path.GetDirectoryName(filePath);
                    }
                    
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.Title = "Save SmartContractReference";
                    sd.Filter = "C# file (.cs)|*.cs";
                    sd.DefaultExt = ".cs";
                    sd.FileName = cd.Name + ".cs";
                    sd.InitialDirectory = filePath;

                    if (sd.ShowDialog() ?? false)
                    {
                        File.WriteAllText(sd.FileName, previewDialog.ResultText);
                    }
                    
                   
                   
                    

                }

            }




        }


    }
}
