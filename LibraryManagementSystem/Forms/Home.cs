using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        // View all books button
        private void btnView_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewTab newTab = new ViewTab();
            newTab.ShowDialog();
            this.Close();
        }

        // Add book button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddTab newTab = new AddTab();
            newTab.ShowDialog();
            this.Close();
        }

        // Delete book button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Hide();
            DeleteTab newTab = new DeleteTab();
            newTab.ShowDialog();
            this.Close();
        }

        // Issue book button
        private void btnIssue_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forms.IssueTab newTab = new Forms.IssueTab();
            newTab.ShowDialog();
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forms.ReturnTab newTab = new Forms.ReturnTab();
            newTab.ShowDialog();
            this.Close();
        }
    }
}
