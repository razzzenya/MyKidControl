using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using ParentControlApp;
using MainLibrary;

namespace ParentControlApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");

            if (Directory.Exists(directoryPath))
            {
                Application.Run(new MainForm());
            }
            else
            {
                Application.Run(new StartScreenForm());
                if (Directory.Exists(directoryPath))
                {
                    Application.Run(new MainForm());
                }
            }
        }
    }
}
