using AlgoStudio.ABI.ARC32;
using System.Windows.Controls;

namespace AlgoStudio.VSIX.Controls
{
    /// <summary>
    /// Interaction logic for ContractJsonToSCR.xaml
    /// </summary>
    public partial class ContractJsonToSCR : BaseDialogWindow
    {
        private AppDescription contractDescription;
        public string ResultText = "";
        public ContractJsonToSCR(AppDescription cd,string nameSpace)
        {
            contractDescription = cd;
            InitializeComponent();
            textBox_name.Text = contractDescription.Contract?.Name;
            if (!string.IsNullOrEmpty(nameSpace)) textBox_namespace.Text = nameSpace;
        }

        private void textBox_namespace_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePreview();
        }

        private void textBox_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePreview();
        }

        private void BaseDialogWindow_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (textBox_preview != null)
            {
                textBox_preview.Text = contractDescription.ToSmartContractReference(textBox_namespace.Text, textBox_name.Text);
            }
        }

        private void button_save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ResultText= contractDescription.ToSmartContractReference(textBox_namespace.Text, textBox_name.Text);
            DialogResult = true;
        }

        private void button_cancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
