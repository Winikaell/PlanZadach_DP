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
    public partial class Auth : Form
    {
        string connectionString = @"Data Source=DESKTOP-RLF89VU\MYSERVER; Integrated Security=SSPI; Initial Catalog=diplom;";
        public Auth()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection MyConnection = new SqlConnection(connectionString);
            string loginUser = textBox1.Text;
            string passwordUser = textBox2.Text;
            string loginsQuery = "SELECT * FROM Users";
            SqlDataReader Reader1;
            SqlCommand loginCmd = new SqlCommand(loginsQuery, MyConnection);
            MyConnection.Open();
            Reader1 = loginCmd.ExecuteReader();
            while (Reader1.Read())
            {
                int UserID = Convert.ToInt32(Reader1[0]);
                string login = Reader1[1].ToString();
                string password = Reader1[2].ToString();
                if (login == loginUser && password == passwordUser)
                {
                    ToDoList tdl = new ToDoList(UserID);
                    this.Hide();
                    tdl.Show();

                    return;
                }
            }
            MessageBox.Show("Неправильный пароль или логин");
            MyConnection.Close();
            Reader1.Close();
        }
    }
}
