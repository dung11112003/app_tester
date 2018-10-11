using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private OpenFileDialog openCPP = new OpenFileDialog();

        public List<fileInQueue> queue = new List<fileInQueue>();

        public class fileInQueue
        {
            public int Index
            {
                get;
                set;
            }
            public string Path
            {
                get;
                set;
            }
            public string Test
            {
                get;
                set;
            }
            public string Status
            {
                get;
                set;
            }
        }

        private void InitializeOpenCPP()
        {
            this.openCPP.Multiselect = true;
            this.openCPP.Filter = "C++ files (.cpp)|*.cpp|All files (*.*)|*.*";
            this.openCPP.Title = "Choose C++ files";
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeOpenCPP();
        }

        private void mainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainTab.SelectedIndex == 3)
            {
                MessageBoxResult exitResult = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.OKCancel);
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
            if (openCPP.ShowDialog() == true)
            {
                foreach (string nameOfFiles in openCPP.FileNames)
                {
                    queue.Add(new fileInQueue()
                    {
                        Index = queue.Count() + 1,
                        Path = System.IO.Path.GetFileName(nameOfFiles),
                        Test = "",
                        Status = "In queue"
                    });
                }
                listTestingQueue.ItemsSource = queue;
                listTestingQueue.Items.Refresh();
            }
        }
    }
}
