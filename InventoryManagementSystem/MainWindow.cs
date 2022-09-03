namespace InventoryManagementSystem
{
    public partial class MainWindow : Form
    {
        private bool is_admin = false;
        private string user;
        public MainWindow(bool is_admin, string user)
        {
            InitializeComponent();
            this.is_admin = is_admin;
            this.user = user;
            
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

            if (!is_admin)
            {
                // if user is not admin
                btnUsers.Visible = false;
                btnProducts.Visible = false;
                lblTitle.Text = "Staff Dashboard";
            }
            else
            {
                // if user is admin
                lblTitle.Text = "Admin Dashboard";
                openChildForm(new Forms.MainDash());
            }

            userLabel.Text = user;
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Users";
            openChildForm(new Forms.Users());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Categories";
            openChildForm(new Forms.Categories());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Products";
            openChildForm(new Forms.Products());
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Order";
            openChildForm(new Forms.OrderForm());
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Sales";
            openChildForm(new Forms.Sales());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userLabel_Click(object sender, EventArgs e)
        {

        }
    }
}