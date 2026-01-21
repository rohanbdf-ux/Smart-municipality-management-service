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
    public partial class Form5 : Form
    {
        string uid1;
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            LoadQ();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        private void LoadQ()
        {
            string query = "SELECT ques FROM ctz WHERE uid = @uid1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        richTextBox1.Text = reader["ques"].ToString();
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ans = textBox1.Text;
            if (string.IsNullOrWhiteSpace(ans))
            {
                MessageBox.Show("Give correct answer to the question.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
            string query = "SELECT COUNT(*) FROM ctz WHERE uid = @uid1 AND ans = @ans";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    command.Parameters.AddWithValue("@ans", ans);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Verification success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form6 f6 = new Form6(uid1);
                        f6.Show();
                    }
                    else
                    {
                        MessageBox.Show("Wrong answer.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
