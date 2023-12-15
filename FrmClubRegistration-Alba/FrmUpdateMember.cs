using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmClubRegistration_Alba
{
    public partial class FrmUpdateMember : Form
    {
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        string value;
        private ClubRegistrationQuery cqr;
        SqlCommand command;
        SqlDataReader reader;

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            cbProgram.Items.Add("BS Information Technology");
            cbProgram.Items.Add("BS Hotel Management");
            cbProgram.Items.Add("BS Computer Science");
            cbProgram.Items.Add("BS Tourism");
            cbProgram.Items.Add("BS Terrorism");
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbGender.Items.Add("Half/Half");

            cqr = new ClubRegistrationQuery();
            using (SqlConnection sqlConn = new SqlConnection(cqr.connectionString))
            {
                sqlConn.Open();
                string query = "Select StudentId FROM ClubMembers";
                command = new SqlCommand(query, sqlConn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cbStudentID.Items.Add(reader.GetValue(0));
                }
                reader.Close();
                sqlConn.Close();
            }
            cbStudentID.SelectedIndex = 0;
        }

        private void cbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            value = cbStudentID.SelectedItem.ToString();

            cqr = new ClubRegistrationQuery();
            using (SqlConnection sqlConn = new SqlConnection(cqr.connectionString))
            {
                sqlConn.Open();
                string query = "Select * FROM ClubMembers WHERE StudentId = '" + value + "'";
                command = new SqlCommand(query, sqlConn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    txtLastName.Text = "" + reader.GetValue(4);
                    txtFirstName.Text = "" + reader.GetValue(2);
                    txtMiddleName.Text = "" + reader.GetValue(3);
                    txtAge.Text = "" + reader.GetValue(5);
                    cbGender.Text = "" + reader.GetValue(6);
                    cbProgram.Text = "" + reader.GetValue(7);
                }
                reader.Close();
                sqlConn.Close();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            cqr = new ClubRegistrationQuery();
            using (SqlConnection sqlConn = new SqlConnection(cqr.connectionString))
            {
                sqlConn.Open();
                string updateQuery = "UPDATE ClubMembers SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId = '" + value + "'";
                command = new SqlCommand(updateQuery, sqlConn);
                command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                command.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
                command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                command.Parameters.AddWithValue("@Age", txtAge.Text);
                command.Parameters.AddWithValue("@Gender", cbGender.Text);
                command.Parameters.AddWithValue("@Program", cbProgram.Text);
                command.ExecuteNonQuery();
                sqlConn.Close();
            }
        }
    }
}
