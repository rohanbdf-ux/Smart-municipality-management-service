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
    public partial class Form16 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string uid1,lb, reject="Rejected";
        public Form16()
        {
            InitializeComponent();
        }
        public Form16(string uid,string ty,string tpe)
        {
            lb = tpe;
            uid1 = uid;
            InitializeComponent();
            label1.Text = ty;
            string query = "SELECT event as Event, status as Status, rem as Remarks, track as Tracking_number, uid as User_Id FROM evn WHERE type = @lb AND status != @reject";
            FillDataGridView(query);
        }
        private void FillDataGridView(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@lb", lb);
                    command.Parameters.AddWithValue("@reject", reject);
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
            Form8 f8 = new Form8(uid1);
            f8.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form17 f17 = new Form17(uid1);
            f17.Show();
        }
    }
}
