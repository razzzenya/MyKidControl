using System;
using System.Windows.Forms;

namespace myfirstapp
{
    public partial class ChangeUserPasswordForm : Form
    {
        public string old_password;
        public string new_password;
        public ChangeUserPasswordForm(string old_password)
        {
            InitializeComponent();
            this.old_password = old_password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Вы не ввели старый пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Вы полностью не заполнили поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(textBox1.Text != old_password)
            {
                MessageBox.Show("Старый пароль не совпадает!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Новые пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (old_password == textBox2.Text)
            {
                MessageBox.Show("Вы ввели тот же самый пароль, что и старый!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            new_password = textBox2.Text;
            Close();
        }
    }
}
