using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using SystemWidgets.FileSplitStrategyEngine;

namespace SystemWidgets.FilePatternTokens
{
    [Export(typeof(IFilePatternToken))]
    public class DateToken : IFilePatternToken
    {
        private string matchedPattern;

        #region Implementation of IFilePatternToken

        /// <summary>
        /// The description for this token.
        /// </summary>
        public string Description
        {
            get { return "Gets replaced with the specified date format"; }
        }

        /// <summary>
        /// The human friendly name for this token.
        /// </summary>
        public string Name
        {
            get { return "Date"; }
        }

        /// <summary>
        /// The actual token that will be used.
        /// </summary>
        public string Token
        {
            get { return "[DATE:yyyyMMdd]"; }
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
                    bool foundMatch = Regex.IsMatch(currentPattern, @"\[DATE:\D*\]");

                    if (foundMatch)
                    {
                        try
                        {
                            this.matchedPattern = Regex.Match(currentPattern, @"\[DATE:\D*\]").Value;
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
            string[] dateParts = this.matchedPattern.Split(':');
            string dateformat = dateParts[1];
            string dtFormat = dateformat.Replace("]", "");

            string datestring = DateTime.Now.ToString(dtFormat);
            string processedDate = currentPattern.Replace(this.matchedPattern, datestring);

            return processedDate;
        }

        #endregion
    }
}
