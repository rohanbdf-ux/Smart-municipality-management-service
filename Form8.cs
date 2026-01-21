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
    public partial class Form8 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string uid1;
        public Form8()
        {
            InitializeComponent();
        }
        public Form8(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            LoadN();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form16 f16 = new Form16(uid1," All complains","Complain");
            f16.Show();
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
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form16 f16 = new Form16(uid1,"All requests","Request");
            f16.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form18 f18 = new Form18(uid1);
            f18.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form19 f19 = new Form19(uid1);
            f19.Show();
        }
    }
}
