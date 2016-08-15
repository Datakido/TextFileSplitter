using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SystemWidgets.FileSplitStrategyEngine
{
    /// <summary>
    /// This creates a context object for the strategy classes. It has metadata that is needed to accomplish splitting tasks.
    /// </summary>
    public class FileSplitContext
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file encoding metadata.
        /// </summary>
        /// <value>
        /// The file encoding metadata.
        /// </value>
        public TextEncodingMetadata FileEncodingMetadata { get; set; }

        /// <summary>
        /// Gets or sets the file pattern tokens.
        /// </summary>
        /// <value>
        /// The file pattern tokens.
        /// </value>
        public IEnumerable<IFilePatternToken> FilePatternTokens { get; set; }

        /// <summary>
        /// Gets or sets the file headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public List<string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the file reader.
        /// </summary>
        /// <value>
        /// The reader.
        /// </value>
        public StreamReader Reader { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to keep headers.
        /// </summary>
        /// <value>
        ///   <c>true</c> if keep headers; otherwise, <c>false</c>.
        /// </value>
        public bool KeepHeaders { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use a custom file name pattern.
        /// </summary>
        /// <value>
        ///   <c>true</c> if split strategy wants to use a custom file name pattern; otherwise, <c>false</c>.
        /// </value>
        public bool UseFileNamePattern { get; set; }

        /// <summary>
        /// Gets or sets the file header line count.
        /// </summary>
        /// <value>
        /// The header line count.
        /// </value>
        public int HeaderLineCount { get; set; }

        /// <summary>
        /// Gets or sets the destination file path.
        /// </summary>
        /// <value>
        /// The destination file path.
        /// </value>
        public string DestinationFilePath { get; set; }

        /// <summary>
        /// Gets or sets the file name pattern.
        /// </summary>
        /// <value>
        /// The file name pattern.
        /// </value>
        public string FileNamePattern { get; set; }

        /// <summary>
        /// Gets or sets the source path for the file being split.
        /// </summary>
        /// <value>
        /// The source file path.
        /// </value>
        /// <remarks>
        /// This could also be a directory.
        /// </remarks>
        public string SourceFilePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the source is a directory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the source is directory; otherwise, <c>false</c>.
        /// </value>
        public bool SourceIsDirectory { get; set; }

        #endregion

        #region Public Members

        /// <summary>
        /// Initializes the context object for the current strategy.
        /// </summary>
        /// <param name="sourceFile">The source text file.</param>
        public void InitContext(string sourceFile)
        {
            if (!SourceFileExists(sourceFile))
            {
                return;
            }

            this.Headers = new List<string>();

            this.Reader = File.OpenText(sourceFile);

            if (this.KeepHeaders)
            {
                this.GetHeaderLines(this.Reader);
            }
        }

        /// <summary>
        /// Processes the file pattern.
        /// </summary>
        /// <param name="processedFilePattern">The processed file pattern.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="currentFileNumber">The current file number.</param>
        /// <returns>Returns a string containing a processed file name patters.</returns>
        public string ProcessFilePattern(string processedFilePattern, string fileName, int currentFileNumber)
        {
            string processedFiledPattern = string.Empty;

            foreach (var token in this.FilePatternTokens)
            {
                if (token.Match(processedFilePattern))
                {
                    processedFilePattern = token.Replace(processedFilePattern, fileName, currentFileNumber);

                    processedFiledPattern = processedFilePattern;
                }
            }

            return processedFiledPattern;
        }

        /// <summary>
        /// Processes the file pattern.
        /// </summary>
        /// <param name="currentFileNumber">The current file number.</param>
        /// <returns>Returns a string containing a processed file name patters.</returns>
        public string ProcessFilePattern(int currentFileNumber)
        {
            string processedFiledPattern = this.FileNamePattern;

            foreach (var token in this.FilePatternTokens)
            {
                if (token.Match(this.FileNamePattern))
                {
                    processedFiledPattern = token.Replace(processedFiledPattern, this.SourceFilePath, currentFileNumber);
                }
            }

            return processedFiledPattern;
        }

        /// <summary>
        /// Writes the header lines into the new file chunk.
        /// </summary>
        /// <param name="destinationFile">The destination file.</param>
        public void WriteHeaderLines(TextWriter destinationFile)
        {
            foreach (var s in this.Headers)
            {
                destinationFile.WriteLine(s);
                destinationFile.Flush();
            }
        }

        /// <summary>
        /// Creates an encoded writer.
        /// </summary>
        /// <param name="currentFile">The current file.</param>
        /// <returns>A <c>StreamWriter</c> that will write the file chunks.</returns>
        public StreamWriter CreateEncodedWriter(Stream currentFile)
        {
            StreamWriter chunkWriter;

            if (this.FileEncodingMetadata.HasBom)
            {
                chunkWriter = new StreamWriter(currentFile, this.FileEncodingMetadata.FileEncoding);
            }
            else
            {
                switch (this.FileEncodingMetadata.FileEncoding.BodyName.ToUpper())
                {
                    case "UTF-8":
                        var utf8WithoutBom = new UTF8Encoding(false);
                        chunkWriter = new StreamWriter(currentFile, utf8WithoutBom);
                        break;
                    default:
                        chunkWriter = new StreamWriter(currentFile, this.FileEncodingMetadata.FileEncoding);
                        break;
                }
            }

            return chunkWriter;
        }

        /// <summary>
        /// Creates an encoded writer.
        /// </summary>
        /// <param name="currentFile">The current file.</param>
        /// <returns>A <c>StreamWriter</c> that will write the file chunks.</returns>
        public StreamWriter CreateEncodedWriter(FileSystemInfo currentFile)
        {
            StreamWriter chunkWriter;

            if (this.FileEncodingMetadata.HasBom)
            {
                chunkWriter = new StreamWriter(currentFile.FullName, false, this.FileEncodingMetadata.FileEncoding);
            }
            else
            {
                switch (this.FileEncodingMetadata.FileEncoding.BodyName.ToUpper())
                {
                    case "UTF-8":
                        var utf8WithoutBom = new UTF8Encoding(false);
                        chunkWriter = new StreamWriter(currentFile.FullName, false, utf8WithoutBom);
                        break;
                    default:
                        chunkWriter = new StreamWriter(currentFile.FullName, false, this.FileEncodingMetadata.FileEncoding);
                        break;
                }
            }

            return chunkWriter;
        }

        #endregion

        #region Private Members

        private static bool SourceFileExists(string sourceFile)
        {
            return File.Exists(sourceFile);
        }

        private void GetHeaderLines(TextReader sourceFile)
        {
            for (int i = 0; i < this.HeaderLineCount; i++)
            {
                string line = sourceFile.ReadLine();
                this.Headers.Add(line);
            }
        } 

        #endregion
    }
}