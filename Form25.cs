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
    public partial class Form25 : Form
    {
        string uid1;
        string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
        public Form25()
        {
            InitializeComponent();
        }
        public Form25(string uid)
        {
            uid1 = uid;
            InitializeComponent();
            string query = "SELECT ctz.uid AS[User Id], ctz.name AS[Name], ed.data AS [Data to change], ed.reason AS[Reason to change data], ed.ndata AS[new data] FROM ed INNER JOIN ctz ON ed.uid=ctz.uid";
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
            Form10 f10 = new Form10(uid1);
            f10.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rid = textBox1.Text;
            string data = comboBox1.Text;
            string ndata = textBox2.Text;

            if (string.IsNullOrWhiteSpace(rid))
            {
                MessageBox.Show("Insert user id to edit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(data))
            {
                MessageBox.Show("Select which data to edit.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ndata))
            {
                MessageBox.Show("Provide the new data.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string msg1="";
            string msg2 = "";
            string query = "";
            if(data=="Name")
            {
                msg1 = "Name changed successfully for "+rid+"!";
                msg2 = "Couldn't change name. Please try again.";
                query = "UPDATE ctz SET name = @ndata WHERE uid = @rid";
            }
            if (data == "Phone")
            {
                msg1 = "Phone number changed successfully for " + rid + "!";
                msg2 = "Couldn't change Phone number. Please try again.";
                query = "UPDATE ctz SET phone = @ndata WHERE uid = @rid";
            }
            if (data == "DOB")
            {
                msg1 = "Date of birth changed successfully for " + rid + "!";
                msg2 = "Couldn't change date of birth. Please try again.";
                query = "UPDATE ctz SET dob = @ndata WHERE uid = @rid";
            }
            if (data == "Question")
            {
                msg1 = "Security question changed successfully for " + rid + "!";
                msg2 = "Couldn't change security question. Please try again.";
                query = "UPDATE ctz SET ques = @ndata WHERE uid = @rid";
            }
            if (data == "Address")
            {
                msg1 = "Address changed successfully for " + rid + "!";
                msg2 = "Couldn't change address. Please try again.";
                query = "UPDATE ctz SET add = @ndata WHERE uid = @rid";
            }
            string delR = "DELETE FROM ed WHERE uid = @rid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ndata", ndata);
                    command.Parameters.AddWithValue("@rid", rid);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show(msg1, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        using (SqlCommand command2 = new SqlCommand(delR, connection))
                        {
                            command2.Parameters.AddWithValue("@rid", rid);
                            int rowsAffected1 = command2.ExecuteNonQuery();
                            if (rowsAffected1 > 0)
                            {
                               MessageBox.Show("Request was deleted successfully!", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Request stays in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(msg2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
