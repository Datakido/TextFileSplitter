
namespace Datakido.FileSplitStrategyEngine
{
    using System.Windows.Forms;

    public interface ISplitStrategy : ISplitStrategyMetaData
    {
        /// <summary>
        /// This tells the processing engine whether this strategy has a valid
        /// list of command line switches in order to work properly.
        /// </summary>
        bool CommandLineIsValid { get; }

        /// <summary>
        /// This is the strategy's main command line argument value.
        /// </summary>
        string CommandLineMainArgument { get; set; }

        /// <summary>
        /// This is the command line switch that will be used to match this
        /// strategy when running from the command line.
        /// </summary>
        string CommandLineSwitch { get; }

        /// <summary>
        /// Contains all the needed information for the file split logic.
        /// </summary>
        FileSplitContext Context { get; set; }

        /// <summary>
        /// This will give the caller the command-line switches for
        /// this strategy, as it is currently set.
        /// </summary>
        /// <returns></returns>
        string CreateCommandLineSwitches();

        /// <summary>
        /// Processes the argument parameters to see if a match was found.
        /// </summary>
        /// <param name="argument">
        /// Should contain the contents of the strategy switch.
        /// </param>
        /// <returns>
        /// Should return true, if the argument matches the strategy's 
        /// assigned switch.
        /// </returns>
        bool MatchCommandLineSwitch(string argument);

        /// <summary>
        /// This is used for strategies that require extra switches
        /// </summary>
        /// <param name="extraswitches">
        /// A string array containing switches to process.
        /// </param>
        void ProcessExtraCommandLineSwitches(string[] extraswitches);

        /// <summary>
        /// This method will start the file splitting process for this
        /// strategy.
        /// </summary>
        void Start();

        /// <summary>
        /// This represents the control that will offer a UI for configuration.
        /// </summary>
        UserControl SettingsControl { get; set; }
    }
}