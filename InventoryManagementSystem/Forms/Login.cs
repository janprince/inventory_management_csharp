using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem.Forms
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            Dictionary<string, string> authenticatUser = User.AuthenticateUser(username, password);
            if (authenticatUser["auth"] == "1")
            {
                Form main = new MainWindow(is_admin: authenticatUser["role"].ToLower() == "admin", user: authenticatUser["name"]);
                main.Show();
            }
            //this.Close();

            //Form main = new MainWindow();
            //main.Show();
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
