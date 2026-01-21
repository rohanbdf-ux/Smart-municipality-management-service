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
    public partial class Form14 : Form
    {
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        string uid1;
        public Form14()
        {
            InitializeComponent();
        }
        public Form14(string uid)
        {
            uid1 = uid;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 f7 = new Form7(uid1);
            f7.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pos = comboBox1.Text;
            string edu = comboBox2.Text;
            string dep = textBox2.Text;
            string year = comboBox3.Text;
            string res = textBox1.Text;
            if (string.IsNullOrWhiteSpace(pos))
            {
                MessageBox.Show("Select the position you want to apply for.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(edu))
            {
                MessageBox.Show("Select your highest education level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(dep))
            {
                MessageBox.Show("Fill up the department field.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                MessageBox.Show("Select passing year.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(res))
            {
                MessageBox.Show("Please fill up the result field.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "INSERT INTO st ([uid], [pos], [edu], [dep], [pas], [res]) VALUES (@uid1, @pos, @edu, @dep, @year, @res)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@uid1", uid1);
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@edu", edu);
                    command.Parameters.AddWithValue("@dep", dep);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@res", res);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Application for "+pos+" successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form7 f7 = new Form7(uid1);
                        f7.Show();
                    }
                    else
                    {
                        MessageBox.Show("Faild to apply", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
