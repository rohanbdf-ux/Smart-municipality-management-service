using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace demo2
{
    public partial class Form3 : Form
    {
        string uid1,role="";
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string query = "SELECT role FROM ctz WHERE uid = @uid1";
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@role", role);
                    command.Parameters.AddWithValue("@uid1", uid1);
                    connection.Open();
                    role = command.ExecuteScalar().ToString();
                    if (role=="Citizen")
                    {
                        MessageBox.Show("log in success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form7 f7 = new Form7(uid1);
                        f7.Show();
                    }
                    else
                    {
                        MessageBox.Show("Account with username "+uid1+" is not a normal citizen.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    command.Parameters.AddWithValue("@role", role);
                    connection.Open();
                    role = command.ExecuteScalar().ToString();
                    if (role == "Officer")
                    {
                        MessageBox.Show("log in success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form8 f8 = new Form8(uid1);
                        f8.Show();
                    }
                    else
                    {
                        MessageBox.Show("Account with username " + uid1 + " is not an officer.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    command.Parameters.AddWithValue("@role", role);
                    connection.Open();
                    role = command.ExecuteScalar().ToString();
                    if (role == "Supervisor")
                    {
                        MessageBox.Show("log in success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form9 f9 = new Form9(uid1);
                        f9.Show();
                    }
                    else
                    {
                        MessageBox.Show("Account with username " + uid1 + " is not a supervisor.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    command.Parameters.AddWithValue("@role", role);
                    connection.Open();
                    role = command.ExecuteScalar().ToString();
                    if (role == "Admin")
                    {
                        MessageBox.Show("log in success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form10 f10 = new Form10(uid1);
                        f10.Show();
                    }
                    else
                    {
                        MessageBox.Show("Account with username " + uid1 + " is not an admin.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
