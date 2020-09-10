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
    public partial class GetUserByPublicId : Form
    {
        public GetUserByPublicId()
        {
            InitializeComponent();
        }

        public GetUserByPublicId(string id, string publicid, string username, string password, string admin, string address)
        {
            InitializeComponent();

            lbId.Text = id;
            lbPublicId.Text = publicid;
            lbUsername.Text = username;
            lbPassword.Text = "Encryted";
            lbAddress.Text = address;
            lbAdmin.Text = admin;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void GetUserByPublicId_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}
