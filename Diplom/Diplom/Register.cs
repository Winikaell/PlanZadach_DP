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
    
    public partial class Register : Form
    {
        string connectionString = @"Data Source=DESKTOP-RLF89VU\MYSERVER; Integrated Security=SSPI; Initial Catalog=diplom;";
        public Register()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection MyConnection = new SqlConnection(connectionString);
            string loginUser = textBox1.Text;
            string passwordUser = textBox2.Text;
            string emailUser = textBox3.Text;
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
                string email = Reader1[3].ToString();
                if (login == loginUser)
                {
                    MessageBox.Show("Пользователь с таким логином уже зарегистрирован");

                    return;
                }
                if (emailUser == email)
                {
                    MessageBox.Show("Данная почта занята");

                    return;
                }
                else
                {
                    Reader1.Close();
                    MyConnection.Close();
                    SqlConnection MyConnection1 = new SqlConnection(connectionString);
                    string loginUser1 = textBox1.Text;
                    string passwordUser1 = textBox2.Text;
                    string Email1 = textBox3.Text;
                    string RegisterQuery = $"Insert Into Users(Login,Password,Email) VALUES('{loginUser1}','{passwordUser1}','{Email1}')";
                    SqlCommand RegisterCmd = new SqlCommand(RegisterQuery, MyConnection1);
                    MyConnection1.Open();
                    RegisterCmd.ExecuteReader();
                    MyConnection1.Close();

                   
                    Auth Auth = new Auth();
                    Auth.Show();
                    this.Close();
                    return;
                }
            }
            
            
        }
    }
}
