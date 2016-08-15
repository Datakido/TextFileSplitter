using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TextFileSplitter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.GetUpperBound(0) >= 0)
            {
                var cmdargs = new Arguments(args);
                Int64 splitSize = 0;
                Int32 lineSize = 0;
                Int32 headerCount = 0;
                bool splitBySize = false;
                bool splitByLines = false;
                bool splitByText = false;
                bool testContains = false;
                bool testLiteral = false;
                bool splitByRegex = false;
                bool keepHeaders = false;
                bool omitBoundaryLine = false;
                bool boundaryAsFileName = false;
                string textToTest = "";
                string regex = "";

                if (!Equals(cmdargs["help"], null) || !Equals(cmdargs["?"], null))
                {
                    ShowUsage();
                }
                else
                {
                    string filePattern = "";
                    string inputFile = cmdargs["i"];
                    string outputDest = cmdargs["o"];
                    

                    if (!Equals(cmdargs["h"], null))
                    {
                        keepHeaders = true;

                        try
                        {
                            headerCount = Convert.ToInt32(cmdargs["h"]);
                        }
                        catch (Exception)
                        {
                            headerCount = 1;
                        }
                    }

                    if (!Equals(cmdargs["ls"], null))
                    {
                        lineSize = Convert.ToInt32(cmdargs["ls"]);
                        splitByLines = true;
                    }

                    if (!Equals(cmdargs["kbs"], null))
                    {
                        splitSize = Convert.ToInt64(cmdargs["kbs"]);
                        splitBySize = true;
                    }

                    if (!Equals(cmdargs["boundary"], null))
                    {
                        splitByText = true;
                        textToTest = cmdargs["boundary"];

                        if (!Equals(cmdargs["testcontains"], null))
                        {
                            testContains = true;
                        }

                        if (!Equals(cmdargs["testliteral"], null))
                        {
                            testLiteral = true;
                        }

                     }

                    if (!Equals(cmdargs["regex"], null))
                    {
                        splitByRegex = true;
                        regex = cmdargs["regex"];
                    }

                    if (!Equals(cmdargs["filepattern"], null))
                    {
                        filePattern = cmdargs["filepattern"];
                    }

                    if (!Equals(cmdargs["omitboundaryline"], null))
                    {
                        omitBoundaryLine = true;
                    }

                    if (!Equals(cmdargs["boundaryasfilename"], null))
                    {
                        boundaryAsFileName = true;
                    }

                    var processor = new FileProcessor();

                    processor.FileToSplit = inputFile;
                    processor.KeepHeaders = keepHeaders;
                    processor.SplitFilesDestination = outputDest;

                    if (!Equals(filePattern, null))
                    {
                        // Make sure that this is a valid file pattern.
                        if (filePattern.Length > 0)
                        {
                            processor.UseFileNamePattern = true;
                            processor.FileNamePattern = filePattern;
                        }
                    }

                    if (keepHeaders)
                    {
                        processor.HeaderLineCount = headerCount;
                    }

                    if (splitByLines)
                    {
                        processor.SplitByLines = true;
                        processor.SplitLineCount = lineSize;
                        processor.SplitByNumberOfLines();
                    }

                    if (splitBySize)
                    {
                        processor.SplitBySize = true;
                        processor.SplitSizeInBytes = splitSize;
                        processor.SplitByNumberOfBytes();
                    }

                    if (omitBoundaryLine)
                    {
                        processor.OmitBoundaryLine = true;
                    }

                    if (boundaryAsFileName)
                    {
                        processor.UseBoundaryLineAsFileName = true;
                    }

                    if (splitByText)
                    {
                        processor.SplitByText = true;
                        processor.TextToTest = textToTest;

                        if (testContains)
                        {
                            processor.TextContainsTest = true;
                        }

                        if (testLiteral)
                        {
                            processor.TextLiteralTest = true;
                        }

                        processor.SplitByTextContainsBoundary();
                    }

                    if (splitByRegex)
                    {
                        processor.SplitByRegEx = true;
                        processor.RegularExpression = regex;
                        processor.SplitByRegularExpression();
                    }
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        private static void ShowUsage()
        {
            //System.Diagnostics.Debugger.Launch();
            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("Text File Splitter v1.4.1  released: December 14, 2008");
            //Console.WriteLine("Copyright (C) 2007-2008 Hector Sosa, Jr");
            //Console.WriteLine("http://www.systemwidgets.com");
            //Console.WriteLine("");
            //Console.WriteLine("Usage: TextFileSplitter [options] -i=<filetosplit> -o=<destinationdir>");
            //Console.WriteLine("Options:");
            //Console.WriteLine("-h              Tells the processor to insert the header into each file chunk.");
            //Console.WriteLine("-ls=<lines>     Used to tell the processor how many lines per text chunk.");
            //Console.WriteLine("-kbs=<bytesize> Used to tell the processor how many bytes per text chunk.");
            //Console.WriteLine("");
            //Console.WriteLine("Examples:");
            //Console.WriteLine("");
            //Console.WriteLine("Example command-line for splitting a file using bytes:");
            //Console.WriteLine(@"-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -h -kbs=100000");
            //Console.WriteLine("");
            //Console.WriteLine("Example command-line for splitting a file using line counts");
            //Console.WriteLine(@"-i=C:\Temp\MonthlyUpdate20070625.txt -o=C:\Temp -h -ls=1000");
            //Console.WriteLine("");
            //Console.WriteLine("Each file will be appended with a dash, then the number in the sequence.");
            //Console.WriteLine("Using the filename above it would look like this:");
            //Console.WriteLine("MonthlyUpdate20070625-1.txt");
            //Console.WriteLine("MonthlyUpdate20070625-2.txt");
            //Console.WriteLine("MonthlyUpdate20070625-3.txt");
            //Console.WriteLine("");
            //Console.WriteLine("Please refer all questions and comments to support@systemwidgets.com");

            string path = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            string fullName = Path.Combine(path, "readme.txt");

            string help = File.ReadAllText(fullName);

            TextWriter helpWriter = Console.Out;

            helpWriter.WriteLine(@help);

            helpWriter.Flush();
        }
    }
}