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
    public partial class ManageTab : Form
    {
        public ManageTab()
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

        // Loads the SQL member table to dataGrid when ManageTab form is launched
        private void ManageTab_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
            con.Open();

            cmd = new SqlCommand("SELECT * FROM member", con);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dgvMembers.DataSource = dt;
            con.Close();
        }

        // Loads the SQL member table to dataGrid
        private void rbtnMembers_CheckedChanged(object sender, EventArgs e)
        {
            ManageTab_Load(sender, e);
        }

        // Loads mem_id, name, borrow_date, and return date where member.mem_id matches with borrow_data.member_id table to dataGrid
        private void rbtnTransac_CheckedChanged(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=" + Program.globalServer + "\\SQLEXPRESS;Initial Catalog=LibDat;Integrated Security=True");
            con.Open();

            cmd = new SqlCommand("SELECT member.mem_id, member.name, borrow_data.borrow_date, borrow_data.return_date FROM member INNER JOIN borrow_data ON member.mem_id=borrow_data.member_id", con);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dgvMembers.DataSource = dt;
            con.Close();
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddMemberTab newTab = new AddMemberTab();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            DeleteMemberTab newTab = new DeleteMemberTab();
            newTab.ShowDialog();
            this.Close();
        }
    }
}
