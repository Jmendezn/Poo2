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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        private List<Productos> listaProductos = new List<Productos>();
        Productos[] productos = new Productos[10];

        private void CargaProductos()
        {
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
            
            productos[0] = new Productos(10, "kaguama 1", 32.05);
            productos[1] = new Productos(11, "kaguama 2", 66.00);
            productos[2] = new Productos(12, "kaguama 3", 33.10);
            CargaProductos();
        }
        
    }

  
}
