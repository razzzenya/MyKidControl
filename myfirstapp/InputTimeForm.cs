using System;
using System.Windows.Forms;

namespace myfirstapp
{
    public partial class InputTimeForm : Form
    {
        public TimeSpan Time;
        public bool is_chosen = false;
        public InputTimeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0 && numericUpDown2.Value == 0 && numericUpDown3.Value == 0)
            {
                MessageBox.Show("Вы не ввели время!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            is_chosen = true;
            Time = new TimeSpan((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            Close();
        }
    }
}
