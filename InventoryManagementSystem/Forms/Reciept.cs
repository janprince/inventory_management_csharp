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
    public partial class Reciept : Form
    {
        private Dictionary<String, int> basket;
        private String total;
        public Reciept(Dictionary<string, int> basket, string total)
        {
            InitializeComponent();
            this.basket = basket;
            this.total = total;
        }

        private void UpdateDataGridView()
        {

            // clear dataGridView
            dgvInvoice.Rows.Clear();

            for (int i = 0; i < basket.Count; i++)
            {
                dgvInvoice.Rows.Add(basket.Keys.ElementAt(i), basket.Values.ElementAt(i));

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            lblTotal.Text = total.ToString();
            this.UpdateDataGridView();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
