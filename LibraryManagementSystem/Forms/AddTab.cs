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

        // Public variable used for BookID
        public static int bookID = 1;

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
            cmbGenre.SelectedItem = null;
            txtEdition.Clear();
            txtPublication.Clear();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {   
            // Checks if any txtBoxes are empty
            if (String.IsNullOrEmpty(txtTitle.Text) || String.IsNullOrEmpty(txtAuthor.Text) || String.IsNullOrEmpty(cmbGenre.Text) || String.IsNullOrEmpty(txtEdition.Text) || String.IsNullOrEmpty(txtPublication.Text))
            {
                MessageBox.Show("Please fill up all the boxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Sets value of genre variable
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
                else if (cmbGenre.SelectedIndex == 6)
                    genre = "Science";
                else if (cmbGenre.SelectedIndex == 7)
                    genre = "Computers & Software";
                else if (cmbGenre.SelectedIndex == 8)
                    genre = "Biographies & Autobiographies";
                else if (cmbGenre.SelectedIndex == 9)
                    genre = "Religion/Philosophy";

                string cmdText = "INSERT INTO booksData VALUES ('" + bookID + "','" + title + "','" + author + "','" + genre + "','" + edition + "','" + publication + "','" + "Avail" + "')";
                cmd = new SqlCommand(cmdText, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully added one book.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();

                // Clears the input on all textboxes and the cmbGenre
                btnClear_Click(sender, e);

                // Everytime a book is added the value of book is increased by 1 to make each ID unique
                bookID += 1;
            }            
        }

        // As the form loads, "Please select..." is automatically in cmbGenre.Text as a placeholder
        // When the user clicks on the combobox, the placeholder will automatically be deleted
        private void cmbGenre_Enter(object sender, EventArgs e)
        {
            if (cmbGenre.Text == "Please select...")
                cmbGenre.Text = "";
        }

        // When the textbox is not in focus and is empty, the placeholder will be present again
        private void cmbGenre_Leave(object sender, EventArgs e)
        {
            if (cmbGenre.Text == "")
                cmbGenre.Text = "Please select...";
        }
    }
}
