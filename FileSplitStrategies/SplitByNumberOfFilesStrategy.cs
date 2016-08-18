using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Datakido.FileSplitStrategyEngine;

namespace Datakido.FileSplitStrategies
{
    [Export(typeof(ISplitStrategy))]
    public class SplitByNumberOfFilesStrategy : ISplitStrategy
    {
        #region Implementation of ISplitStrategyMetaData

        /// <summary>
        /// The human friendly name for this split strategy.
        /// </summary>
        public string StrategyName
        {
            get { return "Split By Number Of Files"; }
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
            get { return "Splits a text file by a preselected number of files."; }
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
            get { return "numberfiles"; }
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
            sb.Append(NumberOfFiles);

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
                NumberOfFiles = int.Parse(CommandLineMainArgument);
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

            var currSourceFile = new FileInfo(Context.SourceFilePath);

            Int64 headerSize = Context.Headers.Aggregate(0l, (current, s) => current + s.Length);

            Int64 threshold = (currSourceFile.Length / NumberOfFiles) + (1024 - headerSize);

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

                    string destFilePath = Path.Combine(Context.DestinationFilePath,Context.ProcessFilePattern(fileCounter));
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

        public SplitByNumberOfFilesStrategy()
        {
            SettingsControl = new SplitByNumberOfFilesSettingsControl();
        }

        #endregion

        #region Properties

        public int NumberOfFiles
        {
            get
            {
                var settingsControl = (SplitByNumberOfFilesSettingsControl)SettingsControl;
                string numberOfChunks = settingsControl.txtChunkCount.Text;

                return int.Parse(numberOfChunks);
            }

            set
            {
                var control = (SplitByNumberOfFilesSettingsControl)SettingsControl;
                control.txtChunkCount.Text = value.ToString();
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
