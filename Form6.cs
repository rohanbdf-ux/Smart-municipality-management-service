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
    public partial class Form6 : Form
    {
        string uid1;
        public Form6()
        {
            InitializeComponent();
        }
        public Form6(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string newPass = textBox2.Text.Trim();
            string newCpass = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPass))
            {
                MessageBox.Show("Enter your new password to reset.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(newCpass))
            {
                MessageBox.Show("Please re-type your new password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newPass != newCpass)
            {
                MessageBox.Show("New password doesn't match in both field.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
            string query = "UPDATE ctz SET pass = @newPass WHERE uid = @uid1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    command.Parameters.AddWithValue("@newPass", newPass);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.Show();
                    }
                    else
                    {
                        MessageBox.Show("Password couldn't be changed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
