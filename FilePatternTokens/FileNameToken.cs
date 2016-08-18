using System.ComponentModel.Composition;
using System.IO;
using Datakido.FileSplitStrategyEngine;

namespace Datakido.FilePatternTokens
{
    [Export(typeof(IFilePatternToken))]
    public class FileNameToken : IFilePatternToken
    {
        #region Implementation of IFilePatternToken

        /// <summary>
        /// The description for this token.
        /// </summary>
        public string Description
        {
            get { return "Gets replaced by the file name, without the extension."; }
        }

        /// <summary>
        /// The human friendly name for this token.
        /// </summary>
        public string Name
        {
            get { return "File Name"; }
        }

        /// <summary>
        /// The actual token that will be used.
        /// </summary>
        public string Token
        {
            get { return "[FILENAME]"; }
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
            var name = Path.GetFileNameWithoutExtension(fileName);

            string newPattern = currentPattern.Replace(Token, name);

            return newPattern;
        }

        #endregion
    }
}
