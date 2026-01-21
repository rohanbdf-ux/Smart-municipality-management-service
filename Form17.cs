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
    public partial class Form17 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string uid1,reject="Rejected",done="Done",approve="Approved",process="In process";
        public Form17()
        {
            InitializeComponent();
        }
        public Form17(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 f8 = new Form8(uid1);
            f8.Show();
        }
        private void APPC(string uid)
        {
            string query =
            @"IF EXISTS(SELECT 1 FROM per WHERE uid = @uid)
                BEGIN
                    UPDATE per
                    SET cmpc = ISNULL(cmpc,0) + 1
                    WHERE uid = @uid
                END
            ELSE
                BEGIN
                    INSERT INTO per(uid, cmpc)
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
        private void REJC(string uid)
        {
            string query = @"IF EXISTS(SELECT 1 FROM per WHERE uid = @uid)
                BEGIN
                    UPDATE per
                    SET reqc = ISNULL(reqc,0) + 1
                    WHERE uid = @uid
                END
            ELSE
                BEGIN
                    INSERT INTO per(uid, reqc)
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
            string newS = "Approved";
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Enter tracking number to proceed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "UPDATE evn SET status = @newS WHERE track = @searchValue";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    command.Parameters.AddWithValue("@newS", newS);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Complaint sent to supervisor!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        APPC(uid1);
                    }
                    else
                    {
                        MessageBox.Show("Couldn't send complaint. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string newS = "Rejected";
            string searchValue = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Enter tracking number to proceed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "UPDATE evn SET status = @newS WHERE track = @searchValue";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    command.Parameters.AddWithValue("@newS", newS);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Request rejected!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        REJC(uid1);
                    }
                    else
                    {
                        MessageBox.Show("unable to reject. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Enter tracking number to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "SELECT event as Event, status as Current_Status, type as Type FROM evn WHERE track LIKE @searchTerm AND status!=@reject AND status!=@done AND status!=@process AND status!=@approve ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchValue + "%");
                    command.Parameters.AddWithValue("@reject", reject);
                    command.Parameters.AddWithValue("@done", done);
                    command.Parameters.AddWithValue("@approve", approve);
                    command.Parameters.AddWithValue("@process", process);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView2.DataSource = dataTable;
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No complains/requests found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

    }
}
