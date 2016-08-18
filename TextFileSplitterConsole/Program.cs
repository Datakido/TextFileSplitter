using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Datakido.FileSplitStrategyEngine;

namespace TextFileSplitterConsole
{
    public class Program
    {
        private static FileSplitContext context;
        private static Arguments cmdargs;
        private static List<string> FilesToProcess;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void Main(string[] args)
        {
            Console.Title = "TextFileSplitterConsole.exe";

            cmdargs = new Arguments(args);

            if (!Equals(cmdargs["help"], null) || !Equals(cmdargs["?"], null))
            {
                ShowUsage();
            }
            else
            {
                context = new FileSplitContext();
                FilesToProcess = new List<string>();

                if (!string.IsNullOrEmpty(cmdargs["hideconsole"]))
                {
                    HideConsole();
                }

                if (!string.IsNullOrEmpty(cmdargs["sourceisdirectory"]))
                {
                    context.SourceIsDirectory = true;
                }
                
                context.SourceFilePath = cmdargs["i"];
                context.DestinationFilePath = cmdargs["o"];

                if (!Equals(cmdargs["h"], null))
                {
                    context.KeepHeaders = true;

                    try
                    {
                        context.HeaderLineCount = Convert.ToInt32(cmdargs["h"]);
                    }
                    catch (Exception)
                    {
                        context.HeaderLineCount = 1;
                    }
                }

                if (!string.IsNullOrEmpty(cmdargs["filepattern"]))
                {
                    context.UseFileNamePattern = true;
                    context.FileNamePattern = cmdargs["filepattern"];
                }
                else
                {
                    context.FileNamePattern = Common.DefaultFilePattern;
                }

                if (!string.IsNullOrEmpty(cmdargs["splitstrategy"]))
                {
                    string strategyRaw = cmdargs["splitstrategy"];

                    ProcessStragegySwitch(strategyRaw);
                }
            }
        }

        private static void HideConsole()
        {
            IntPtr hWnd = FindWindow(null, "TextFileSplitterConsole.exe");

            if (hWnd != IntPtr.Zero)
            {
                // Hide the window
                ShowWindow(hWnd, 0); // 0 = SW_HIDE
            }
        }

        private static void ProcessStragegySwitch(string strategyRaw)
        {
            ISplitStrategy currentStrategy = null;
            string[] parts = strategyRaw.Split(':');

            var engineShell = new Shell();

            foreach (var strategy in engineShell.Strategies)
            {
                if (strategy.MatchCommandLineSwitch(parts[0]))
                {
                    currentStrategy = strategy;
                }
            }

            if (context.SourceIsDirectory)
            {
                CreateFileListToProcess();
            }
            else
            {
                FilesToProcess.Add(context.SourceFilePath);
            }

            if (!Equals(currentStrategy, null))
            {
                currentStrategy.Context = context;
                currentStrategy.Context.FilePatternTokens = engineShell.Tokens;

                if (!string.IsNullOrEmpty(parts[1]))
                {
                    currentStrategy.CommandLineMainArgument = parts[1];
                }

                string[] pieces = ArgumentsToArray(cmdargs);

                currentStrategy.ProcessExtraCommandLineSwitches(pieces);

                if (currentStrategy.CommandLineIsValid)
                {
                    Console.WriteLine("Start of file splitting operations...");

                    string currStategyName = string.Format("Using {0}", currentStrategy.StrategyName);
                    Console.WriteLine(currStategyName);

                    foreach (var currFile in FilesToProcess)
                    {
                        currentStrategy.Context.FileEncodingMetadata = Common.GetFileEncoding(currFile);

                        currentStrategy.Context.SourceFilePath = currFile;
                        currentStrategy.Context.InitContext(currFile);

                        string currFileLine = string.Format("Processing {0}...", currFile);

                        Console.WriteLine(currFileLine);

                        currentStrategy.Start();
                    }

                    Console.WriteLine("Finished file splitting operations!");
                }
            }
        }

        private static void CreateFileListToProcess()
        {
            FilesToProcess.AddRange(Directory.GetFiles(context.SourceFilePath));
        }

        private static string[] ArgumentsToArray(Arguments switches)
        {
            var parts = new string[switches.Count];

            for (int i = 0; i < switches.Count; i++)
            {
                parts[i] = switches[i];
            }

            return parts;
        }

        private static void ShowUsage()
        {
            string path = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            string fullName = Path.Combine(path, "readme.txt");

            string help = File.ReadAllText(fullName);

            TextWriter helpWriter = Console.Out;

            helpWriter.WriteLine(@help);

            helpWriter.Flush();
        }
    }
}