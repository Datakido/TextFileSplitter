using System.ComponentModel.Composition;
using System.IO;
using Datakido.FileSplitStrategyEngine;

namespace Datakido.FilePatternTokens
{
    [Export(typeof(IFilePatternToken))]
    public class FileExtensionToken : IFilePatternToken
    {
        #region Implementation of IFilePatternToken

        /// <summary>
        /// The description for this token.
        /// </summary>
        public string Description
        {
            get { return "Gets replaced with the file's extension."; }
        }

        /// <summary>
        /// The human friendly name for this token.
        /// </summary>
        public string Name
        {
            get { return "File Extension"; }
        }

        /// <summary>
        /// The actual token that will be used.
        /// </summary>
        public string Token
        {
            get { return "[EXT]"; }
        }

        /// <summary>
        /// Contains logic on how to find this token in a string.
        /// </summary>
        /// <param name="currentPattern">The file pattern that is being tested.</param>
        /// <returns>True if there is a match. False if no match was found.</returns>
        public bool Match(string currentPattern)
        {
            return currentPattern.Contains(Token);
        }

        /// <summary>
        /// Contains the logic that will replace the token with the specified content.
        /// </summary>
        /// <param name="currentPattern">The file pattern that is being tested.</param>
        /// <param name="fileName">The name of the file that is being used to build the pattern.</param>
        /// <param name="fileChunkNumber">The numeric sequence for the current file chunk being built.</param>
        /// <returns>Returns a string with the current token replaced with the specified content.</returns>
        public string Replace(string currentPattern, string fileName, int fileChunkNumber)
        {
            var extension = Path.GetExtension(fileName);

            if (extension != null)
            {
                extension = extension.Replace(".", "");
            }

            string newPattern = currentPattern.Replace(Token, extension);

            return newPattern;
        }

        #endregion
    }
}