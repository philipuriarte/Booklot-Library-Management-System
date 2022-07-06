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
            this.ActiveControl = dgvBooks;
            con = new SqlConnection("Data Source=MAJO-PC\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
            con.Open();

            cmd = new SqlCommand("SELECT * FROM booksData", con);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dgvBooks.DataSource = dt;
            con.Close();
        }

        //As the form loads, "Enter text" is automatically in the txtSearch textbox as a placeholder
        // When the user clicks on the textbox, the placeholder will automatically be deleted.
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Enter text")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        //When the textbox is not in focus and is empty, the placeholder will be present again.
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
            // checks if txtSearch is empty
            if (txtSearch.Text == "Enter text" || String.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Field is empty. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ViewTab_Load(sender, e);
            }
            else
            {
                con = new SqlConnection("Data Source=MAJO-PC\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
                con.Open();
                string searchText = txtSearch.Text;

                // checks if input in txtSearch is an integer value
                if (int.TryParse(txtSearch.Text, out int id))
                {
                    string cmdTextInt = "SELECT * FROM booksData WHERE BookID = '" + searchText + "'";
                    cmd = new SqlCommand(cmdTextInt, con);
                }
                else
                {
                    string cmdTextStr = "SELECT * FROM booksData WHERE Title = '" + searchText + "' OR Author = '" + searchText + "' OR Genre = '" + searchText + "' OR Edition = '" + searchText + "' OR Publication = '" + searchText + "'";
                    cmd = new SqlCommand(cmdTextStr, con);
                }

                
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvBooks.DataSource = dt;

                //Check if search key is not found and display error message.
                if (dt != null)
                {
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Search key not found. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ViewTab_Load(sender, e); //reload the forms immediately
                    }
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

        // Sort feature incomplete. Currently in testing.
        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=MAJO-PC\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
            con.Open();

            // Sort Title in alphabetical order
            if (cmbSort.SelectedIndex == 0)
            {
                string cmdText = "SELECT * FROM booksData ORDER BY Title";
                cmd = new SqlCommand(cmdText, con);
            }
            // Sort BookID in ascending order
            else if (cmbSort.SelectedIndex == 1)
            {
                string cmdText = "SELECT * FROM booksData ORDER BY BookID";
                cmd = new SqlCommand(cmdText, con);
            }
            // Sort Genre in alphabetical order. Not useful atm.
            else if (cmbSort.SelectedIndex == 2)
            {
                string cmdText = "SELECT * FROM booksData ORDER BY Genre";
                cmd = new SqlCommand(cmdText, con);
            }

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvBooks.DataSource = dt;
            con.Close();
        }
    }
}
