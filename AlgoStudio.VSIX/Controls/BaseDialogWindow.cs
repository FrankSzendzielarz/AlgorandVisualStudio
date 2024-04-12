using Microsoft.VisualStudio.PlatformUI;
using System.Windows;


namespace AlgoStudio.VSIX.Controls
{

    public class BaseDialogWindow : DialogWindow
    {

        public BaseDialogWindow()
        {
            this.HasMaximizeButton = true;
            this.HasMinimizeButton = true;
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
        }
    }
}
