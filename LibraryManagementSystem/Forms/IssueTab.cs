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
            txtStudID.Clear();
            txtName.Clear();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
            con.Open();

            int bookID = Convert.ToInt32(txtBookID.Text);
            string studID = txtStudID.Text;
            string name = txtName.Text;
            string unavailText = "Borrowed by " + name + ", student ID: " + studID;

            string cmdText = "UPDATE booksData SET Status = '" + unavailText + "' WHERE BookID = '" + bookID + "'";
            cmd = new SqlCommand(cmdText, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully issued book ID " + bookID + " to student " + studID);
            con.Close();

            txtBookID.Clear();
            txtStudID.Clear();
            txtName.Clear();
        }
    }
}
