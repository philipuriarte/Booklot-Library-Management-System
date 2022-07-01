using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystem.Forms
{
    public partial class LoginTab : System.Windows.Forms.Form
    {
        public LoginTab()
        {
            InitializeComponent();
        }
        
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Checks if any txtBoxes are empty
            if (String.IsNullOrEmpty(txtUser.Text) || String.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Please enter username and password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                con = new SqlConnection("Data Source = DESKTOP-9MBNT14\\SQLEXPRESS; Initial Catalog = libraryData; Integrated Security = True");
                con.Open();
                cmd = new SqlCommand("SELECT * FROM loginData WHERE username = '" + txtUser.Text + "' AND password ='" + txtPass.Text + "'", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                // Checks if any entered username and password is correct/within the database.
                // Revert if condition back to "if (dt.Rows.Count > 0)" later.
                if (txtUser.Text == "admin" & txtPass.Text == "password123")
                {
                    this.Hide();
                    Home newTab = new Home();
                    newTab.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Incorrect username or password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUser.Clear();
                    txtPass.Clear();
                }
            }
        }

        // Checks if the user pressed enter in txtPass
        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        // Checks if the user pressed enter in txtUser
        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }

}
