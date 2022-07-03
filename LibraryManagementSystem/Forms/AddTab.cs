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
    public partial class AddTab : Form
    {
        public AddTab()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        public static int bookID = 1;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home newTab = new Home();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtEdition.Clear();
            txtPublication.Clear();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {   
            // Checks if any txtBoxes are empty
            if (String.IsNullOrEmpty(txtTitle.Text) || String.IsNullOrEmpty(txtAuthor.Text) || String.IsNullOrEmpty(txtEdition.Text) || String.IsNullOrEmpty(txtPublication.Text))
            {
                MessageBox.Show("Please fill up all the textboxes.");
            }
            else
            {
                con = new SqlConnection("Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
                con.Open();

                string title = txtTitle.Text;
                string author = txtAuthor.Text;
                string genre = "Undefined";
                string edition = txtEdition.Text;
                string publication = txtPublication.Text;

                // Sets value of genre variable. Incomplete, need to define all genres first in the library.
                if (cmbGenre.SelectedIndex == 0)
                    genre = "Fantasy";
                else if (cmbGenre.SelectedIndex == 1)
                    genre = "Science Fiction";
                else if (cmbGenre.SelectedIndex == 2)
                    genre = "Romance";
                else if (cmbGenre.SelectedIndex == 3)
                    genre = "Comedy";
                else if (cmbGenre.SelectedIndex == 4)
                    genre = "Classic";
                else if (cmbGenre.SelectedIndex == 5)
                    genre = "History";

                string cmdText = "INSERT INTO booksData VALUES ('" + bookID + "','" + title + "','" + author + "','" + genre + "','" + edition + "','" + publication + "','" + "Avail" + "')";
                cmd = new SqlCommand(cmdText, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully added one book.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();

                btnClear_Click(sender, e);

                bookID += 1;
            }            
        }
    }
}
