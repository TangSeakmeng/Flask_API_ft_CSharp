using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fetch_API
{
    public partial class MenuCategoryManagement : Form
    {
        public MenuCategoryManagement()
        {
            InitializeComponent();
        }

        MenuCategory menuCategory;

        private async void btnAddCategory_Click(object sender, EventArgs e)
        {
            if((txtMenuCategoryName.Text).Length == 0)
            {
                MessageBox.Show("Invalid Input! Please input any text!", "Add Menu Category");
                return;
            }

            menuCategory = new MenuCategory();
            menuCategory.Name = txtMenuCategoryName.Text;
            await menuCategory.createMenuCategory();

            menuCategory = new MenuCategory();
            await menuCategory.GetMenuCategories(dataGridViewMenuCategory);

            btnReset_Click(null, null);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtMenuCategoryName.Clear();
        }

        private async void MenuCategory_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            txtId.Enabled = false;

            menuCategory = new MenuCategory();
            await menuCategory.GetMenuCategories(dataGridViewMenuCategory);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Dispose();
        }

        private void dataGridViewMenuCategory_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridViewMenuCategory.SelectedRows.Count > 0)
            {
                int index = dataGridViewMenuCategory.SelectedRows[0].Index;

                txtId.Text = dataGridViewMenuCategory.Rows[index].Cells[0].Value.ToString();
                txtMenuCategoryName.Text = dataGridViewMenuCategory.Rows[index].Cells[1].Value.ToString();
            }
            else
            {
                btnReset_Click(null, null);
            }
        }

        private async void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            if (dataGridViewMenuCategory.SelectedRows.Count > 0 && txtId.Text.Length > 0)
            {
                menuCategory = new MenuCategory();
                menuCategory.Id = int.Parse(txtId.Text);
                menuCategory.Name = txtMenuCategoryName.Text;
                await menuCategory.updateMenuCategory();

                menuCategory = new MenuCategory();
                await menuCategory.GetMenuCategories(dataGridViewMenuCategory);

                btnReset_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please select any row to delete");
            }
        }

        private async void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (dataGridViewMenuCategory.SelectedRows.Count > 0)
            {
                menuCategory = new MenuCategory();
                await menuCategory.deleteMenuCategory(dataGridViewMenuCategory);

                menuCategory = new MenuCategory();
                await menuCategory.GetMenuCategories(dataGridViewMenuCategory);

                btnReset_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please select any row to delete");
            }
        }
    }
}
