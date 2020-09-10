using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fetch_API
{
    public partial class MenuManagement : Form
    {
        public MenuManagement()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Dispose();
        }

        MenuCategory menuCategory;
        Menu menu;
        List<MenuCategory> menuCategories;

        private async void MenuManagement_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            txtId.Enabled = false;

            menuCategory = new MenuCategory();
            menuCategories = await menuCategory.GetMenuCategoriesName(comboBoxMenuCategory);

            menu = new Menu();
            await menu.GetMenus(dataGridViewMenu, menuCategories);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtMenuName.Clear();
            txtPrice.Clear();
            comboBoxMenuCategory.SelectedIndex = 0;
            pictureBoxImage.Image = Properties.Resources.http_error_404_not_found;

            pictureFilename = "none";
        }

        string pictureFilename = "none";

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
            openFileDialog.InitialDirectory = @"D:\";
            DialogResult result = openFileDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                pictureBoxImage.ImageLocation = filename;
                pictureFilename = filename;
            }
        }

        int categoryId = 0;

        private async void comboBoxMenuCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            menuCategory = new MenuCategory();
            categoryId = await menuCategory.getMenuCategoryIdByName(comboBoxMenuCategory.SelectedItem.ToString());
        }

        private async void btnAddMenu_Click(object sender, EventArgs e)
        {
            menu = new Menu();
            menu.Name = txtMenuName.Text;
            menu.Price = double.Parse(txtPrice.Text);
            menu.Image = pictureFilename;
            menu.Menucategory_id = categoryId.ToString();
            await menu.createMenu();

            menu = new Menu();
            await menu.GetMenus(dataGridViewMenu, menuCategories);

            btnReset_Click(null, null);
        }

        private async void btnUpdateMenu_Click(object sender, EventArgs e)
        {
            if (dataGridViewMenu.SelectedRows.Count > 0)
            {
                menu = new Menu();
                menu.Id = int.Parse(txtId.Text);
                menu.Name = txtMenuName.Text;
                menu.Price = double.Parse(txtPrice.Text);
                menu.Menucategory_id = getCategoryId().ToString();
                MessageBox.Show(pictureFilename);
                menu.Image = pictureFilename;
                await menu.updateMenu();

                menu = new Menu();
                await menu.GetMenus(dataGridViewMenu, menuCategories);

                btnReset_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please select any row to delete");
            }
        }

        private int getCategoryId()
        {
            int id = 0;

            for(int index = 0; index < menuCategories.Count; index++)
            {
                if (menuCategories[index].Name == comboBoxMenuCategory.SelectedItem.ToString())
                    id = menuCategories[index].Id;
            }

            return id;
        }

        private async void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            if (dataGridViewMenu.SelectedRows.Count > 0)
            {
                menu = new Menu();
                await menu.deleteMenu(dataGridViewMenu);
                await menu.GetMenus(dataGridViewMenu, menuCategories);

                btnReset_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please select any row to delete");
            }
        }

        private void dataGridViewMenu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewMenu.SelectedRows.Count > 0)
            {
                int index = dataGridViewMenu.SelectedRows[0].Index;

                txtId.Text = dataGridViewMenu.Rows[index].Cells[0].Value.ToString();
                txtMenuName.Text = dataGridViewMenu.Rows[index].Cells[1].Value.ToString();
                comboBoxMenuCategory.SelectedItem = dataGridViewMenu.Rows[index].Cells[2].Value.ToString();
                txtPrice.Text = dataGridViewMenu.Rows[index].Cells[3].Value.ToString();
                pictureBoxImage.Image = (Image)dataGridViewMenu.Rows[index].Cells[4].Value;

                pictureFilename = "none";
            }
            else
            {
                btnReset_Click(null, null);
            }
        }
    }
}
