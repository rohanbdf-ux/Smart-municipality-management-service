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
    public partial class Form24 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form24()
        {
            InitializeComponent();
        }
        public Form24(string uid)
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
            string did = textBox1.Text;
            if (string.IsNullOrWhiteSpace(did))
            {
                MessageBox.Show("Please enter username to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Are you sure you want to delete this user?","Confirm Deletion",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM ctz WHERE uid = @did";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@did", did);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No user was found to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
