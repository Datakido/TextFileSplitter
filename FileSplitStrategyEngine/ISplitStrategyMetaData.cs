namespace Datakido.FileSplitStrategyEngine
{
    public interface ISplitStrategyMetaData
    {
        /// <summary>
        /// The human friendly name for this split strategy.
        /// </summary>
        string StrategyName { get; }

        /// <summary>
        /// The date this strategy was created.
        /// </summary>
        string CreationDate { get; }

        /// <summary>
        /// Who created this strategy.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// The current split strategy version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// The long description for this split strategy.
        /// </summary>
        string Description { get; }
    }
}