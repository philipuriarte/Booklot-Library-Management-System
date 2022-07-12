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
                con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
                con.Open();

                int bookID = 1;
                string title = txtTitle.Text;
                string author = txtAuthor.Text;
                string genre = "Undefined";
                string edition = txtEdition.Text;
                string publication = txtPublication.Text;

                // Sets value of genre variable
                switch(cmbGenre.SelectedIndex)
                {
                    case 0:
                        genre = "Fantasy";
                        break;
                    case 1:
                        genre = "Science Fiction";
                        break;
                    case 2:
                        genre = "Romance";
                        break;
                    case 3:
                        genre = "Comedy";
                        break;
                    case 4:
                        genre = "Classic";
                        break;
                    case 5:
                        genre = "History";
                        break;
                    case 6:
                        genre = "Science";
                        break;
                    case 7:
                        genre = "Computers & Software";
                        break;
                    case 8:
                        genre = "Biographies & Autobiographies";
                        break;
                    case 9:
                        genre = "Religion & Philosophy";
                        break;
                }

                // Variable used to check if current value of bookID exists in database
                bool idExist = true;                

                // Auto-generation of book_id
                while (idExist)
                {
                    cmd = new SqlCommand("SELECT * FROM book WHERE book_id = '" + bookID + "'", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    // Checks if book_id exists in database, else increments value of bookID by +1
                    if (dt.Rows.Count == 0)
                        idExist = false;
                    else
                        bookID += 1;
                }

                string cmdText = "INSERT INTO book VALUES ('" + bookID + "','" + title + "','" + author + "','" + genre + "','" + edition + "','" + publication + "','" + "Avail" + "')";
                cmd = new SqlCommand(cmdText, con);
                cmd.ExecuteNonQuery();
                string cmdText2 = "INSERT INTO borrow_data (book_id) VALUES(" + bookID + ")";
                cmd = new SqlCommand(cmdText2, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully added one book.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();

                // Clears the input on all textboxes and the cmbGenre
                btnClear_Click(sender, e);
            }            
        }

        // As the form loads, "Please select..." is automatically in cmbGenre.Text as a placeholder
        // When the user clicks on cmbGenre, the placeholder will automatically be deleted and the list of options will be shown
        private void cmbGenre_Enter(object sender, EventArgs e)
        {
            if (cmbGenre.Text == "Please select...")
                cmbGenre.Text = "";             
            cmbGenre.DroppedDown = true;
        }

        // When cmbGenre is not in focus and is empty, the placeholder will be present again
        private void cmbGenre_Leave(object sender, EventArgs e)
        {
            if (cmbGenre.Text == "")
                cmbGenre.Text = "Please select...";
        }
    }
}
