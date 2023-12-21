using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using MainLibrary;

namespace ParentControlApp
{
    public partial class MainForm : Form
    {
        public SuperUser suser = new SuperUser();
        public User current_user = new User();
        public bool is_password_entered = false;
        public bool is_profile_chosen = false;
        public bool is_processing = false;
        private readonly System.Timers.Timer timer = new System.Timers.Timer(1000);
        private NotifyIcon notifyIcon;
        private readonly Logger logger = new Logger();
        private bool is_need_to_notify = true;
        private UsersCollection users = new UsersCollection();
        private int current_user_index;
        private DateTime start;

        public MainForm()
        {
            InitializeComponent();
            if (!suser.LoadFromFile())
            {
                MessageBox.Show("Файл суперпользователя был удалён!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.LogDeletedSuserFile();
                NewSuserPasswordForm form = new NewSuserPasswordForm();
                form.ShowDialog();
                if (!form.is_created)
                {
                    Close();
                }
                users.LoadFromFile();
                suser.LoadFromFile();
                InitializeListView();
                InitializeTrayIcon();
                logger.LogStartApp();
            }
            else
            {
                users.LoadFromFile();
                suser.LoadFromFile();
                InitializeListView();
                InitializeTrayIcon();
                logger.LogStartApp();
            }
        }

        private void InitializeTrayIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Visible = false;
            notifyIcon.Icon = Properties.Resources.icon;
            notifyIcon.Text = "Parent control app";
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
        }

        private void InitializeListView()
        {
            processListView.Clear();
            processListView.View = View.Details;
            processListView.Columns.Add("Имя процесса", 150);
            processListView.Columns.Add("Оставшееся время", 150);
        }

        private void UpdateListView(string processName)
        {
            if (processListView.InvokeRequired)
            {
                processListView.Invoke(new Action<string>(UpdateListView), processName);
            }
            else
            {
                ListViewItem item = processListView.FindItemWithText(processName);
                if (item == null)
                {
                    item = new ListViewItem(processName);
                    item.SubItems.Add(current_user.Processes[processName]);
                    processListView.Items.Add(item);
                }
                else
                {
                    item.SubItems[1].Text = current_user.Processes[processName];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (is_processing)
            {
                MessageBox.Show("Завершите сессию для работы с профилями!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ProfilesForm form  = new ProfilesForm(is_password_entered);
            form.ShowDialog();
            if (form.is_any_chosen)
            {
                if (timer.Enabled)
                {
                    logger.LogEndSession(current_user.Name);
                    timer.Stop();
                }
                users.LoadFromFile();
                is_need_to_notify = true;
                current_user = form.users.Users[form.selected_user_index];
                current_user_index = form.selected_user_index;
                is_profile_chosen = true;
                label2.Text = "Профиль: " + current_user.Name;
                processListView.Items.Clear();
                foreach (var process in current_user.Processes)
                {
                    ListViewItem item = new ListViewItem(new[] { process.Key, process.Value });
                    processListView.Items.Add(item);
                }
            }
        }

        private void войтиКакСуперпользовательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!is_password_entered)
            {
                SuserinputpasswordForm form = new SuserinputpasswordForm(suser);
                form.ShowDialog();
                if (form.is_password_correct)
                {
                    is_password_entered = true;
                    logger.LogSuserLogIn();
                }
                else
                {
                    is_password_entered = false;
                }
            }
            else
            {
                MessageBox.Show("Пароль уже введен","", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(is_password_entered)
            {
                is_password_entered=false;
                logger.LogSuserLogOut();
                MessageBox.Show("Вы успешно вышли", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Вы не вошли как суперпользователь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (!is_profile_chosen)
            {
                MessageBox.Show("Профиль не был выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (is_processing){
                MessageBox.Show("Сессия уже запущена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                logger.LogStartSession(current_user.Name);
                is_processing = true;
                start = DateTime.Now;
                StartTracking();
            }
        }

        private void StartTracking()
        {
            timer.Elapsed -= TimerElapsed;
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var activeProcesses = Process.GetProcesses().Select(p => p.ProcessName).ToList();

            foreach (var processName in current_user.Processes.Keys.ToList())
            {
                if (activeProcesses.Contains(processName))
                {
                    var timeSpan = TimeSpan.Parse(current_user.Processes[processName]);
                    if(timeSpan <= TimeSpan.Zero)
                    {
                        CloseProcess(processName);
                        break;
                    }
                    timeSpan = timeSpan.Subtract(TimeSpan.FromSeconds(1));
                    UpdateListView(processName);
                    current_user.Processes[processName] = timeSpan.ToString();

                    if (timeSpan.TotalSeconds <= 0)
                    {
                        CloseProcess(processName);
                    }
                }
            }

            if (current_user.Processes.All(p => TimeSpan.Parse(p.Value).TotalSeconds <= 0))
            {
                if (is_need_to_notify)
                {
                    NotifyUser();
                }
            }
        }

        public void NotifyUser()
        {
            users.Users[current_user_index].TimeStatistics[start] = DateTime.Now - start;
            users.SaveToFile();
            is_need_to_notify = false;
            is_processing = false;
            logger.LogThatUserNotified(current_user.Name);
            MessageBox.Show("Ваше допустимое время для всех приложений закончилось", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void CloseProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }

        private void SuserActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!is_password_entered)
            {
                MessageBox.Show("Для начала войдите как суперпользователь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                SuserActionsForm form = new SuserActionsForm(suser.SuperuserPassword);
                form.ShowDialog();
                if (form.is_changed)
                {
                    suser.SuperuserPassword = form.password;
                    suser.SaveToFile();
                }
            }
        }

        private void End_Click(object sender, EventArgs e)
        {
            if (!is_processing && !timer.Enabled)
            {
                MessageBox.Show("Вы еще не начали сессию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!is_processing && timer.Enabled)
            {
                is_profile_chosen = false;
                is_need_to_notify = true;
                timer.Stop();
                logger.LogEndSessionWhenTimeIsGone(current_user.Name);
                current_user = null;
                InitializeListView();
                label2.Text = "Выберите профиль";
                MessageBox.Show("Сессия была закончена", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!is_profile_chosen)
            {
               MessageBox.Show("Вы не выбрали пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            users.Users[current_user_index].TimeStatistics[start] = DateTime.Now - start;
            users.SaveToFile();
            is_processing = false;
            is_profile_chosen = false;
            is_need_to_notify = true;
            timer.Stop();
            logger.LogForciblyEndSession(current_user.Name);
            current_user = null;
            InitializeListView();
            label2.Text = "Выберите профиль";
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowFromTray();
            }
        }

        private void HideToTray()
        {
            Hide();
            notifyIcon.Visible = true;
        }

        private void ShowFromTray()
        {
            Show();
            notifyIcon.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void спрятатьВТрейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideToTray();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_processing || timer.Enabled)
            {
                e.Cancel = true;
                HideToTray();
            }
            else if(!is_processing && !timer.Enabled)
            {
                logger.LogExitApp();
            }
        }
    }
}
