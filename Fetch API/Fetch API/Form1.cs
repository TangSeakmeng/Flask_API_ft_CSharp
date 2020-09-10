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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Name = txtUsername.Text.Trim();
            user.Password = txtPassword.Text.Trim();
            await user.Login(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}
