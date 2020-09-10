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
    public partial class UserManagement : Form
    {
        public UserManagement()
        {
            InitializeComponent();
        }

        private User user;

        private async void UserManagement_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            radioButtonTrue.Checked = true;
            txtId.Enabled = false; 
            txtPublicId.Enabled = false;

            user = new User();
            await user.GetUsers(dataGridViewUser);
        }

        private void dataGridViewUser_MouseClick(object sender, MouseEventArgs e)
        {
            if(dataGridViewUser.SelectedRows.Count > 0)
            {
                int index = dataGridViewUser.SelectedRows[0].Index;

                txtId.Text = dataGridViewUser.Rows[index].Cells[0].Value.ToString();
                txtPublicId.Text = dataGridViewUser.Rows[index].Cells[1].Value.ToString();
                txtUsername.Text = dataGridViewUser.Rows[index].Cells[2].Value.ToString();
                txtPassword.Text = dataGridViewUser.Rows[index].Cells[3].Value.ToString();

                if (dataGridViewUser.Rows[index].Cells[4].Value.ToString() == "true")
                    radioButtonTrue.Checked = true;
                else
                    radioButtonFalse.Checked = true;

                txtAddresss.Text = dataGridViewUser.Rows[index].Cells[5].Value.ToString();

                txtId.Enabled = false;
                txtPublicId.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                btnReset_Click(null, null);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Dispose();
        }

        private async void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if(dataGridViewUser.SelectedRows.Count > 0)
            {
                user = new User();
                await user.deleteUser(dataGridViewUser);

                user = new User();
                await user.GetUsers(dataGridViewUser);

                btnReset_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please select any row to delete");
            }
        }   

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            user = new User();
            user.Name = txtUsername.Text;
            user.Address = txtAddresss.Text;
            user.Admin = radioButtonTrue.Checked ? "true" : "false";
            user.Password = txtPassword.Text;
            await user.createUser();

            user = new User();
            await user.GetUsers(dataGridViewUser);

            btnReset_Click(null, null);
        }

        private async void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUser.SelectedRows.Count > 0)
            {
                user = new User();
                user.Name = txtUsername.Text;
                user.Address = txtAddresss.Text;
                user.Admin = radioButtonTrue.Checked ? "true" : "false";
                user.Public_id = txtPublicId.Text;
                await user.updateUser();

                user = new User();
                await user.GetUsers(dataGridViewUser);

                btnReset_Click(null, null);
            }
            else
            {
                MessageBox.Show("Please select any row to delete");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtPublicId.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtAddresss.Text = "";
            radioButtonTrue.Checked = true;
            txtPassword.Enabled = true;
        }

        private void dataGridViewUser_SelectionChanged(object sender, EventArgs e)
        {
            //dataGridViewUser.CurrentCell.RowIndex.ToString();
            //MessageBox.Show(dataGridViewUser.SelectedRows.Count.ToString());

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            user = new User();
            await user.getUserbyPublicId(txtSearchPublicId.Text);
        }
    }
}
