using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SystemWidgets.FileSplitStrategyEngine;

namespace SystemWidgets.FileSplitStrategies
{
    [Export(typeof(ISplitStrategy))]
    public class SplitByTextBoundaryStrategy : ISplitStrategy
    {
        #region Implementation of ISplitStrategyMetaData

        /// <summary>
        /// The human friendly name for this split strategy.
        /// </summary>
        public string StrategyName
        {
            get { return "Split By Text Boundary"; }
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
            get { return "Splits a text file by a text boundary. This boundary can be literal or can be checked to see if it contains a specific text."; }
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
        /// This is the command line switch that will be used to match this strategy when running from the command line.
        /// </summary>
        public string CommandLineSwitch
        {
            get { return "boundary"; }
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
            sb.Append(Boundary);

            if (BoundaryAsFileName)
            {
                sb.Append(" -boundaryasfilename");
            }

            if (OmitBoundaryLine)
            {
                sb.Append(" -omitboundary");
            }

            if (TextIsLiteral)
            {
                sb.Append(" -textliteral");
            }

            if (TextContains)
            {
                sb.Append(" -textcontains");
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
                Boundary = CommandLineMainArgument;

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
                        case "testliteral":
                            TextIsLiteral = true;
                            break;
                        case "testcontains":
                            TextContains = true;
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

            var settingsControl = (SplitByTextBoundarySettingsControl)SettingsControl;
            Boundary = settingsControl.txtBoundary.Text;
            OmitBoundaryLine = settingsControl.chkOmit.Checked;
            BoundaryAsFileName = settingsControl.chkFileName.Checked;
            TextContains = settingsControl.rabContains.Checked;
            TextIsLiteral = settingsControl.rabLiteral.Checked;

            var chunkInfo = new FileInfo(Path.Combine(Context.DestinationFilePath, newChunkFileName));

            if (BoundaryAsFileName)
            {
                TextReader firstFile = new StreamReader(Context.SourceFilePath);
                string header = firstFile.ReadLine();
                firstFile.Close();

                if (TextContains)
                {
                    controlBreak = header.Contains(Boundary);
                }

                if (TextIsLiteral)
                {
                    controlBreak = header.Equals(Boundary);
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

                if (TextContains)
                {
                    controlBreak = line.Contains(Boundary);
                }

                if (TextIsLiteral)
                {
                    controlBreak = line.Equals(Boundary);
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

        public SplitByTextBoundaryStrategy()
        {
            SettingsControl = new SplitByTextBoundarySettingsControl();
        }

        #endregion

        #region Properties

        public string Boundary
        {
            get
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;

                return control.txtBoundary.Text;
            }

            set
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;
                control.txtBoundary.Text = value;
            }
        }

        public bool BoundaryAsFileName
        {
            get
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;

                return control.chkFileName.Checked;
            }

            set
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;
                control.chkFileName.Checked = value;
            }
        }

        public bool OmitBoundaryLine
        {
            get
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;

                return control.chkOmit.Checked;
            }

            set
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;
                control.chkOmit.Checked = value;
            }
        }

        public bool TextIsLiteral
        {
            get
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;

                return control.rabLiteral.Checked;
            }

            set
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;
                control.rabLiteral.Checked = value;
            }
        }

        public bool TextContains
        {
            get
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;

                return control.rabContains.Checked;
            }

            set
            {
                var control = (SplitByTextBoundarySettingsControl)SettingsControl;
                control.rabContains.Checked = value;
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
