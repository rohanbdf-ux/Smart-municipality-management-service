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
    public partial class Form9 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form9()
        {
            InitializeComponent();
        }
        public Form9(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            LoadN();
        }
        private void LoadN()
        {
            string query = "SELECT name FROM ctz WHERE uid = @uid1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        label1.Text = reader["name"].ToString();
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form20 f20 = new Form20(uid1);
            f20.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form21 f21 = new Form21(uid1);
            f21.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form22 f22 = new Form22(uid1);
            f22.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form23 f23 = new Form23(uid1);
            f23.Show();
        }
    }
}
