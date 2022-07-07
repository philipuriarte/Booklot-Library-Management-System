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
        int bookID;
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
            con = new SqlConnection("Data Source=LAPTOP-ID3FFBBJ\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
            con.Open();

            /*int bookID = Convert.ToInt32(txtBookID.Text);
            string libraryID = txtLibraryID.Text;
            string unavailText = "Borrowed by student library ID: " + libraryID;

            string cmdText = "UPDATE booksData SET Status = '" + unavailText + "' WHERE BookID = '" + bookID + "'";
            cmd = new SqlCommand(cmdText, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully issued book ID " + bookID + " to student " + libraryID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();

            txtBookID.Clear();
            txtLibraryID.Clear();*/

            string libraryID = txtLibraryID.Text;
            string unavailText = "Borrowed by student library ID: " + libraryID;


            if (String.IsNullOrEmpty(txtBookID.Text) || String.IsNullOrEmpty(txtLibraryID.Text))
            {
                MessageBox.Show("Please complete all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                bookID = Convert.ToInt32(txtBookID.Text);
                cmd = new SqlCommand("SELECT * FROM booksData WHERE BookID = '" + bookID + "'", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    /*
                    cmd = new SqlCommand("UPDATE booksData SET Status = '" + unavailText + "' WHERE BookID = '" + bookID + "'", con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully issued book ID " + bookID + " to student " + libraryID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();

                    txtBookID.Clear();
                    txtLibraryID.Clear();
                    */

                    cmd = new SqlCommand("SELECT * FROM booksData WHERE BookID = '" + bookID + "' AND Status = 'avail'", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmd = new SqlCommand("UPDATE booksData SET Status = '" + unavailText + "' WHERE BookID = '" + bookID + "'", con);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully issued book ID " + bookID + " to member " + libraryID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();

                        txtBookID.Clear();
                        txtLibraryID.Clear();
                    }
                    else {
                        MessageBox.Show("Book is currently unavailabe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookID.Clear();
                        txtLibraryID.Clear();
                    }

                }
                else {
                    MessageBox.Show("Book ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBookID.Clear();
                    txtLibraryID.Clear();
                }
            }
        }
    }
}
