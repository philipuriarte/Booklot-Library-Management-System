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
            con = new SqlConnection("Data Source = DESKTOP-9MBNT14\\SQLEXPRESS; Initial Catalog = libraryData; Integrated Security = True");
            con.Open();
            cmd = new SqlCommand("SELECT * FROM loginData WHERE username = '" + txtUser.Text + "' AND password ='" + txtPass.Text + "'", con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Login Success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Hide();
                Home newTab = new Home();
                newTab.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Error", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

}
