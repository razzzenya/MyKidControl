using System;
using System.Windows.Forms;

namespace myfirstapp
{
    public partial class ChangeUserNameForm : Form
    {
        public string name;
        public string old_name;
        public ChangeUserNameForm(string old_name)
        {
            InitializeComponent();
            this.old_name = old_name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Вы не ввели имя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(textBox1.Text == old_name)
            {
                MessageBox.Show("Вы ввели тоже самое имя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            name = textBox1.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
