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
    public partial class Form27 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form27()
        {
            InitializeComponent();
        }
        public Form27(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f10 = new Form10(uid1);
            f10.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tn = textBox1.Text;
            string ns = comboBox1.Text;
            string nr = textBox2.Text;

            if (string.IsNullOrWhiteSpace(tn))
            {
                MessageBox.Show("Enter tracking number to Modify.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ns))
            {
                MessageBox.Show("Select new status to modify.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(nr))
            {
                MessageBox.Show("Add new remarks to modify.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE evn SET status = @ns, rem = @nr WHERE track = @tn";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ns", ns);
                    command.Parameters.AddWithValue("@nr", nr);
                    command.Parameters.AddWithValue("@tn", tn);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Event modified successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Couldn't Modify event. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
