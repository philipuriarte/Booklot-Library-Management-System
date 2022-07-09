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

namespace LibraryManagementSystem
{
    public partial class DeleteTab : Form
    {
        public DeleteTab()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd, cmd0, cmdT, cmdA, cmdG, cmdE, cmdP;

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home newTab = new Home();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // checks if the txtBookID is empty
            if (String.IsNullOrEmpty(txtBookID.Text))
            {
                MessageBox.Show("Please enter a Book ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
                con.Open();
                int bookID = Convert.ToInt32(txtBookID.Text);
                bool idExist = false;

                cmd = new SqlCommand("SELECT * FROM booksData", con);

                // selects value from booksData table in the database with the same bookID as inputted and assigns them to their respective variables
                cmdT = new SqlCommand("SELECT Title FROM booksData WHERE BookID = '" + bookID + "'", con);
                var title = cmdT.ExecuteScalar();

                cmdA = new SqlCommand("SELECT Author FROM booksData WHERE BookID = '" + bookID + "'", con);
                var author = cmdA.ExecuteScalar();

                cmdG = new SqlCommand("SELECT Genre FROM booksData WHERE BookID = '" + bookID + "'", con);
                var genre = cmdG.ExecuteScalar();

                cmdE = new SqlCommand("SELECT Edition FROM booksData WHERE BookID = '" + bookID + "'", con);
                var edition = cmdE.ExecuteScalar();

                cmdP = new SqlCommand("SELECT Publication FROM booksData WHERE BookID = '" + bookID + "'", con);
                var pub = cmdP.ExecuteScalar();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // checks if the reader finds the text in txtBookID in the database
                    if (reader[0].ToString() == txtBookID.Text)
                    {
                        idExist = true;
                        break;
                    }
                }
                // checks if the boolean idExist is true
                if (idExist == true)
                {
                    con.Close();
                    con.Open();

                    DialogResult result = MessageBox.Show("Are you sure you want to delete Book with book ID: " + bookID + " ? \n Title: " + title + "\n Author: " + author + "\n Genre: " + genre + "\n Edition: " + edition + "\n Publication: " + pub, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    // checks if user chooses the "yes" option in the message box
                    if (result == DialogResult.Yes)
                    {
                        cmd0 = new SqlCommand("DELETE FROM booksData WHERE BookID = '" + bookID + "'", con);
                        cmd0.ExecuteNonQuery();
                        MessageBox.Show("Succesfully deleted book ID: " + bookID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        txtBookID.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Book ID: " + bookID + " does not exist. Please try again.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    txtBookID.Clear();
                }
            }
        }
    }
}