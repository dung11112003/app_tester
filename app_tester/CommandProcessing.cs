using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace app_tester
{
    public partial class MainWindow : Window
    {
        private string commandOutput(List<string> cmdCommand)
        {
            ProcessStartInfo cmdProcess = new ProcessStartInfo("cmd");
            cmdProcess.UseShellExecute = false;
            cmdProcess.RedirectStandardOutput = true;
            cmdProcess.CreateNoWindow = true;
            cmdProcess.RedirectStandardInput = true;
            var process = Process.Start(cmdProcess);
            foreach (string str in cmdCommand)
            {
                process.StandardInput.WriteLine(str);
            }
            process.StandardInput.WriteLine("exit");

            string output = process.StandardOutput.ReadToEnd();

            return output;
        }
    }
}
