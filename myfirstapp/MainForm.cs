using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using TrackMethods;

namespace ParentControlApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Process[] processes = Process.GetProcesses();
            string targetProcessName = "Notepad";
            foreach (Process process in processes)
            {
                textBox1.Text += $"{process} запущен!";
                //if (process.ProcessName.Equals(targetProcessName, StringComparison.OrdinalIgnoreCase))
                //{
                //    textBox1.Text += ($"{targetProcessName} запущен!");
                //    break;
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string processName = "Notepad";

            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                try
                {
                    process.CloseMainWindow();
                    if (!process.WaitForExit(5000))
                    {
                        process.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при закрытии процесса {process.ProcessName}: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var a = new Dictionary<string, string>()
            {
                {"Notepad", "1000" }
            };

            TrackMethods.User user = new TrackMethods.User(a, "Ivan", "1234");
            TrackMethods.UsersCollection users = new TrackMethods.UsersCollection();
            users.AddUser(user);
        }
    }
}
