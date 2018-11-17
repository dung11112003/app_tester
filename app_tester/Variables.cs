using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;

namespace app_tester
{
    class Variables
    {
        public static Microsoft.Win32.OpenFileDialog openCPP = new Microsoft.Win32.OpenFileDialog();

        public static FolderBrowserDialog openFolder = new FolderBrowserDialog();

        public static DirectoryInfo testFolderPath;

        public static List<fileInQueue> queue = new List<fileInQueue>();

        public static List<listOfTest> test_list = new List<listOfTest>();

        public class fileInQueue
        {
            public int Index
            {
                get;
                set;
            }
            public string Name
            {
                get;
                set;
            }
            public string FullName
            {
                get;
                set;
            }
            public listOfTest Test
            {
                get;
                set;
            }
            public string Status
            {
                get;
                set;
            }
            public string InitDirectory
            {
                get;
                set;
            }
            public string FullPath
            {
                get;
                set;
            }
        }

        public class listOfTest
        {
            public int TestIndex
            {
                get;
                set;
            }
            public string TestName
            {
                get;
                set;
            }
            public List<DirectoryInfo> List
            {
                get;
                set;
            }
            public string IO
            {
                get;
                set;
            }
        }
    }
}
