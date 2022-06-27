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
            txtBookID.Clear();
            txtTitle.Clear();
            txtAuthor.Clear();
            txtEdition.Clear();
            txtPublication.Clear();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
            con.Open();

            int bookID = Convert.ToInt32(txtBookID.Text);
            string title = txtTitle.Text;
            string author = txtAuthor.Text;
            string edition = txtEdition.Text;
            string publication = txtPublication.Text;

            string cmdText = "INSERT INTO booksData VALUES ('" + bookID + "','" + title + "','" + author + "','" + edition + "','" + publication + "','" + "avail" + "')";
            cmd = new SqlCommand(cmdText, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully added one book.");
            con.Close();

            txtBookID.Clear();
            txtTitle.Clear();
            txtAuthor.Clear();
            txtEdition.Clear();
            txtPublication.Clear();
        }
    }
}
