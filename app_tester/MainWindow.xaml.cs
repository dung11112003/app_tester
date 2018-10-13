using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            if (mainTab.SelectedIndex == 3)
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

        public void addFiles(object sender, RoutedEventArgs e)
        {
            if (Variables.openCPP.ShowDialog() == true)
            {
                foreach (string nameOfFiles in Variables.openCPP.FileNames)
                {
                    Variables.queue.Add(new Variables.fileInQueue()
                    {
                        Index = Variables.queue.Count() + 1,
                        Path = System.IO.Path.GetFileName(nameOfFiles),
                        Test = "",
                        Status = "In queue"
                    });
                }
                listTestingQueueBox.ItemsSource = Variables.queue;
                listTestingQueueBox.Items.Refresh();
            }
        }

        public void chooseMappingFolder(object sender, RoutedEventArgs e)
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
                            IO = subDir.GetDirectories().Length.ToString(),
                        });
                    }
                    testListBox.ItemsSource = Variables.test_list;
                    testListBox.Items.Refresh();
                }
            }
        }
    }
}
