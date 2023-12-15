using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FrmClubRegistration_Alba
{
    public partial class FrmClubRegistration : Form
    {
        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        public int RegistrtionID()
        {
            count++;
            return count;
        }

        public void RefreshListOfClubMembers()
        {
            try
            {
                ClubRegistrationQuery clubrgstrnqry = new ClubRegistrationQuery();
                clubrgstrnqry.DisplayList();
                dgvList.DataSource = clubrgstrnqry.bindingSource;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error po kuya");
            }
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            cbProgram.Items.Add("BS Information Technology");
            cbProgram.Items.Add("BS Hotel Management");
            cbProgram.Items.Add("BS Computer Science");
            cbProgram.Items.Add("BS Tourism Management");
            cbProgram.Items.Add("BS Terrorism Suicide Thing");
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
            cbGender.Items.Add("Half/Half");
            RefreshListOfClubMembers();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateMember fum = new FrmUpdateMember();
            fum.ShowDialog();
        }

        long StudentID;
        private void btnRegister_Click(object sender, EventArgs e)
        {
            ID = RegistrtionID();
            Age = Convert.ToInt32(txtAge.Text);
            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Gender = cbGender.Text;
            Program = cbProgram.Text;
            StudentID = Convert.ToInt64(txtStudentID.Text);
            clubRegistrationQuery = new ClubRegistrationQuery();
            clubRegistrationQuery.RegisterStudent(ID, StudentID, FirstName, MiddleName, LastName, Age, Gender, Program);
            RefreshListOfClubMembers();
        }
    }
}
