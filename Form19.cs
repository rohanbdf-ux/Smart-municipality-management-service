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
    public partial class Form19 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string uid1;
        public Form19()
        {
            InitializeComponent();
        }
        public Form19(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8(uid1);
            f8.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Enter tracking number to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "SELECT event as Event, status as Current_Status, type as Type FROM evn WHERE track LIKE @searchTerm";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchValue + "%");
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
        private void RMC(string uid)
        {
            string query =
            @"IF EXISTS(SELECT 1 FROM per WHERE uid = @uid)
                BEGIN
                    UPDATE per
                    SET rmc = ISNULL(rmc,0) + 1
                    WHERE uid = @uid
                END
            ELSE
                BEGIN
                    INSERT INTO per(uid, rmc)
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
        private void button3_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();
            string rem = textBox2.Text;

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Insert tracking number to add remarks.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(rem))
            {
                MessageBox.Show("Provide remarks to add.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
            string query = "UPDATE evn SET rem = @rem WHERE track = @searchValue";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    command.Parameters.AddWithValue("@rem", rem);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Remarks added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RMC(uid1);
                    }
                    else
                    {
                        MessageBox.Show("Unable to add remarks. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
