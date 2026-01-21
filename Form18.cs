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
    public partial class Form18 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string uid1,reject="Rejected";
        public Form18()
        {
            InitializeComponent();
        }
        public Form18(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }

        private void Form18_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8(uid1);
            f8.Show();
        }
        private void STC(string uid)
        {
            string query = @"IF EXISTS(SELECT 1 FROM per WHERE uid = @uid)
                BEGIN
                    UPDATE per
                    SET stc = stc + 1
                    WHERE uid = @uid
                END
            ELSE
                BEGIN
                    INSERT INTO per(uid, stc)
                     VALUES(@uid, 1)
                END";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@uid", SqlDbType.NVarChar).Value = uid;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string newS = comboBox1.Text;
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(newS))
            {
                MessageBox.Show("Select new status to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Enter tracking number to update status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "UPDATE evn SET status = @newS WHERE track = @searchValue AND status!=@reject";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    command.Parameters.AddWithValue("@newS", newS);
                    command.Parameters.AddWithValue("@reject", reject);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        STC(uid1);
                    }
                    else
                    {
                        MessageBox.Show("Couldn't update status. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Enter tracking number to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "SELECT event as Event, status as Current_Status, type as Type FROM evn WHERE track LIKE @searchTerm AND status!=@reject ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchValue + "%");
                    command.Parameters.AddWithValue("@reject", reject);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No complains/requests found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
