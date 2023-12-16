using System.Collections.Generic;
using System.Windows.Forms;

namespace ParentControlApp
{
    public partial class LogsForm : Form
    {
        public List<string> Logs = new List<string>();
        public LogsForm(List<string>Logs)
        {
            InitializeComponent();
            this.Logs = Logs;
            foreach (string log in Logs)
            {
                richTextBox1.AppendText(log);
            }
        }
    }
}
