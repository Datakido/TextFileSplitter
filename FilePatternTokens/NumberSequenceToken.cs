using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using SystemWidgets.FileSplitStrategyEngine;

namespace SystemWidgets.FilePatternTokens
{
    [Export(typeof(IFilePatternToken))]
    public class NumberSequenceToken : IFilePatternToken
    {
        private string _matchedPattern;

        #region Implementation of IFilePatternToken

        /// <summary>
        /// The description for this token.
        /// </summary>
        public string Description
        {
            get { return "Gets replaced by a sequential numeric sequence."; }
        }

        /// <summary>
        /// The human friendly name for this token.
        /// </summary>
        public string Name
        {
            get { return "Number Sequence"; }
        }

        /// <summary>
        /// The actual token that will be used.
        /// </summary>
        public string Token
        {
            get { return "[SEQUENCE:0]"; }
        }

        /// <summary>
        /// Contains logic on how to find this token in a string.
        /// </summary>
        /// <param name="currentPattern">The file pattern that is being tested.</param>
        /// <returns>True if there is a match. False if no match was found.</returns>
        public bool Match(string currentPattern)
        {
            bool retval;

            try
            {
                try
                {
                    bool foundMatch = Regex.IsMatch(currentPattern, @"\[SEQUENCE:\d*\]");

                    if (foundMatch)
                    {
                        try
                        {
                            _matchedPattern = Regex.Match(currentPattern, @"\[SEQUENCE:\d*\]").Value;
                            retval = true;
                        }
                        catch (ArgumentException)
                        {
                            // Syntax error in the regular expression
                            retval = false;
                        }
                    }
                    else
                    {
                        retval = false;
                    }
                }
                catch (ArgumentException)
                {
                    // Syntax error in the regular expression
                    retval = false;
                }
            }
            catch (ArgumentException)
            {
                // Syntax error in the regular expression
                retval = false;
            }

            return retval;
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
            string[] seqParts = _matchedPattern.Split(':');
            string seqFormat = seqParts[1];
            seqFormat = seqFormat.Replace("]", "");
            string seqProcessed = fileChunkNumber.ToString(seqFormat);

            string processedSequence = currentPattern.Replace(_matchedPattern, seqProcessed);

            return processedSequence;
        }

        #endregion
    }
}
