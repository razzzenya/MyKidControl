using System;
using System.Windows.Forms;
using MainLibrary;

namespace ParentControlApp
{
    public partial class SuserinputpasswordForm : Form
    {
        public SuperUser suser = new SuperUser();
        public bool is_password_correct {  get; set; }
        public SuserinputpasswordForm(SuperUser suser)
        {
            InitializeComponent();
            this.suser = suser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (suser.SuperuserPassword != textBox1.Text)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                is_password_correct = true;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            is_password_correct = false;
            Close();
        }
    }
}
