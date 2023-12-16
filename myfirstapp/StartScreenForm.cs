using System;
using System.IO;
using System.Windows.Forms;

namespace ParentControlApp
{
    public partial class StartScreenForm : Form
    {
        public StartScreenForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PasswordForm passwordWindow = new PasswordForm();
            passwordWindow.FormClosed += PasswordWindow_FormClosed;
            passwordWindow.Show();

            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "logs.dat");
            if (File.Exists(flagFilePath))
            {
                Close();
            }
        }

        private void PasswordWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

    }
}
