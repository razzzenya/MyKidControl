using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ParentControlApp
{
    public partial class ChangeProcessesForm : Form
    {
        public bool is_any_changes = false;
        public Dictionary<string, string> processes = new Dictionary<string, string>();
        public ChangeProcessesForm(Dictionary<string, string> processes)
        {
            InitializeComponent();
            this.processes = processes;
            InitializeProcessesList();
            
        }

        private void InitializeProcessesList()
        {
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Процесс", 180);
            listView1.Columns.Add("Время", 180);
            if (processes.Count > 0)
            {
                foreach (var process in processes)
                {
                    ListViewItem item = new ListViewItem(new[] { process.Key, process.Value });
                    listView1.Items.Add(item);
                }
            }

        }

        private void UpdateProcessesList()
        {
            listView1.Items.Clear();
            if (processes.Count > 0)
            {
                foreach (var process in processes)
                {
                    ListViewItem item = new ListViewItem(new[] { process.Key, process.Value });
                    listView1.Items.Add(item);
                }
            }
        }

        static string SelectExeFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Исполняемые файлы (*.exe)|*.exe";
            openFileDialog.Title = "Выберите исполняемый файл";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string processName = Path.GetFileNameWithoutExtension(filePath);
                return processName;
            }

            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string processName = SelectExeFilePath();
            string processNameout;
            if (!string.IsNullOrEmpty(processName) && processes.TryGetValue(processName, out processNameout))
            {
                MessageBox.Show("Этот процесс уже добавлен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(processName))
            {
                return;
            }
            TimeSpan time = new TimeSpan();
            InputTimeForm form = new InputTimeForm();
            form.ShowDialog();
            if (form.is_chosen)
            {
                time = form.Time;
            }
            else if (!form.is_chosen)
            {
                return;
            }
            processes.Add(processName, time.ToString());
            is_any_changes = true;
            UpdateProcessesList();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Enabled = listView1.SelectedItems.Count > 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string key = selectedItem.SubItems[0].Text;
                if (processes.ContainsKey(key))
                {
                    processes.Remove(key);
                }
                listView1.Items.Remove(selectedItem);
                is_any_changes = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            is_any_changes = false;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (processes.Count == 0)
            {
                MessageBox.Show("Вы не добавили ни одного процесса!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Close();
        }
    }
}
