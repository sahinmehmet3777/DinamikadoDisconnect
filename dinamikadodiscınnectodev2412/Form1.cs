using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dinamikadodiscınnectodev2412
{
    public partial class Form1 : Form
    {
        // nesnelerimi tanımladım
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private Button reloadButton = new Button();
        private Button submitButton = new Button();
        public Form1()
        {
            InitializeComponent();
            reloadButton.Text = "Yenileme";
            submitButton.Text = "Kaydet";
            reloadButton.Click += new System.EventHandler(reloadButton_Click);
            submitButton.Click += new System.EventHandler(submitButton_Click);
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.Dock = DockStyle.Top;
            panel.AutoSize = true;
            dataGridView1.Dock = DockStyle.Fill;
            panel.Controls.AddRange(new Control[] { reloadButton, submitButton });
            this.Controls.AddRange(new Control[] { panel, dataGridView1 });
            this.Load += new System.EventHandler(Form1_Load);
            this.Text = "Listelenen verilerin yenilenmesi ve kaydedilmesi";
        }
        private void reloadButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sülüman sana bi mesaj gönderdim BAK.");
            GetData(dataAdapter.SelectCommand.CommandText);
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            dataAdapter.Update((DataTable)bindingSource1.DataSource);
        }
        private void GetData(string selectCommand)
        {
            //Data Source=216-08\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa; Password=Fbu123456
            try
            {
                //"Server = DESKTOP-7EA7R1P;Initial Catalog=Northwind;Trusted_Connection=True";
                // "Data Source=216-08\\SQLEXPRESS;Initial Catalog=Northwind;User ID=sa; Password=Fbu123456";
                string connectionstring = "Server = DESKTOP-7EA7R1P;Initial Catalog=Northwind;Trusted_Connection=True";
                //veri tabnındaki tüm bilgi data adapterde tutulur.
                dataAdapter = new SqlDataAdapter(selectCommand, connectionstring);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.DataSource = bindingSource1;
            GetData("Select *from Customers");
        }
    }
}
