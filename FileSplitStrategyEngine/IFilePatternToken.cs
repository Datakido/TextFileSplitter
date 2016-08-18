namespace Datakido.FileSplitStrategyEngine
{
    public interface IFilePatternToken
    {
        /// <summary>
        /// The description for this token.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The human friendly name for this token.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The actual token that will be used.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Contains logic on how to find this token in a string.
        /// </summary>
        /// <param name="currentPattern">The file pattern that is being tested.</param>
        /// <returns>True if there is a match. False if no match was found.</returns>
        bool Match(string currentPattern);

        /// <summary>
        /// Contains the logic that will replace the token with the specified content.
        /// </summary>
        /// <param name="currentPattern">The file pattern that is being tested.</param>
        /// <param name="fileName">The name of the file that is being used to build the pattern.</param>
        /// <param name="fileChunkNumber">The numeric sequence for the current file chunk being built.</param>
        /// <returns>Returns a string with the current token replaced with the specified content.</returns>
        string Replace(string currentPattern, string fileName, int fileChunkNumber);
    }
}