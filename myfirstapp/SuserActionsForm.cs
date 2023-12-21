using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MainLibrary;

namespace ParentControlApp
{
    public partial class SuserActionsForm : Form
    {
        public string password;
        public Logger logger = new Logger();
        public bool is_changed = false;
        public SuserActionsForm(string password)
        {
            InitializeComponent();
            this.password = password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeUserPasswordForm form = new ChangeUserPasswordForm(password);
            form.ShowDialog();
            if (!string.IsNullOrEmpty(form.new_password))
            {
                logger.LogNewSuperuserPassword(password, form.new_password);
                password = form.new_password;
                is_changed = true;
                MessageBox.Show("Пароль суперпользователя был успешно изменен", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> Logs = logger.LoadLogsFromFile();
            LogsForm form = new LogsForm(Logs);
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            logger.ClearLogs();
            MessageBox.Show("Файл с логами был успешно очищен", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StatisticsForm form = new StatisticsForm();
            form.ShowDialog();
        }
    }
}
