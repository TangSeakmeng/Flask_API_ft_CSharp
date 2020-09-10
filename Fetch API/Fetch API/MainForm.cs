using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fetch_API
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManagement um = new UserManagement();
            um.Show();
            this.Dispose();
        }

        Menu menu;
        MenuCategory menuCategory;

        private async void MainForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            menuCategory = new MenuCategory();
            await menuCategory.GetMenuCategoriesName(comboBox1);
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            MenuManagement mcm = new MenuManagement();
            mcm.Show();
        }

        private void categoryManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            MenuCategoryManagement mcm = new MenuCategoryManagement();
            mcm.Show();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Subtotal: " + lbTotal.Text);
            dataGridViewData.Rows.Clear();
            lbTotal.Text = "0";
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            menu = new Menu();
            await menu.GetMenusWithCategoryNameGenerateUserControl(flowLayoutPanel1, dataGridViewData, lbTotal, comboBox1.SelectedItem.ToString());
        }

        private async void btnGetAll_Click(object sender, EventArgs e)
        {
            menu = new Menu();
            await menu.GetMenusGenerateUserControl(flowLayoutPanel1, dataGridViewData, lbTotal);
        }
    }
}
