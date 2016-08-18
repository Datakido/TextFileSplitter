
namespace Datakido.FileSplitStrategyEngine
{
    using System.Text;

    /// <summary>
    /// Contains information about the file's encoding.
    /// </summary>
    public class TextEncodingMetadata
    {
        /// <summary>
        /// Gets or sets a value indicating whether this file has a Byte Order Mark (BOM).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this file has a Byte Order Mark (BOM); otherwise, <c>false</c>.
        /// </value>
        public bool HasBom { get; set; }

        /// <summary>
        /// Gets or sets the character set name for this file.
        /// </summary>
        /// <value>
        /// The character set name for this file.
        /// </value>
        public string CharacterSet { get; set; }

        /// <summary>
        /// Gets or sets the file encoding.
        /// </summary>
        /// <value>
        /// The file encoding.
        /// </value>
        public Encoding FileEncoding { get; set; }

        /// <summary>
        /// Gets or sets the detection confidence determined by the file encoding detection engine.
        /// </summary>
        /// <value>
        /// The detection confidence.
        /// </value>
        public float DetectionConfidence { get; set; }
    }
}
