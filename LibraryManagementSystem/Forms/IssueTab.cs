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
    public partial class IssueTab : Form
    {
        public IssueTab()
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
            Home newTab = new Home();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBookID.Clear();
            txtLibraryID.Clear();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtBookID.Text) || String.IsNullOrEmpty(txtLibraryID.Text)) //checks if txtBookID or txtLibraryID fields are empty
            {
                MessageBox.Show("Please complete all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
                con.Open();

                int bookID = Convert.ToInt32(txtBookID.Text);
                string libraryID = txtLibraryID.Text;
                string unavailText = "Borrowed by member ID: " + libraryID;

                
                cmd = new SqlCommand("SELECT * FROM booksData WHERE BookID = '" + bookID + "'", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0) //check if bookID entered matches any in BookID column from booksData
                {
                    cmd = new SqlCommand("SELECT * FROM booksData WHERE BookID = '" + bookID + "' AND Status = 'avail'", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0) //check if bookID matches and Status is 'avail'
                    {
                        cmd = new SqlCommand("UPDATE booksData SET Status = '" + unavailText + "' WHERE BookID = '" + bookID + "'", con);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully issued book ID " + bookID + " to member " + libraryID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();

                        btnClear_Click(sender, e);
                    }
                    else {
                        MessageBox.Show("Book is currently unavailabe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnClear_Click(sender, e);
                    }
                }
                else {
                    MessageBox.Show("Book ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnClear_Click(sender, e);
                }
            }
        }
    }
}
