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
    public partial class Form12 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        String uid1;
        public Form12()
        {
            InitializeComponent();
        }
        public Form12(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            string query = "SELECT event as Event, status as Status, type as Type, rem as Remarks, track as Tracking_number FROM evn WHERE uid = @uid1";
            FillDataGridView(query);
        }
        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
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
            Form7 f7 = new Form7(uid1);
            f7.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Enter tracking number to check.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "SELECT event as Event, status as Status, type as Type, rem as Remarks, track as Tracking_number FROM evn WHERE track LIKE @searchTerm and uid=@uid1 ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchValue + "%");
                    command.Parameters.AddWithValue("@uid1",uid1);
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
        private void Form12_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
