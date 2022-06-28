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
    public partial class ReturnTab : Form
    {
        public ReturnTab()
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
            con.Open();

            int bookID = Convert.ToInt32(txtBookID.Text);
            string availText = "avail";

            string cmdText = "UPDATE booksData SET Status = '" + availText + "' WHERE BookID = '" + bookID + "'";
            cmd = new SqlCommand(cmdText, con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully returned book ID: " + bookID);
            con.Close();

            txtBookID.Clear();
        }
    }
}
