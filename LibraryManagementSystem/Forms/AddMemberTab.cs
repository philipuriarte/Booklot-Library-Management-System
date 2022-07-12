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
    public partial class AddMemberTab : Form
    {
        public AddMemberTab()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageTab newTab = new ManageTab();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Checks if any txtBoxes are empty
            if (String.IsNullOrEmpty(txtFullName.Text) || String.IsNullOrEmpty(txtEmailAd.Text))
            {
                MessageBox.Show("Please fill up all the boxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
                con.Open();

                int member_id = 1;
                string name = txtFullName.Text;
                string email_address = txtEmailAd.Text;

                // Variable used to check if current value of member_id exists in database
                bool idExist = true;

                // Auto-generation of member_id
                while (idExist)
                {
                    cmd = new SqlCommand("SELECT * FROM member WHERE mem_id = '" + member_id + "'", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    // Checks if member_id exists in database, else increments value of member_id by +1
                    if (dt.Rows.Count == 0)
                        idExist = false;
                    else
                        member_id += 1;
                }

                string cmdText = "INSERT INTO member VALUES ('" + member_id + "','" + name + "','" + email_address+ "')";
                cmd = new SqlCommand(cmdText, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully added one member.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();

                // Clears the input on all textboxes
                btnClear_Click(sender, e);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFullName.Clear();
            txtEmailAd.Clear();
        }
    }
}