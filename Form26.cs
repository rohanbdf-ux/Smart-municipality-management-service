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
    public partial class Form26 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form26()
        {
            InitializeComponent();
        }
        public Form26(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f10 = new Form10(uid1);
            f10.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nid = textBox1.Text;
            string nrole = comboBox1.Text;

            if (string.IsNullOrWhiteSpace(nid))
            {
                MessageBox.Show("Enter username to update role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(nrole))
            {
                MessageBox.Show("Select new role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "UPDATE ctz SET role = @nrole WHERE uid = @nid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nrole", nrole);
                    command.Parameters.AddWithValue("@nid", nid);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Role changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Couldn't change role. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
