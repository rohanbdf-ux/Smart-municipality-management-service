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
    public partial class Form11 : Form
    {
        string uid1,type,ms1,ms2;
        public Form11()
        {
            InitializeComponent();
        }
        public Form11(string uid, string t, string t1,string m1,string m2)
        {
               uid1 = uid;
            type = t1;
            ms1 = m1;
            ms2 = m2;
            InitializeComponent();
            label1.Text = t;
        }
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string status = "Pending";
        string rem = "";
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7(uid1);
            f7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7(uid1);
            f7.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string comp = richTextBox1.Text;
            if (string.IsNullOrWhiteSpace(comp))
            {
                MessageBox.Show("Fill up the empty field to submit complain.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "INSERT INTO evn ([event], [status], [type], [rem], [uid]) VALUES (@comp, @status, @type, @rem, @uid1)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@comp", comp);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rem", rem);
                    command.Parameters.AddWithValue("@uid1", uid1);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show(ms1, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form7 f7 = new Form7(uid1);
                        f7.Show();
                    }
                    else
                    {
                        MessageBox.Show(ms2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
