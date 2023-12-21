using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MainLibrary;

namespace ParentControlApp
{
    public partial class StatisticsForm : Form
    {
        UsersCollection users = new UsersCollection();
        public int selected_user_index;
        public DateTime chosen_date = new DateTime();
        public bool chosen_mode = false;
        public StatisticsForm()
        {
            InitializeComponent();
            users.LoadFromFile();
            InitializeProfilesList();
            chart1.Series.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void InitializeProfilesList()
        {
            comboBox1.Items.Clear();

            foreach (User user in users.Users)
            {
                comboBox1.Items.Add(user.Name);
            }
        }

        private void BuildWeekChart(DateTime[] daysOfWeek, TimeSpan[] timeSpans)
        {
            chart1.Series.Clear();
            Series series = new Series("Время активности (часы)");
            series.ChartType = SeriesChartType.Column;

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                DataPoint point = new DataPoint(i, timeSpans[i].TotalHours);
                point.AxisLabel = daysOfWeek[i].ToString("dd.MM");
                series.Points.Add(point);
            }

            chart1.Series.Add(series);

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Title = "День недели";
            chart1.ChartAreas[0].AxisY.Title = "Время (часы)";

            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = timeSpans.Max().Hours + 1;
            chart1.Invalidate();
        }

        private void BuildMonthChart(DateTime[] daysOfMonth, TimeSpan[] timeSpans)
        {
            chart1.Series.Clear();
            Series series = new Series("Время активности (часы)");
            series.ChartType = SeriesChartType.Column;

            for (int i = 0; i < daysOfMonth.Length; i++)
            {
                DataPoint point = new DataPoint(i, timeSpans[i].TotalHours);
                point.AxisLabel = daysOfMonth[i].ToString("dd.MM");
                series.Points.Add(point);
            }

            chart1.Series.Add(series);

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.Title = "День месяца";
            chart1.ChartAreas[0].AxisY.Title = "Время (часы)";

            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = timeSpans.Max().Hours + 1;
            chart1.Invalidate();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            chosen_date = dateTimePicker1.Value.Date;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chosen_mode = checkBox1.Checked;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected_user_index = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!chosen_mode)
            {
                DateTime startOfWeek = new DateTime();
                DateTime endOfWeek = new DateTime();
                if (chosen_date.DayOfWeek == DayOfWeek.Sunday)
                {
                    startOfWeek = chosen_date.AddDays(-6);
                    endOfWeek = chosen_date;
                }
                else
                {
                    startOfWeek = chosen_date.AddDays(-(int)chosen_date.DayOfWeek + (int)DayOfWeek.Monday);
                    endOfWeek = startOfWeek.AddDays(6);
                }

                DateTime[] daysOfWeek = Enumerable.Range(0, 7).Select(i => startOfWeek.AddDays(i)).ToArray();
                TimeSpan[] timeSpans = new TimeSpan[daysOfWeek.Length];

                foreach (var day in users.Users[selected_user_index].TimeStatistics.Keys)
                {
                    int index = (int)day.DayOfWeek;
                    if (index == 0)
                    {
                        if (day.Date == daysOfWeek[6])
                        {
                            timeSpans[6] += users.Users[selected_user_index].TimeStatistics[day];
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (day.Date == daysOfWeek[(int)day.DayOfWeek - 1])
                    {
                        timeSpans[(int)day.DayOfWeek - 1] += (users.Users[selected_user_index].TimeStatistics[day]);
                    }
                }
                BuildWeekChart(daysOfWeek, timeSpans);
            }
            else
            {
                DateTime startOfMonth = new DateTime(chosen_date.Year, chosen_date.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                DateTime[] daysOfMonth = Enumerable.Range(0, endOfMonth.Day).Select(i => startOfMonth.AddDays(i)).ToArray();
                TimeSpan[] timeSpans = new TimeSpan[daysOfMonth.Length];
                foreach (var day in users.Users[selected_user_index].TimeStatistics.Keys)
                {
                    if(day.Month == startOfMonth.Month)
                    {
                        timeSpans[day.Day - 1] += (users.Users[selected_user_index].TimeStatistics[day]);
                    }
                }
                BuildMonthChart(daysOfMonth, timeSpans);
            }
        }

    }
}
