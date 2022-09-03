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
    public partial class OrderForm : Form
    {
        private List<Product> products = Product.GetProducts();

        // product names with quantity
        private Dictionary<string, int> basket = new Dictionary<string, int>();
        // product name with quantity
        private Dictionary<string, int> basketWithCode = new Dictionary<string, int>();
        public OrderForm()
        {
            InitializeComponent();
        }

        private void UpdateSearchDataGridView()
        {   

            // clear dataGridView
            dgvSearch.Rows.Clear();

            for (int i = 0; i < products.Count; i++)
            {
                Product product = products[i];
                dgvSearch.Rows.Add(product.code, product.name, product.retail_price);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            // load products search table
            this.UpdateSearchDataGridView();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dgvSearch.Rows.Clear();

            for (int i = 0; i < products.Count; i++)
            {
                Product product = products[i];

                // if product name contains search term
                if (product.name.ToLower().Contains(textBoxSearch.Text.ToLower())) {
                    dgvSearch.Rows.Add(product.code, product.name, product.retail_price); }
            }
        }

        private void dgvSearch_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0)
            {
                // if delete is clicked 
                if (e.ColumnIndex == this.colAddBtn.Index)
                {
                    // retrieve code and name
                    string code = dgvSearch.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string name = dgvSearch.Rows[e.RowIndex].Cells[1].Value.ToString();

                    textBoxBarcode.Text = code;
                    textBoxProduct.Text = name;

                    textBoxProduct.Enabled = false;
                    textBoxBarcode.Enabled = false;

                    numericQuantity.Value = 1;

                }
               
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            textBoxBarcode.Enabled = true;
            textBoxProduct.Enabled = true;

            // clear
            textBoxProduct.Clear();
            textBoxBarcode.Clear();
            numericQuantity.Value = 0;
        }

        private void btnAddBasket_Click(object sender, EventArgs e)
        {
            string code = textBoxBarcode.Text;
            string name = textBoxProduct.Text;
            int quantity = Convert.ToInt32(numericQuantity.Value);

            // add to basket
            if (basket.Keys.Contains(name)) basket[name] += quantity;
            else basket.Add(name, quantity);

            // add to basketWithCode
            if (basketWithCode.Keys.Contains(code)) basketWithCode[code] += quantity;
            else basketWithCode.Add(code, quantity);

            // update basket
            dgvBasket.Rows.Clear();

            
            for (int i = 0; i < basket.Count; i++)
            {
                dgvBasket.Rows.Add(basket.Keys.ElementAt(i), basket.Values.ElementAt(i));

            }

            // update total
            decimal total_price = 0;
            for (int i = 0; i < basket.Count; i++)
            {
                string product_name = basket.Keys.ElementAt(i);
                int prod_quantity = basket.Values.ElementAt(i);

                for (int j = 0; j < products.Count; j++)
                {
                    Product product = products[j];

                    // if product name contains search term
                    if (product.name.ToLower() == product_name.ToLower())
                    {
                        total_price += (decimal) product.retail_price * prod_quantity;
                    }
                }

            }

            // update the label Total
            lblTotal.Text = total_price.ToString();
        }

        // Event Handler for Process Sale Button
        private void button1_Click_1(object sender, EventArgs e)
        {
            // get the total price
            String total = lblTotal.Text;

            // check payment and change
            decimal paid = numericPaid.Value;


            int saveSales;
            if (paid >= decimal.Parse(total))
            {
                numericChange.Value = paid - decimal.Parse(total);

                // save order
                saveSales = Sale.AddSale(basketWithCode);

                // pass total price and basket data to Invoice Form

                Form receiptForm = new Reciept(basket, total);
                receiptForm.Show();
            }
            else
            {
                saveSales = 0;
            }

            

            if (saveSales > 0)
            {

                // clear
                lblTotal.Text = "";
                textBoxProduct.Clear();
                textBoxBarcode.Clear();
                numericQuantity.Value = 1;
                numericPaid.Value = 0;
                numericChange.Value = 0;

                textBoxBarcode.Enabled = true;
                textBoxProduct.Enabled = true;


                // update product quantities


                // clear basket
                dgvBasket.Rows.Clear();

                // empty basket
                basket.Clear();
            }

        }

        private void textBoxSearch_TextChanged_1(object sender, EventArgs e)
        {
            dgvSearch.Rows.Clear();

            for (int i = 0; i < products.Count; i++)
            {
                Product product = products[i];

                // if product name contains search term
                if (product.name.ToLower().Contains(textBoxSearch.Text.ToLower()))
                {
                    dgvSearch.Rows.Add(product.code, product.name, product.retail_price);
                }
            }
        }

    }
}
