using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;

namespace app_tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void InitializeOpenCPP()
        {
            Variables.openCPP.Multiselect = true;
            Variables.openCPP.Filter = "C++ files (.cpp)|*.cpp|All files (*.*)|*.*";
            Variables.openCPP.Title = "Choose C++ files";
        }

        private void InitializeOpenFolder()
        {
            Variables.openFolder.Description = "Choose folder:";
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeOpenCPP();
            InitializeOpenFolder();
        }

        private void mainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainTab.SelectedIndex == 5)
            {
                MessageBoxResult exitResult = System.Windows.MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.OKCancel);
                switch (exitResult)
                {
                    case MessageBoxResult.OK:
                        this.Close();
                        break;
                    case MessageBoxResult.Cancel:
                        mainTab.SelectedIndex = 0;
                        return;

                }
            }
        }

        private void auto_detect_check(object sender, RoutedEventArgs e)
        {

        }

        private Variables.listOfTest testRecornize(string fileName, DirectoryInfo PathOfFolder)
        {
            if (PathOfFolder != null)
            {
                for (int i = 0; i < PathOfFolder.GetDirectories().Length; i++)
                {
                    if (fileName == PathOfFolder.GetDirectories()[i].Name + ".cpp")
                    {
                        return new Variables.listOfTest()
                        {
                            TestIndex = i + 1,
                            TestName = PathOfFolder.GetDirectories()[i].Name,
                            IO = PathOfFolder.GetDirectories()[i].GetDirectories().Length.ToString(),
                        };
                    }
                }
            }
            return new Variables.listOfTest()
            {
                TestIndex = -1,
                TestName = string.Empty,
                IO = string.Empty,
            };
        }

        private void addFiles(object sender, RoutedEventArgs e)
        {
            if (Variables.openCPP.ShowDialog() == true)
            {
                foreach (string pathOfFiles in Variables.openCPP.FileNames)
                {
                    Variables.queue.Add(new Variables.fileInQueue()
                    {
                        Index = Variables.queue.Count() + 1,
                        Name = Path.GetFileNameWithoutExtension(pathOfFiles),
                        FullName = Path.GetFileName(pathOfFiles),
                        Test = testRecornize(Path.GetFileName(pathOfFiles), Variables.testFolderPath),
                        Status = "In queue",
                        InitDirectory = Variables.openCPP.InitialDirectory,
                        FullPath = pathOfFiles,
                    });
                }
                listTestingQueueBox.ItemsSource = Variables.queue;
                listTestingQueueBox.Items.Refresh();
            }
        }

        private void chooseMappingFolder(object sender, RoutedEventArgs e)
        {
            if (Variables.openFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Variables.testFolderPath = new DirectoryInfo(Variables.openFolder.SelectedPath);
                mappingFolder.Text = Variables.openFolder.SelectedPath;

                if (Variables.testFolderPath.Exists)
                {
                    Variables.test_list.Clear();
                    foreach (DirectoryInfo subDir in Variables.testFolderPath.GetDirectories())
                    {
                        Variables.test_list.Add(new Variables.listOfTest()
                        {
                            TestIndex = Variables.test_list.Count() + 1,
                            TestName = subDir.Name,
                            List = subDir.GetDirectories().ToList(),
                            IO = subDir.GetDirectories().Length.ToString(),
                        });
                    }
                    testListBox.ItemsSource = Variables.test_list;
                    testListBox.Items.Refresh();
                }
            }
        }

        private void queueRefresh(object sender, RoutedEventArgs e)
        {
            foreach (Variables.fileInQueue part in Variables.queue)
            {
                part.Test = testRecornize(part.Name, Variables.testFolderPath);
            }

            listTestingQueueBox.ItemsSource = Variables.queue;
            listTestingQueueBox.Items.Refresh();
        }
    }
}
