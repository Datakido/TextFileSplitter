using System.Windows.Forms;

namespace SystemWidgets.FileSplitStrategies
{
    public partial class SplitByRegularExpressionSettingsControl : UserControl
    {
        public SplitByRegularExpressionSettingsControl()
        {
            InitializeComponent();
        }

        private void btnResetRegex_Click(object sender, System.EventArgs e)
        {
            chkOmit.Checked = false;
            chkFileName.Checked = false;
        }

        private void txtRegex_TextChanged(object sender, System.EventArgs e)
        {
            if (txtRegex.Text.Length == 0)
            {
                epRegex.SetError(txtRegex, "This is a required field.");
            }
            else
            {
                epRegex.Clear();
            }
        }
    }
}
