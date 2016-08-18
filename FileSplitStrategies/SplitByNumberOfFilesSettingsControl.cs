using System;
using System.Windows.Forms;
using Datakido.FileSplitStrategyEngine;

namespace Datakido.FileSplitStrategies
{
    public partial class SplitByNumberOfFilesSettingsControl : UserControl
    {
        public SplitByNumberOfFilesSettingsControl()
        {
            InitializeComponent();
        }

        public string ChunkCount { get; set; }

        private void txtChunkCount_TextChanged(object sender, EventArgs e)
        {
            if (txtChunkCount.Text.Length == 0)
            {
                epFiles.SetError(txtChunkCount, "This is a required field.");
            }
            else
            {
                if (Common.IsInteger(txtChunkCount.Text))
                {
                    if (txtChunkCount.Text == "0")
                    {
                        epFiles.SetError(txtChunkCount, "The amount must be greater than zero.");
                    }
                    else
                    {
                        epFiles.Clear();
                        ChunkCount = txtChunkCount.Text;
                    }
                }
                else
                {
                    epFiles.SetError(txtChunkCount, "This is not a number, or it is invalid.");
                }
            }
        }
    }
}
