using System;
using System.Windows.Forms;

namespace SystemWidgets.FileSplitStrategies
{
    public partial class SplitByTextBoundarySettingsControl : UserControl
    {
        public SplitByTextBoundarySettingsControl()
        {
            InitializeComponent();
        }

        private void btnResetText_Click(object sender, EventArgs e)
        {
            chkFileName.Checked = false;
            chkOmit.Checked = false;
        }

        private void txtBoundary_TextChanged(object sender, EventArgs e)
        {
            if (txtBoundary.Text.Length == 0)
            {
                epBoundary.SetError(txtBoundary, "This is a required field.");
            }
            else
            {
                epBoundary.Clear();
            }
        }
    }
}
