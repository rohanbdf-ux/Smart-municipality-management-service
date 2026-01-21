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
    public partial class Form21 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form21()
        {
            InitializeComponent();
        }
        public Form21(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            string query = "SELECT ctz.uid AS[Applicant's Id], ctz.name AS[Applicant's name], st.pos AS [Applied position], st.edu AS[Highest education level], st.dep AS[department], st.pas AS [Passing year], st.res AS [Result] FROM st INNER JOIN ctz ON st.uid=ctz.uid";
            FillDataGridView(query);
        }
        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form9 f9 = new Form9(uid1);
            f9.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string aid = textBox1.Text.Trim();
            string pos = comboBox1.Text;

            if (string.IsNullOrWhiteSpace(aid))
            {
                MessageBox.Show("Provide username to hire employee.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(pos))
            {
                MessageBox.Show("Select position for applicant "+aid, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "UPDATE ctz SET role = @pos WHERE uid = @aid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@aid", aid);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Citizen "+aid+" hired successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string query1 = "DELETE FROM st WHERE uid = @aid";
                        using (SqlConnection connection1 = new SqlConnection(connectionString))
                        {
                            using (SqlCommand command1 = new SqlCommand(query1, connection1))
                            {
                                command1.Parameters.AddWithValue("@aid", aid);
                                connection1.Open();
                                command1.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to hire. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string aid = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(aid))
            {
                MessageBox.Show("Provide username to reject applicant.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Are you sure you want to reject this application?","Confirm Deletion",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query1 = "DELETE FROM st WHERE uid = @aid";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@aid", aid);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Applicant " + aid + "'s application rejected successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to reject application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
