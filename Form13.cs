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
    public partial class Form13 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string uid1;
        public Form13()
        {
            InitializeComponent();
        }
        public Form13(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7(uid1);
            f7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = comboBox1.Text;
            string reason = richTextBox1.Text;
            string ndata = textBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(data))
            {
                MessageBox.Show("Select the data you want to change.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Give specific reason to change the data.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ndata))
            {
                MessageBox.Show("Enter new "+data, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "INSERT INTO ed ([uid], [reason], [data], [ndata]) VALUES (@uid1, @reason, @data, @ndata)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    command.Parameters.AddWithValue("@reason", reason);
                    command.Parameters.AddWithValue("@data", data);
                    command.Parameters.AddWithValue("@ndata", ndata);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Request for edit successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form7 f7 = new Form7(uid1);
                        f7.Show();
                    }
                    else
                    {
                        MessageBox.Show("Unable to request edit. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
