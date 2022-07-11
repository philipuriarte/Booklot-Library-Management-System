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
        DataTable dt;
        

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home newTab = new Home();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBookID.Text)) //checks if txtBookID field is empty
            {
                MessageBox.Show("Please enter Book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
                con.Open();
                string availText = "Avail";
                int bookID = Convert.ToInt32(txtBookID.Text);
                cmd = new SqlCommand("SELECT * FROM book WHERE book_id = '" + bookID + "'", con); 
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0) //check if bookID entered matches any in book_id column from book
                {
                    cmd = new SqlCommand("SELECT * FROM book WHERE book_id = '" + bookID + "' AND status = 'Avail'", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0) //check if bookID matches and Status is 'avail'
                    {
                        MessageBox.Show("Book ID" + bookID + " has not been issued and is currently available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookID.Clear();
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE book SET status = '" + availText + "' WHERE book_id = '" + bookID + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("UPDATE borrow_data SET member_id = NULL WHERE book_id = '" + bookID + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("UPDATE borrow_data SET borrow_date = NULL WHERE book_id = '" + bookID + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("UPDATE borrow_data SET return_date = NULL WHERE book_id = '" + bookID + "'", con);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Successfully returned book ID " + bookID, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();

                        txtBookID.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Book ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBookID.Clear();
                }
            }
        }
    }
}
