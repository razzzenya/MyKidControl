using System;
using System.IO;
using System.Windows.Forms;

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
