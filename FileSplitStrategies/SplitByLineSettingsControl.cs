using System.Windows.Forms;
using SystemWidgets.FileSplitStrategyEngine;

namespace SystemWidgets.FileSplitStrategies
{
    public partial class SplitByLineSettingsControl : UserControl
    {
        public SplitByLineSettingsControl()
        {
            InitializeComponent();
        }

        private void txtLines_TextChanged(object sender, System.EventArgs e)
        {
            if (txtLines.Text.Length == 0)
            {
                epLines.SetError(txtLines, "This is a required field.");
            }
            else
            {
                if (Common.IsInteger(txtLines.Text))
                {
                    if (txtLines.Text == "0")
                    {
                        epLines.SetError(txtLines, "The amount must be greater than zero.");
                    }
                    else
                    {
                        epLines.Clear();
                    }
                }
                else
                {
                    epLines.SetError(txtLines, "This is not a number, or it is invalid.");
                }
            }
        }
    }
}