using System;
using System.Windows.Forms;
using MainLibrary;

namespace ParentControlApp
{
    public partial class PasswordForm : Form
    {
        readonly Logger logger = new Logger();
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string first_password = textBox1.Text;
            string second_password = textBox2.Text;
            if (first_password == second_password)
            {
                SuperUser new_suser = new SuperUser();
                new_suser.SuperuserPassword = first_password;
                logger.LogNewSuperuserPassword(first_password, first_password);
                new_suser.SaveToFile();
                Close();
            }
            else if(string.IsNullOrEmpty(first_password) || string.IsNullOrEmpty(second_password))
            {
                MessageBox.Show("Вы не до конца заполнили поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
