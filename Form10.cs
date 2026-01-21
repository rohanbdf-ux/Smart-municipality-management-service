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
    public partial class Form10 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form10()
        {
            InitializeComponent();
        }
        public Form10(string uid)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form24 f24 = new Form24(uid1);
            f24.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form25 f25 = new Form25(uid1);
            f25.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form27 f27 = new Form27(uid1);
            f27.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form26 f26 = new Form26(uid1);
            f26.Show();
        }
    }
}
