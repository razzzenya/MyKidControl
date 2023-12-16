using System;
using System.IO;
using System.Windows.Forms;
using MainLibrary;
using ParentControlApp;

namespace myfirstapp
{
    public partial class ProfilesForm : Form
    {
        public bool is_any_chosen = false;
        public UsersCollection users = new UsersCollection();
        public bool is_password_entered = false;
        public int selected_user_index;
        private Logger logger = new Logger();

        public ProfilesForm(bool is_password_entered)
        {
            InitializeComponent();
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ParentControl");
            string flagFilePath = Path.Combine(directoryPath, "users.dat");
            if (File.Exists(flagFilePath))
            {
                users.LoadFromFile();
            }
            this.is_password_entered = is_password_entered;
            UpdateProfilesList();
        }

        private void UpdateProfilesList()
        {
            comboBox1.Items.Clear();

            foreach (User user in users.Users)
            {
                comboBox1.Items.Add(user.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!is_password_entered)
            {
                MessageBox.Show("Необходимо ввести пароль суперпользователя", "Нет доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                CreateUserForm form = new CreateUserForm();
                form.ShowDialog();
                if (form.is_created)
                {
                    users.AddUser(form.user);
                    users.SaveToFile();
                    logger.LogCreatingUser(form.user.Name);
                    comboBox1.DataSource = null;
                    comboBox1.DataSource = users.Users;
                    comboBox1.DisplayMember = "Name";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_user_index = comboBox1.SelectedIndex;
            nameLabel.Text = users.Users[selected_user_index].Name;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (users.Users.Count == 0)
            {
                MessageBox.Show("Список профилей пуст", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LogInProfileForm form = new LogInProfileForm(users.Users[selected_user_index].Password);
            form.ShowDialog();
            if (form.is_correct)
            {
                logger.LogLogIn(users.Users[selected_user_index].Name);
                is_any_chosen = true;
                Close();
            }
        }

        private void ChangeName_Click(object sender, EventArgs e)
        {
            if (!is_password_entered)
            {
                MessageBox.Show("Необходимо ввести пароль суперпользователя", "Нет доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if(users.Users.Count == 0)
            {
                MessageBox.Show("Нет профиля для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ChangeUserNameForm form = new ChangeUserNameForm(users.Users[selected_user_index].Name);
                form.ShowDialog();
                if (!string.IsNullOrEmpty(form.name))
                {
                    logger.LogUsernameChanged(users.Users[selected_user_index].Name, form.name);
                    users.Users[selected_user_index].Name = form.name;
                    users.SaveToFile();
                    UpdateProfilesList();
                }
            }
        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            if (!is_password_entered)
            {
                MessageBox.Show("Необходимо ввести пароль суперпользователя", "Нет доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (users.Users.Count == 0)
            {
                MessageBox.Show("Нет профиля для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ChangeUserPasswordForm form = new ChangeUserPasswordForm(users.Users[selected_user_index].Password);
                form.ShowDialog();
                if (!string.IsNullOrEmpty(form.new_password))
                {
                    logger.LogUserPasswordChanged(users.Users[selected_user_index].Name, users.Users[selected_user_index].Password, form.new_password);
                    users.Users[selected_user_index].Password = form.new_password;
                    users.SaveToFile();
                    UpdateProfilesList();
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (!is_password_entered)
            {
                MessageBox.Show("Необходимо ввести пароль суперпользователя", "Нет доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            else if (users.Users.Count == 0)
            {
                MessageBox.Show("Нечего удалять", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AcceptDeleteForm form = new AcceptDeleteForm();
            form.ShowDialog();
            if (form.users_choice)
            {
                users.Users.Remove(users.Users[selected_user_index]);
                logger.LogDeletingUser(users.Users[selected_user_index].Name);
                users.SaveToFile();
                UpdateProfilesList();
            }
        }

        private void ChangeProcesses_Click(object sender, EventArgs e)
        {
            if (!is_password_entered)
            {
                MessageBox.Show("Необходимо ввести пароль суперпользователя", "Нет доступа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            else if (users.Users.Count == 0)
            {
                MessageBox.Show("Нет профиля для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ChangeProcessesForm form = new ChangeProcessesForm(users.Users[selected_user_index].Processes);
            form.ShowDialog();
            if (form.is_any_changes)
            {
                users.Users[selected_user_index].Processes = form.processes;
                users.SaveToFile(); 
                UpdateProfilesList();
                logger.LogUserProcessesChanged(users.Users[selected_user_index].Name);
            }
        }
    }
}
