using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Diplom
{
    public partial class ToDoList : Form
    {
        int UserID;
        string connectionString = @"Data Source=DESKTOP-RLF89VU\MYSERVER; Integrated Security=SSPI; Initial Catalog=diplom;";
        public ToDoList(int userid)
        {
            InitializeComponent();
            UserID = userid;
        }
   
        public void Initializate()
        {
            dataGridView1.Columns.Clear();
            SqlConnection connect1 = new SqlConnection(connectionString);
            string query = $"Select Category,ID,Title,Description,DateStart,DateEnd,Status FROM AllTasks Where UserID = {UserID} and Status = 'False'";
            dataGridView1.Columns.Add("Category", "Category");
            dataGridView1.Columns.Add("ID", "№");
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns.Add("Description", "Description");
            dataGridView1.Columns.Add("DateStart", "DateStart");
            dataGridView1.Columns.Add("DateEnd", "DateEnd");
            dataGridView1.Columns.Add("Status", "Status");

            SqlCommand cmd = new SqlCommand(query, connect1);
            connect1.Open();
            SqlDataReader reader1 = cmd.ExecuteReader();
            while (reader1.Read())
            {
                dataGridView1.Rows.Add(reader1[0].ToString(), reader1[1].ToString(), reader1[2].ToString(), reader1[3].ToString(), reader1[4].ToString(), reader1[5].ToString(),reader1[6].ToString());

            }
            reader1.Close();
            connect1.Close();
        }
        public void InitializateSpisokOne()
        {
            dataGridView1.Columns.Clear();
            SqlConnection connect1 = new SqlConnection(connectionString);
            string query = $"Select Category,ID,Title,Description,DateStart,DateEnd,Status FROM AllTasks Where UserID = {UserID} and Status = 'True'";
            dataGridView1.Columns.Add("Category", "Category");
            dataGridView1.Columns.Add("ID", "№");
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns.Add("Description", "Description");
            dataGridView1.Columns.Add("DateStart", "DateStart");
            dataGridView1.Columns.Add("DateEnd", "DateEnd");
            dataGridView1.Columns.Add("Status", "Status");

            SqlCommand cmd = new SqlCommand(query, connect1);
            connect1.Open();
            SqlDataReader reader1 = cmd.ExecuteReader();
            while (reader1.Read())
            {
                dataGridView1.Rows.Add(reader1[0].ToString(), reader1[1].ToString(), reader1[2].ToString(), reader1[3].ToString(), reader1[4].ToString(), reader1[5].ToString(), reader1[6].ToString());

            }
            reader1.Close();
            connect1.Close();
        }

        public void InitializateSpisokTwo()
        {
            dataGridView1.Columns.Clear();
            
            SqlConnection connect1 = new SqlConnection(connectionString);
            string query = $"Select Category,ID,Title,Description,DateStart,DateEnd,Status FROM AllTasks Where DateEnd < '{textBox1.Text}' and Status = 'False' and UserID = {UserID}";
            dataGridView1.Columns.Add("Category", "Category");
            dataGridView1.Columns.Add("ID", "№");
            dataGridView1.Columns.Add("Title", "Title");
            dataGridView1.Columns.Add("Description", "Description");
            dataGridView1.Columns.Add("DateStart", "DateStart");
            dataGridView1.Columns.Add("DateEnd", "DateEnd");
            dataGridView1.Columns.Add("Status", "Status");

            SqlCommand cmd = new SqlCommand(query, connect1);
            connect1.Open();
            SqlDataReader reader1 = cmd.ExecuteReader();
            while (reader1.Read())
            {
                dataGridView1.Rows.Add(reader1[0].ToString(), reader1[1].ToString(), reader1[2].ToString(), reader1[3].ToString(), reader1[4].ToString(), reader1[5].ToString(), reader1[6].ToString());

            }
            reader1.Close();
            connect1.Close();
        }
        private void ToDoList_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text = DateTime.Today.ToString();
            Initializate();
           
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = monthCalendar1.SelectionEnd.ToShortDateString();
            }
            else
            {
                textBox4.Text = monthCalendar1.SelectionEnd.ToShortDateString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection InsertConnection = new SqlConnection(connectionString);
            string Category = comboBox2.Text;
            string Title = textBox2.Text;
            string Description = textBox3.Text;
            string DateEnd = textBox4.Text;
            string DateStart = textBox1.Text;
            textBox1.Text = DateTime.Today.ToString();
                string Status = "False";
                string InsertQuery = $"Insert Into AllTasks VALUES('{UserID}','{Category}','{Title}','{Description}','{DateStart}','{DateEnd}','{Status}')";
                InsertConnection.Open();
                SqlCommand RegisterCmd = new SqlCommand(InsertQuery, InsertConnection);
                RegisterCmd.ExecuteReader();
                InsertConnection.Close();
                Initializate();
            
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
        }

        private void RedactBTN_Click(object sender, EventArgs e)
        {
            SqlConnection UpdateConnection = new SqlConnection(connectionString);
            string UpdateQuery = $"Update AllTasks set Category = '{comboBox2.Text}', Title = '{textBox2.Text}', Description = '{textBox3.Text}',DateStart = '{textBox1.Text}', DateEnd = '{textBox4.Text}' Where ID = '{textBox5.Text}'";
            UpdateConnection.Open();
            SqlCommand UpdateCmd = new SqlCommand(UpdateQuery, UpdateConnection);
            UpdateCmd.ExecuteReader();
            UpdateConnection.Close();  
            if (comboBox1.Text.Equals("Задания"))
            {
                Initializate();
            }
            if (comboBox1.Text.Equals("Выполненные задания"))
            {
                InitializateSpisokOne();
            }
             if (comboBox1.Text.Equals("Просроченные задания"))
            {
                InitializateSpisokTwo();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection Statusonnection = new SqlConnection(connectionString);
            Statusonnection.Open();
            string Status = dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString();
            if (Status == "False")
            {
                string Done = $"UPDATE AllTasks SET Status = 'True' where ID = '{textBox5.Text}'";
                SqlCommand SetStatus = new SqlCommand(Done, Statusonnection);
                SetStatus.ExecuteReader();



                if (comboBox1.Text.Equals("Задания"))
                {
                    Initializate();
                }
                if (comboBox1.Text.Equals("Выполненные задания"))
                {
                    InitializateSpisokOne();
                }
                if (comboBox1.Text.Equals("Просроченные задания"))
                {
                    InitializateSpisokTwo();
                }
            }
            if (Status == "True")
            {
                string Done = $"UPDATE AllTasks SET Status = 'False' where ID = '{textBox5.Text}'";
                SqlCommand SetStatus = new SqlCommand(Done, Statusonnection);
                SetStatus.ExecuteReader();
                if (comboBox1.Text.Equals("Задания"))
                {
                    Initializate();
                }
                if (comboBox1.Text.Equals("Выполненные задания"))
                {
                    InitializateSpisokOne();
                }
                if (comboBox1.Text.Equals("Просроченные задания"))
                {
                    InitializateSpisokTwo();
                }
            }
            Statusonnection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text.Equals("Задания"))
            {
                Initializate();
            }
            if(comboBox1.Text.Equals("Выполненные задания"))
            {
                InitializateSpisokOne();
            }
            if(comboBox1.Text.Equals("Просроченные задания"))
            {
                InitializateSpisokTwo();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Другое")
            {
                comboBox2.SelectedIndex = -1;
                MessageBox.Show("Введите свое название категории");
            }
        }
    }
}
