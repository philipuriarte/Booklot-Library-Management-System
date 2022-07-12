using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibraryManagementSystem.Forms
{
    public partial class DeleteMemberTab : Form
    {
        public DeleteMemberTab()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd, cmd0, cmdN, cmdEA;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Checks if the txtMemberID is empty
            if (String.IsNullOrEmpty(txtMemberID.Text))
            {
                MessageBox.Show("Please enter a Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
                con.Open();
                int memberID = Convert.ToInt32(txtMemberID.Text);
                bool idExist = false;

                cmd = new SqlCommand("SELECT * FROM member", con);

                // Selects value from members_data table in the database with the same memberID as inputted and assigns them to their respective variables
                cmdN = new SqlCommand("SELECT name FROM member WHERE mem_id = '" + memberID + "'", con);
                var name = cmdN.ExecuteScalar();

                cmdEA = new SqlCommand("SELECT email FROM member WHERE mem_id = '" + memberID + "'", con);
                var email_address = cmdEA.ExecuteScalar();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Checks if the reader finds the text in txtMemberID in the database
                    if (reader[0].ToString() == txtMemberID.Text)
                    {
                        idExist = true;
                        break;
                    }
                }
                // Checks if the boolean idExist is true
                if (idExist == true)
                {
                    con.Close();
                    con.Open();

                    DialogResult result = MessageBox.Show("Are you sure you want to member with Member ID: " + memberID + " ? \n Name: " + name + "\n Email address: " + email_address, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    // Checks if user chooses the "yes" option in the message box
                    if (result == DialogResult.Yes)
                    {
                        cmd0 = new SqlCommand("DELETE FROM member WHERE mem_id = '" + memberID + "'", con);
                        cmd0.ExecuteNonQuery();
                        MessageBox.Show("Succesfully deleted Member ID: " + memberID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        txtMemberID.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Member ID: " + memberID + " does not exist. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    txtMemberID.Clear();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageTab newTab = new ManageTab();
            newTab.ShowDialog();
            this.Close();
        }
    }
}
