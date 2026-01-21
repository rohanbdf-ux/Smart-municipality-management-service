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
    public partial class Form22 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form22()
        {
            InitializeComponent();
        }
        public Form22(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            string query = "SELECT ctz.uid AS[Officer's Id], ctz.name AS[Officer's name], per.rmc AS Remarks_Added, per.cmpc AS[Total Approved], per.reqc AS[Total Rejected], per.stc AS Status_Updated FROM per INNER JOIN ctz ON per.uid=ctz.uid";
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
    }
}
