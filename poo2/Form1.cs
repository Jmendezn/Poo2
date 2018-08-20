using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace poo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int seleCt;

        class Productos
        {

            // calis
            // Propiedades
            public long producto_codigo = 0;
            public string producto_nombre = "";
            public float producto_precio = 0.00f;

            // Constructor
            public Productos()
            {
                MessageBox.Show("me llamaron sin parametros");
            }

            public Productos(long producto_codigo, string producto_nombre, float producto_precio)
            {
                // MessageBox.Show("me llamaron con long, string y float");
                this.producto_codigo = producto_codigo;
                this.producto_nombre = producto_nombre;
               // this.producto_precio = producto_precio;
            }
            public Productos(long producto_codigo, string producto_nombre, double producto_precio)
            {
                //MessageBox.Show("me llamaron con long, string y double");
                this.producto_codigo = producto_codigo;
                this.producto_nombre = producto_nombre;
                this.producto_precio = (float)producto_precio;
            }

        }

        private void Strt_1 ()
        {
            string[] infoproducto;
            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader("Productos.csv");
            dataGridView1.RowCount = 0;
            while ((line = file.ReadLine()) != null)
            {
                infoproducto = line.Split(',');
                dataGridView1.Rows.Add(line.Split(','));


            }

            file.Close();
        }

        private List<Productos> listaProductos = new List<Productos>();
        Productos[] productos = new Productos[10];

        private void CargaProductos()
        {
            dataGridView1.RowCount = 1;

            dataGridView1.Columns["Column3"].DefaultCellStyle.Format = "N2" ;
            //for (int i = 0; i < productos.Length; i++)
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    dataGridView1.Rows.Add(productos[i].producto_codigo, productos[i].producto_nombre, productos[i].producto_precio);

                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Productos productos = new Productos();
             productos.producto_codigo = 12345698764;
             productos.producto_nombre = "Miller";
             productos.producto_precio = 36.00f;

             richTextBox1.Text += productos.producto_codigo + " " + productos.producto_nombre + " " + productos.producto_precio;
             */
            // Productos productos = new Productos();
            // Productos productos = new Productos(1,"kaguamon", 36.00);
            //  Productos productos = new Productos();

            //productos[0] = new Productos(10, "kaguama 1", 32.05);
            //productos[1] = new Productos(11, "kaguama 2", 66.00);
            //productos[2] = new Productos(12, "kaguama 3", 33.10);
            // CargaProductos();

            Strt_1();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            // AGREGAR
            // CLAVE + NOMBRE PRODUCTO + PU + CANT
           
            bool REp = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1[0, i].Value.ToString() == textBox2.Text)
                {
                    REp = true;
                    break;
                }
            }
             
            if (REp == false)
            {
                dataGridView1.Rows.Add(textBox2.Text, textBox1.Text, textBox3.Text, textBox5.Text);
               // dataGridView1_Numerar();
            }
            else
            {
                MessageBox.Show("Codigo ya ingresado", (MessageBoxButtons.OK).ToString());
                Limpiar();
            }

        }

        private void Limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox2.Focus();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // DELETE
            // CLAVE + NOMBRE PRODUCTO + PU + CANT
           
           
            if (MessageBox.Show("Desea eliminar " + dataGridView1[1,
                   seleCt].Value.ToString() + " con Precio de : "
                   + dataGridView1[2, seleCt].Value.ToString(), "Titulo de la ventana",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.RemoveAt(seleCt);
            }
            Limpiar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Update
            if (MessageBox.Show("Desea modificar " + dataGridView1[1,
                seleCt].Value.ToString() + " con Precio de : "
                + textBox5.Text, "Titulo de la ventana",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1[1, seleCt].Value = textBox1.Text;
                dataGridView1[2, seleCt].Value = textBox2.Text;
            }
            Limpiar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                textBox2.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                textBox1.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                textBox3.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                textBox4.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                seleCt = e.RowIndex;
                textBox2.Enabled = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataGridView1.SelectAll();
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            DataObject dataObject = dataGridView1.GetClipboardContent();
            File.WriteAllText("Productos.csv", dataObject.GetText(TextDataFormat.CommaSeparatedValue));
        }
    }

  
}
