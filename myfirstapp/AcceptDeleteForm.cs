using System;
using System.Windows.Forms;

namespace ParentControlApp
{
    public partial class AcceptDeleteForm : Form
    {
        public bool users_choice = false;
        public AcceptDeleteForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            users_choice = true;
            Close();
        }
    }
}
