using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace WavHelper
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static string RunCommandAndGetOutput(string programName, string args, bool includeStdErr)
        {
            Console.WriteLine("Running command: " + programName + " " + args);
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            var process = Process.Start(processStartInfo);
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (includeStdErr)
                output = output + "\r\n" + err;
            return output;
        }

        public static void RunCommand(string programName, string args)
        {
            Console.WriteLine("Running command: " + programName + " " + args);
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programName,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            var process = Process.Start(processStartInfo);
            
        }
    }

    public class SoundReference : IComparable<SoundReference>
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string LineBefore { get; set; }
        public string Line { get; set; }
        public string LineAfter { get; set; }

        public int CompareTo(SoundReference other)
        {
            return Name.CompareTo(other.Name);
        }

        public string GetInfo()
        {
            string retVal = 
            "Name:" + Name + "\r\n" +
            "Alias:" + Alias + "\r\n" +
            "FileName:" + FileName + "\r\n" +
            //"LineNumber:" + LineNumber + "\r\n" +
            "LineBefore:" + LineBefore + "\r\n" +
            "Line:" + Line + "\r\n" +
            "LineAfter:" + LineAfter + "\r\n";
            return retVal;
        }
    }
}
