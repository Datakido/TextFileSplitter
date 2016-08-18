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
    public class SplitOffChunksAndStop : ISplitStrategy
    {
        #region Implementation of ISplitStrategyMetaData

        /// <summary>
        /// The human friendly name for this split strategy.
        /// </summary>
        public string StrategyName
        {
            get { return "Split Off Chunks And Stop"; }
        }

        /// <summary>
        /// The date this strategy was created.
        /// </summary>
        public string CreationDate
        {
            get
            {
                var createDate = new DateTime(2010, 5, 4);
                return createDate.ToShortDateString();
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
            get {
                var sb = new StringBuilder();
                sb.Append("Gets several pieces of the source file of the specified size. ");
                sb.Append("It starts from the top of the source file, but stops splitting before reading the whole file.");

                return sb.ToString(); 
            }
        }

        #endregion

        #region Implementation of ISplitStrategy

        /// <summary>
        /// This tells the processing engine whether this strategy has a valid
        /// list of command line switches in order to work properly.
        /// </summary>
        public bool CommandLineIsValid { get; private set; }

        /// <summary>
        /// This is the strategy's main command line argument value.
        /// </summary>
        public string CommandLineMainArgument { get; set; }

        /// <summary>
        /// This is the command line switch that will be used to match this
        /// strategy when running from the command line.
        /// </summary>
        public string CommandLineSwitch
        {
            get { return "chunkandstop"; }
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
            sb.Append(":");
            sb.Append(ChunkCount);

            return sb.ToString();
        }

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
        public bool MatchCommandLineSwitch(string argument)
        {
            return argument.Contains(CommandLineSwitch);
        }

        /// <summary>
        /// This is used for strategies that require extra switches
        /// </summary>
        /// <param name="extraswitches">
        /// A string array containing switches to process.
        /// </param>
        public void ProcessExtraCommandLineSwitches(string[] extraswitches)
        {
            if (!Equals(extraswitches, null))
            {
                int lastSwitch = extraswitches.Count() - 1;
                string[] switches = extraswitches[lastSwitch].Split(':');

                SplitBySizeThreshold = int.Parse(switches[1]);
                ChunkCount = switches[2];

                CommandLineIsValid = true;
            }
            else
            {
                CommandLineIsValid = false;
            }
        }

        /// <summary>
        /// This method will start the file splitting process for this
        /// strategy.
        /// </summary>
        public void Start()
        {
            int fileCounter = 1;
            var settingsControl = (SplitOffChunksAndStopSettingsControl)SettingsControl;
            
            Int64 headerSize;
            if (!Equals(null, Context.Headers))
            {
                headerSize = Context.Headers.Aggregate(0l, (current, s) => current + s.Length);
            }
            else
            {
                headerSize = 0;
            }

            Int64 threshold;

            if (Context.KeepHeaders)
            {
                threshold = settingsControl.SplitThreshold + headerSize;
            }
            else
            {
                threshold = settingsControl.SplitThreshold;
            }

            bool doneChunking = false;
            string newChunkFileName = Context.ProcessFilePattern(fileCounter);
            var chunkInfo = new FileInfo(Path.Combine(Context.DestinationFilePath, newChunkFileName));

            // Delete the file, if it already exists.
            if (File.Exists(chunkInfo.FullName))
            {
                File.Delete(chunkInfo.FullName);
            }

            var destStream = new FileStream(chunkInfo.FullName, FileMode.CreateNew);
            TextWriter writer = Context.CreateEncodedWriter(destStream);

            if (Context.KeepHeaders)
            {
                Context.WriteHeaderLines(writer);
            }

            while (Context.Reader.Peek() != -1)
            {
                string line = Context.Reader.ReadLine();

                if (destStream.Position + line.Length + 2 >= threshold)
                {
                    writer.Flush();
                    writer.Close();
                    destStream.Close();

                    fileCounter++;

                    if (fileCounter > int.Parse(ChunkCount))
                    {
                        doneChunking = true;
                    }
                    else
                    {
                        string destFilePath = Path.Combine(Context.DestinationFilePath, Context.ProcessFilePattern(fileCounter)); ;

                        // Delete the file, if it already exists.
                        if (File.Exists(destFilePath))
                        {
                            File.Delete(destFilePath);
                        }

                        destStream = new FileStream(destFilePath, FileMode.CreateNew);
                        writer = Context.CreateEncodedWriter(destStream);

                        if (Context.KeepHeaders)
                        {
                            Context.WriteHeaderLines(writer);
                        }

                        writer.Flush();
                    }
                }
                else
                {
                    writer.WriteLine(line);
                    writer.Flush();
                }

                if (doneChunking)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This represents the control that will offer a UI for configuration.
        /// </summary>
        public UserControl SettingsControl { get; set; }

        #endregion

        #region Constructors

        public SplitOffChunksAndStop()
        {
            SettingsControl = new SplitOffChunksAndStopSettingsControl();
        }

        #endregion

        #region Properties

        public Int64 SplitBySizeThreshold
        {
            get
            {
                var control = (SplitOffChunksAndStopSettingsControl)SettingsControl;

                return control.SplitThreshold;
            }

            set
            {
                var control = (SplitOffChunksAndStopSettingsControl)SettingsControl;
                control.SplitThreshold = value;
            }
        }

        public string CurrentFileSystemSize
        {
            get
            {
                var control = (SplitOffChunksAndStopSettingsControl)SettingsControl;

                return control.CalculateSize().ToString();
            }
        }

        public string ChunkCount
        {
            get
            {
                var settingsControl = (SplitOffChunksAndStopSettingsControl)SettingsControl;
                int chunkCount = int.Parse(settingsControl.CurrentChunkCount);

                return chunkCount.ToString();
            }

            set
            {
                var settingsControl = (SplitOffChunksAndStopSettingsControl)SettingsControl;
                settingsControl.CurrentChunkCount = value;
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
