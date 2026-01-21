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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "data source=ROHAN\\SQLEXPRESS; database=pr; integrated security=SSPI";
            string name = textBox1.Text.Trim();
            string phone = textBox2.Text.Trim();
            string uid = textBox3.Text.Trim();
            string pass = textBox4.Text.Trim();
            string Cpass = textBox5.Text.Trim();
            string ans = textBox6.Text.Trim();
            string ques = comboBox1.Text.Trim();
            string dob = dateTimePicker1.Text;
            string add = richTextBox1.Text;
            string gender="";
            string role = "Citizen";
            
            if (radioButton1.Checked)
            {
                gender = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                gender = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                gender = radioButton3.Text;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name field must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(dob))
            {
                MessageBox.Show("Please select your date of birth.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(gender))
            {
                MessageBox.Show("Select your gender.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Provide your phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(uid))
            {
                MessageBox.Show("Username field must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(add))
            {
                MessageBox.Show("Address field must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Password field must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(Cpass))
            {
                MessageBox.Show("Please re-type your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ques))
            {
                MessageBox.Show("Please select security question.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ans))
            {
                MessageBox.Show("Give answer to the question.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(pass!=Cpass)
            {
                MessageBox.Show("Passwords doesn't match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM ctz WHERE uid=@uid", con))
                {
                    con.Open();
                    check.Parameters.AddWithValue("@uid", uid);
                    int exists = (int)check.ExecuteScalar();
                    if (exists > 0)
                    {
                        MessageBox.Show("Username already taken!");
                        return;
                    }
                }
            }
            string query = "INSERT INTO ctz ([name], [dob], [gender], [phone], [uid], [add], [pass], [ques], [ans], [role]) VALUES (@name, @dob, @gender, @phone, @uid, @add, @pass, @ques, @ans, @role)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@dob", dob);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@uid", uid);
                    command.Parameters.AddWithValue("@add", add);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.Parameters.AddWithValue("@ques", ques);
                    command.Parameters.AddWithValue("@ans", ans);
                    command.Parameters.AddWithValue("@role", role);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to create the profile. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.UseSystemPasswordChar = false;
                textBox5.UseSystemPasswordChar = false;
            }
            else
            {
                textBox4.UseSystemPasswordChar = true;
                textBox5.UseSystemPasswordChar = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
