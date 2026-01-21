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
    public partial class Form7 : Form
    {
        string uid1;
        public Form7()
        {
            InitializeComponent();
        }
        public Form7(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            LoadN();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
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
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 f11 = new Form11(uid1,"Submit your complain below","Complain", "Complain submitted successfully!", "Failed to sumbit complain. Please try again.");
            f11.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 f11 = new Form11(uid1, "Make a request below","Request", "Request submitted successfully!", "Failed to sumbit complain. Please try again.");
            f11.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form12 f12 = new Form12(uid1);
            f12.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form13 f13 = new Form13(uid1);
            f13.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form14 f14 = new Form14(uid1);
            f14.Show();
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form15 f15 = new Form15(uid1);
            f15.Show();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
