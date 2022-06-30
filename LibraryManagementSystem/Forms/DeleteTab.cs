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
        SqlCommand cmd;
        SqlDataAdapter da;

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home newTab = new Home();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-9MBNT14\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
            con.Open();
            int bookID = Convert.ToInt32(txtBookID.Text);

            cmd = new SqlCommand("DELETE FROM booksData WHERE BookID = '" + bookID + "'", con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Successfully deleted book ID: " + bookID);
            con.Close();

            txtBookID.Clear();
        }

        private void DeleteTab_Load(object sender, EventArgs e)
        {

        }
    }
}
