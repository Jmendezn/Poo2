using LibPrintTicket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poo2
{
    public partial class Punto_venta : Form
    {
        public Punto_venta()
        {
            InitializeComponent();
        }

        private double Total = 0, Monedas = 0, cambio = 0, subtotal = 0;
        bool Valid = false;
        private string[] CantidadProducto;

        private void Punto_venta_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, 10);
            label2.Location = new Point(this.Width / 2 - label2.Width / 2, label1.Height + 11);
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, label1.Height + label2.Height + 11);
            dataGridView1.Location = new Point(1, label1.Height + label2.Height + label3.Height + 21);
            dataGridView1.Width = this.Width - 2;
            dataGridView1.Height = int.Parse(this.Height * 0.75 + "");
            label4.Text = "TOTAL: $0.00";


            textBox1.Location = new Point(1, this.Height - textBox1.Height);
            label4.Location = new Point(this.Width - label4.Width, this.Height - textBox1.Height - label4.Height);
            textBox1.Width = this.Width - 2;
            timer1.Enabled = true;
            Flx_cols("1");
            textBox1.Focus();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void Flx_cols(String Fsel)
        {

            switch (Fsel)
            {
                case "1":
                    dataGridView1.ColumnCount = 4;
                    dataGridView1.ColumnHeadersVisible = true;
                    DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

                    columnHeaderStyle.BackColor = Color.Beige;
                    columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                    dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;


                    dataGridView1.Columns[0].Name = "Cantidad";
                    dataGridView1.Columns[1].Name = "Producto";
                    dataGridView1.Columns[2].Name = "Precio";
                    dataGridView1.Columns[3].Name = "Importe";

                    dataGridView1.Columns[0].Width = Convert.ToInt32(this.Width * 0.15f);
                    dataGridView1.Columns[1].Width = Convert.ToInt32(this.Width * 0.35f);
                    dataGridView1.Columns[2].Width = Convert.ToInt32(this.Width * 0.20f);
                    dataGridView1.Columns[3].Width = Convert.ToInt32(this.Width * 0.20f);

                    break;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (textBox1.Text.IndexOf("*") == -1)
                {
                    //CantidadProducto = textBox1.Text.Split('*');
                    //MessageBox.Show(CantidadProducto.ToString());
                    Buscar(1, textBox1.Text);
                }
                else
                {
                    CantidadProducto = textBox1.Text.Split('*');
                    Buscar(int.Parse(CantidadProducto[1]), CantidadProducto[0]);
                }
                textBox1.Clear();
                textBox1.Focus();

            }

            if (e.KeyChar == 27)
            {
                Eliminar();
                ImporteF();
            }

            if (e.KeyChar == 32)
            {
                Duplicar();
                ImporteF();
            }

            if (e.KeyChar.ToString() == "P")
            {
                Cobrar();
                ImporteF();
            }



        }

        private void Buscar(int cantidad, string producto)
        {
            String[] infoProducto;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader("Productos.csv");
            while ((line = file.ReadLine()) != null)
            {
                infoProducto = line.Split(',');
                if (producto == infoProducto[0])
                {
                    dataGridView1.Rows.Add(cantidad, infoProducto[1], infoProducto[2], cantidad * double.Parse(infoProducto[2]));

                    Total = Total + cantidad * Convert.ToDouble(infoProducto[2]);

                    ImporteF();
                    Valid = true;
                }
            }
            if (Valid == false)
            {
               
            }
            file.Close();
        }


        private void Cobrar()
        {
            Monedas = Convert.ToDouble(textBox1.Text);
            subtotal += this.Total;


            this.Total = Convert.ToDouble(textBox1.Text) - this.Total;
            cambio = Total;
            label4.Text = "Cambio: $" + Total;
            label4.Location = new Point(this.Width - 120, this.Height - label4.Height - 60);
            textBox1.Clear();

            Print();
            dataGridView1.Rows.Clear();
            textBox1.Clear();
        }


        private void ImporteF()
        {
            double total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                total += Convert.ToDouble(dataGridView1[3, i].Value.ToString());
            }
            label4.Text = "Total: $ " + total.ToString("n");

            label4.Location = new Point(this.Width - label4.Width + 2,
                this.Height - textBox1.Height - label4.Height);
        }


        private void Duplicar()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Add(dataGridView1[0, dataGridView1.Rows.Count - 1].Value.ToString(),
                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value.ToString(),
                    dataGridView1[2, dataGridView1.Rows.Count - 1].Value.ToString(),
                    dataGridView1[3, dataGridView1.Rows.Count - 1].Value.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Eliminar()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }
        }

        private void Print()
        {
            Ticket ticket = new Ticket();
            ticket.HeaderImage = Image.FromFile(@"C:\Users\jhmn_\source\repos\poo2\poo2\logo.jpg");
            ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + "" + DateTime.Now.ToShortTimeString());



            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                ticket.AddItem(dataGridView1[0, i].Value.ToString(), dataGridView1[1, i].Value.ToString(), dataGridView1[3, i].Value.ToString());
            }

            ticket.AddFooterLine("TOTAL: $" + subtotal.ToString("n"));
            ticket.AddFooterLine("PAGO: $" + Monedas.ToString("n"));
            ticket.AddFooterLine("CAMBIO: $" + Total.ToString("n"));
            ticket.AddFooterLine("Gracias por su compra");
            ticket.AddFooterLine("Vuelve pronto");
            ticket.AddFooterLine("EC-PM-5890X");
            ticket.PrintTicket("EC-PM-5890X");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
