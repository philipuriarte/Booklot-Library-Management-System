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
    public partial class ViewTab : Form
    {
        public ViewTab()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        // Closes ViewTab form and launches Home form
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home newTab = new Home();
            newTab.ShowDialog();
            this.Close();
        }

        // Loads the database to dataGrid when ViewTab form is launched
        private void ViewTab_Load(object sender, EventArgs e)
        {
            // Focus on datagrid because focusing on searchbox removes the appeal of placeholder
            this.ActiveControl = dgvBooks;
            con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
            con.Open();

            cmd = new SqlCommand("SELECT book.book_id, book.title, book.author, book.genre, book.edition, book.status, bd.borrow_date, bd.return_date FROM book INNER JOIN borrow_data bd ON book.book_id = bd.book_id; ", con);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dgvBooks.DataSource = dt;
            con.Close();
        }

        // As the form loads, "Enter text" is automatically in txtSearch textbox as a placeholder
        // When the user clicks on txtSearch, the placeholder will automatically be deleted
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Enter text")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        // When txtSearch is not in focus and is empty, the placeholder will be present again
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Enter text";
                txtSearch.ForeColor= Color.Gray;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Checks if txtSearch is empty
            if (txtSearch.Text == "Enter text" || String.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Field is empty. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ViewTab_Load(sender, e);
            }
            else
            {
                con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
                con.Open();
                string searchText = txtSearch.Text;

                // Checks if input in txtSearch is an integer value
                if (int.TryParse(txtSearch.Text, out int id))
                {
                    string cmdTextInt = "SELECT * FROM book WHERE book_id = '" + searchText + "'";
                    cmd = new SqlCommand(cmdTextInt, con);
                }
                else
                {
                    string cmdTextStr = "SELECT * FROM book WHERE title = '" + searchText + "' OR author = '" + searchText + "' OR genre = '" + searchText + "' OR edition = '" + searchText + "' OR publication = '" + searchText + "'";
                    cmd = new SqlCommand(cmdTextStr, con);
                }

                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvBooks.DataSource = dt;

                // Checks if search key is not found and displays error message
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Search key not found. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Reload the forms immediately
                    ViewTab_Load(sender, e);
                }
                con.Close();
            }
        }

        // Checks if the user pressed enter in txtSearch
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {         
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }            
        }

        // Sort feature
        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
            con.Open();
            string cmdText;

            switch (cmbSort.SelectedIndex)
            {
                // Sort Title in alphabetical order
                case 0:
                    cmdText = "SELECT * FROM book ORDER BY title";
                    cmd = new SqlCommand(cmdText, con);
                    break;
                // Sort Author in alphabetical order
                case 1:
                    cmdText = "SELECT * FROM book ORDER BY author";
                    cmd = new SqlCommand(cmdText, con);
                    break;
                // Sort BookID in ascending order
                case 2:
                    cmdText = "SELECT * FROM book ORDER BY book_id";
                    cmd = new SqlCommand(cmdText, con);
                    break;
                // Sort books by the date they were borrowed
                case 3:
                    cmdText = "SELECT book.book_id, book.title, book.author, book.genre, book.edition, book.status, bd.borrow_date, bd.return_date FROM book INNER JOIN borrow_data bd ON book.book_id = bd.book_id ORDER BY bd.borrow_date;";
                    cmd = new SqlCommand(cmdText, con);
                    break;
                // Sort books by the date they should be returned
                case 4:
                    cmdText = "SELECT book.book_id, book.title, book.author, book.genre, book.edition, book.status, bd.borrow_date, bd.return_date FROM book INNER JOIN borrow_data bd ON book.book_id = bd.book_id ORDER BY bd.return_date;";
                    cmd = new SqlCommand(cmdText, con);
                    break;
            }

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvBooks.DataSource = dt;
            con.Close();
        }

        // As the form loads, "Sort by:" is automatically in cmbSort.Text as a placeholder
        // When the user clicks on cmbSort, the placeholder will automatically be deleted and the list of options will be shown
        private void cmbSort_Enter(object sender, EventArgs e)
        {
            if (cmbSort.Text == "Sort by:")
                cmbSort.Text = "";
            cmbSort.DroppedDown = true;
        }

        // When cmbSort is not in focus and is empty, the placeholder will be present again
        private void cmbSort_Leave(object sender, EventArgs e)
        {
            if (cmbSort.Text == "")
                cmbSort.Text = "Sort by:";
        }
    }
}
