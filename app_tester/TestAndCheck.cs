using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_tester
{
    public partial class MainWindow : Window
    {
        private void startTesting(object sender, RoutedEventArgs e)
        {
            List<string> commands = new List<string>();
            foreach (Variables.fileInQueue part in Variables.queue)
            {
                foreach (DirectoryInfo test in part.Test.List)
                {
                    commands.Add("cd " + test.FullName);
                    commands.Add("g++ -g " + part.FullPath + " -o \\" + part.Name);

                }
            }
        }
    }
}
