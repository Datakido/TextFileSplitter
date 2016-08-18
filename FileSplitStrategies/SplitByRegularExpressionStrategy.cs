using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Datakido.FileSplitStrategyEngine;

namespace Datakido.FileSplitStrategies
{
    [Export(typeof(ISplitStrategy))]
    public class SplitByRegularExpressionStrategy : ISplitStrategy
    {
        #region Implementation of ISplitStrategyMetaData

        /// <summary>
        /// The human friendly name for this split strategy.
        /// </summary>
        public string StrategyName
        {
            get { return "Split By Regular Expression"; }
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
            get { return "Splits a text file using a regular expression to find file chunk boundaries."; }
        }

        #endregion

        #region Implementation of ISplitStrategy

        /// <summary>
        /// This tells the processing engine whether this strategy has a valid list of command line switches in order to work properly.
        /// </summary>
        public bool CommandLineIsValid { get; set; }

        /// <summary>
        /// This is the strategy's main command line argument value.
        /// </summary>
        public string CommandLineMainArgument { get; set; }

        /// <summary>
        /// This is the command line switch that will be used to match this strategy when running the console program.
        /// </summary>
        public string CommandLineSwitch
        {
            get { return "regex"; }
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
            sb.Append(CommandLineMainArgument);
            sb.Append(RegularExpression);

            if (BoundaryAsFileName)
            {
                sb.Append(" -boundaryasfilename");
            }

            if (OmitBoundaryLine)
            {
                sb.Append(" -omitboundary");
            }

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
                RegularExpression = CommandLineMainArgument;

                foreach (var s in extraswitches)
                {
                    switch (s.ToLower())
                    {
                        case "boundaryasfilename":
                            BoundaryAsFileName = true;
                            break;
                        case "omitboundary":
                            OmitBoundaryLine = true;
                            break;
                    }
                }

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
            bool controlBreak = false;
            bool firstLine = true;

            var settingsControl = (SplitByRegularExpressionSettingsControl)SettingsControl;
            string regularExpression = settingsControl.txtRegex.Text;
            OmitBoundaryLine = settingsControl.chkOmit.Checked;
            BoundaryAsFileName = settingsControl.chkFileName.Checked; 
            
            var chunkInfo = new FileInfo(Path.Combine(Context.DestinationFilePath, newChunkFileName));

            if (BoundaryAsFileName)
            {
                TextReader firstFile = new StreamReader(Context.SourceFilePath);
                string header = firstFile.ReadLine();
                firstFile.Close();

                try
                {
                    controlBreak = Regex.IsMatch(header, regularExpression);
                }
                catch (ArgumentException ex)
                {
                    // Syntax error in the regular expression
                    Console.WriteLine(ex.Message);
                }

                newChunkFileName = CleanupHeader(header);
                newChunkFileName = string.Concat(newChunkFileName, chunkInfo.Extension);
                chunkInfo = new FileInfo(Path.Combine(Context.DestinationFilePath, newChunkFileName));
            }
            
            var destStream = new FileStream(chunkInfo.FullName, FileMode.CreateNew);
            TextWriter writer = Context.CreateEncodedWriter(destStream);

            while (Context.Reader.Peek() != -1)
            {
                string line = Context.Reader.ReadLine();

                if (!firstLine)
                {
                    try
                    {
                        controlBreak = Regex.IsMatch(line, regularExpression);
                    }
                    catch (ArgumentException ex)
                    {
                        // Syntax error in the regular expression
                        Console.WriteLine(ex.Message);
                    }
                }

                if (!firstLine)
                {
                    if (controlBreak)
                    {
                        fileCounter += 1;
                        destStream.Flush();
                        writer.Flush();
                        writer.Close();
                        destStream.Close();

                        chunkInfo = new FileInfo(Path.Combine(Context.DestinationFilePath, Context.ProcessFilePattern(fileCounter)));

                        if (BoundaryAsFileName)
                        {
                            newChunkFileName = CleanupHeader(line);
                            newChunkFileName = string.Concat(newChunkFileName, chunkInfo.Extension);
                            chunkInfo = new FileInfo(Path.Combine(Context.DestinationFilePath, newChunkFileName));
                        }

                        destStream = new FileStream(chunkInfo.FullName, FileMode.CreateNew);
                        writer = Context.CreateEncodedWriter(destStream);

                        if (Context.KeepHeaders)
                        {
                            Context.WriteHeaderLines(writer);
                        }

                        writer.Flush();
                    }
                }

                if (firstLine)
                {
                    if (Context.KeepHeaders)
                    {
                        Context.WriteHeaderLines(writer);
                    }

                    try
                    {
                        controlBreak = Regex.IsMatch(line, regularExpression);
                    }
                    catch (ArgumentException ex)
                    {
                        // Syntax error in the regular expression
                        Console.WriteLine(ex.Message);
                    }
                }

                if (OmitBoundaryLine)
                {
                    if (!controlBreak)
                    {
                        writer.WriteLine(line);
                    }
                }
                else
                {
                    writer.WriteLine(line);
                }

                writer.Flush();

                firstLine = false;
            }

            writer.Flush();
            writer.Close();
            destStream.Close();
        }

        /// <summary>
        /// This represents the control that will offer a UI for configuration.
        /// </summary>
        public UserControl SettingsControl { get; set; }

        #endregion

        #region Constructors

        public SplitByRegularExpressionStrategy()
        {
            SettingsControl = new SplitByRegularExpressionSettingsControl();
        }

        #endregion

        #region Properties

        public string RegularExpression
        {
            get
            {
                var control = (SplitByRegularExpressionSettingsControl)SettingsControl;

                return control.txtRegex.Text;
            }

            set
            {
                var control = (SplitByRegularExpressionSettingsControl)SettingsControl;
                control.txtRegex.Text = value;
            }
        }

        public bool BoundaryAsFileName
        {
            get
            {
                var control = (SplitByRegularExpressionSettingsControl)SettingsControl;

                return control.chkFileName.Checked;
            }

            set
            {
                var control = (SplitByRegularExpressionSettingsControl)SettingsControl;
                control.chkFileName.Checked = value;
            }
        }

        public bool OmitBoundaryLine
        {
            get
            {
                var control = (SplitByRegularExpressionSettingsControl)SettingsControl;

                return control.chkOmit.Checked;
            }

            set
            {
                var control = (SplitByRegularExpressionSettingsControl)SettingsControl;
                control.chkOmit.Checked = value;
            }
        }

        #endregion

        #region Public Members

        public override string ToString()
        {
            return StrategyName;
        }

        #endregion

        #region Private Members

        private static string CleanupHeader(string line)
        {
            return Regex.Replace(line, @"[\[\]?:\/*""<>|]", "");
        }

        #endregion
    }
}
