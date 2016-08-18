using System;
using System.Windows.Forms;

namespace Datakido.FileSplitStrategies
{
    public partial class SplitOffChunksAndStopSettingsControl : UserControl
    {
        #region Member Variables

        private bool _isLoaded; 

        #endregion

        #region Constructors

        public SplitOffChunksAndStopSettingsControl()
        {
            InitializeComponent();

            cbMeasure.SelectedIndex = 0;
            txtSize.Focus();
        } 

        #endregion

        #region Properties

        public Int64 SplitThreshold { get; set; } 

        public string CurrentMeasure
        {
            get
            {
                string retval;

                if (string.IsNullOrEmpty(cbMeasure.SelectedText))
                {
                    retval = cbMeasure.Text;
                }
                else
                {
                    retval = cbMeasure.SelectedText;
                }

                return retval;
            }
        }

        public string CurrentMeasureAmount
        {
            get { return txtSize.Text; }
        }

        public string CurrentChunkCount
        {
            get { return txtChunkCount.Text.Trim(); }

            set { txtChunkCount.Text = value; }
        }

        #endregion

        #region Public Members

        public Int64 CalculateSize()
        {
            Int64 retval = 0L;

            switch (CurrentMeasure)
            {
                case "KB - Kilobytes":
                    retval = Convert.ToInt64(CurrentMeasureAmount) * 1024;
                    break;
                case "MB - Megabytes":
                    retval = Convert.ToInt64(CurrentMeasureAmount) * 1048576;
                    break;
                case "GB - Gigabytes":
                    retval = Convert.ToInt64(CurrentMeasureAmount) * 1073741824;
                    break;
            }

            return retval;
        } 

        #endregion

        #region Private Members

        private void cbMeasure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSize.Text.Length > 0)
            {
                GetMeasure();
            }
        }

        private void txtSize_TextChanged(object sender, EventArgs e)
        {
            if (txtSize.Text.Length > 0)
            {
                GetMeasure();
            }
        }

        private void GetMeasure()
        {
            if (_isLoaded)
            {
                SplitThreshold = CalculateSize();
            }
        }

        private void SplitOffOneChunkSettingsControl_Resize(object sender, EventArgs e)
        {
            _isLoaded = true;
        } 

        #endregion
    }
}
