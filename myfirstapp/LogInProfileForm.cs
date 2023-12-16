using System;
using System.Windows.Forms;

namespace myfirstapp
{
    public partial class LogInProfileForm : Form
    {
        public string password;
        public bool is_correct = false;
        public LogInProfileForm(string password)
        {
            InitializeComponent();
            this.password = password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Вы не ввели пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(textBox1.Text != password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox1.Text == password) 
            {
                is_correct = true;
                Close();
            }
        }
    }
}
