
namespace SystemWidgets.FileSplitStrategyEngine
{
    using System.Text;

    /// <summary>
    /// Holds information about a file's encoding.
    /// </summary>
    public class EncodingMetaInfo
    {
        /// <summary>
        /// Gets or sets the text encoding.
        /// </summary>
        /// <value>
        /// The text encoding.
        /// </value>
        public Encoding TextEncoding { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has BOM.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has BOM; otherwise, <c>false</c>.
        /// </value>
        public bool HasBom { get; set; }
    }
}
