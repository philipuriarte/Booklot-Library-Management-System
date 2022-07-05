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
        SqlCommand cmd, cmd0;
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
            if (String.IsNullOrEmpty(txtBookID.Text))
            {
                MessageBox.Show("Please enter a Book ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                con = new SqlConnection("Data Source=LAPTOP-SBHT2OEG\\SQLEXPRESS;Initial Catalog=libraryData;Integrated Security=True");
                con.Open();
                int bookID = Convert.ToInt32(txtBookID.Text);
                bool idExist = false;

                cmd = new SqlCommand("SELECT * FROM booksData", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == txtBookID.Text)
                    {
                        idExist = true;
                        break;
                    }
                }
                if (idExist == true)
                {
                    con.Close();
                    con.Open();

                    cmd0 = new SqlCommand("DELETE FROM booksData WHERE BookID = '" + bookID + "'", con);
                    cmd0.ExecuteNonQuery();
                    MessageBox.Show("Succesfully deleted book ID: " + bookID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    txtBookID.Clear();
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