using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SystemWidgets.FileSplitStrategyEngine;

namespace SystemWidgets.FileSplitStrategies
{
    [Export(typeof(ISplitStrategy))]
    public class SplitBySizeStrategy : ISplitStrategy
    {
        #region Implementation of ISplitStrategyMetaData

        /// <summary>
        /// The human friendly name for this split strategy.
        /// </summary>
        public string StrategyName
        {
            get { return "Split By Size"; }
        }

        /// <summary>
        /// The date this strategy was created.
        /// </summary>
        public string CreationDate
        {
            get
            {
                string fullPath = Assembly.GetExecutingAssembly().Location;
                var info = new FileInfo(fullPath);
                string date = info.CreationTime.ToShortDateString();

                return date;
            }
        }

        /// <summary>
        /// Who created this strategy.
        /// </summary>
        public string Author
        {
            get { return "SystemWidgets"; }
        }

        /// <summary>
        /// The current split strategy version.
        /// </summary>
        public string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        /// <summary>
        /// The long description for this split strategy.
        /// </summary>
        public string Description
        {
            get { return "Splits a text file by a size. Each text file chunk will be the exact size. Sometimes the last chunk will be smaller, and that's normal."; }
        }

        #endregion

        #region Implementation of ISplitStrategy

        /// <summary>
        /// This tells the processing engine whether this strategy has a valid list of command line switches in order to work properly.
        /// </summary>
        public bool CommandLineIsValid { get; private set; }

        /// <summary>
        /// This is the strategy's main command line argument value.
        /// </summary>
        public string CommandLineMainArgument { get; set; }

        /// <summary>
        /// This is the command line switch that will be used to match this strategy when running the console program.
        /// </summary>
        public string CommandLineSwitch
        {
            get { return "kbs"; }
        }

        /// <summary>
        /// Contains all the needed information for the file split logic.
        /// </summary>
        public FileSplitContext Context { get; set; }

        /// <summary>
        /// This will give the caller the command-line switches for
        /// this strategy, as it is currently set.
        /// </summary>
        /// <returns></returns>
        public string CreateCommandLineSwitches()
        {
            var sb = new StringBuilder();

            sb.Append("-splitstrategy:");
            sb.Append(CommandLineSwitch);
            sb.Append(":");
            sb.Append(CurrentFileSystemSize);

            return sb.ToString();
        }

        /// <summary>
        /// Processes the argument parameters to see if a match was found.
        /// </summary>
        /// <param name="argument">Should contain the contents of the strategy switch.</param>
        /// <returns>Should return true, if the argument matches the strategy's assigned switch.</returns>
        public bool MatchCommandLineSwitch(string argument)
        {
            return argument.Contains(CommandLineSwitch);
        }

        /// <summary>
        /// This is used for strategies that require extra switches
        /// </summary>
        /// <param name="extraswitches">A list of switches to process.</param>
        public void ProcessExtraCommandLineSwitches(string[] extraswitches)
        {
            if (!string.IsNullOrEmpty(CommandLineMainArgument))
            {
                SplitBySizeThreshold = int.Parse(CommandLineMainArgument);
                CommandLineIsValid = true;
            }
            else
            {
                CommandLineIsValid = false;
            }
        }

        /// <summary>
        /// This method will start the file splitting process for this strategy.
        /// </summary>
        public void Start()
        {
            int fileCounter = 1;
            string newChunkFileName = Context.ProcessFilePattern(fileCounter);
            var chunkInfo = new FileInfo(Path.Combine(Context.DestinationFilePath, newChunkFileName));
            var destStream = new FileStream(chunkInfo.FullName, FileMode.CreateNew);
            TextWriter writer = Context.CreateEncodedWriter(destStream);
            var settingsControl = (SplitBySizeSettingsControl) SettingsControl;
            Int64 threshold = settingsControl.SplitThreshold;

            if (Context.KeepHeaders)
            {
                Context.WriteHeaderLines(writer);
            }

            while (Context.Reader.Peek() != -1)
            {
                string line = Context.Reader.ReadLine();

                if (destStream.Position + line.Length + 2 >= threshold)
                {
                    fileCounter += 1;
                    writer.Flush();
                    writer.Close();
                    destStream.Close();

                    string destFilePath = Path.Combine(Context.DestinationFilePath,Context.ProcessFilePattern(fileCounter)); ;
                    destStream = new FileStream(destFilePath, FileMode.CreateNew);
                    writer = Context.CreateEncodedWriter(destStream);

                    if (Context.KeepHeaders)
                    {
                        Context.WriteHeaderLines(writer);
                    }

                    writer.Flush();
                }

                writer.WriteLine(line);
                writer.Flush();
            }

            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// This represents the control that will offer a UI for configuration.
        /// </summary>
        public UserControl SettingsControl { get; set; }

        #endregion

        #region Constructors

        public SplitBySizeStrategy()
        {
            SettingsControl = new SplitBySizeSettingsControl();
        }

        #endregion

        #region Properties

        public Int64 SplitBySizeThreshold
        {
            get
            {
                var control = (SplitBySizeSettingsControl)SettingsControl;

                return control.SplitThreshold;
            }

            set
            {
                var control = (SplitBySizeSettingsControl)SettingsControl;
                control.SplitThreshold = value;
            }
        }
 
        public string CurrentFileSystemSize
        {
            get
            {
                var control = (SplitBySizeSettingsControl)SettingsControl;

                return control.CalculateSize().ToString();
            }
        }

        #endregion

        #region Public Members

        public override string ToString()
        {
            return StrategyName;
        }

        #endregion
    }
}