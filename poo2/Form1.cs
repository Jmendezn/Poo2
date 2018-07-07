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
            }

        }

        private List<Productos> listaProductos = new List<Productos>();

        private void CargaProductos()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           /* Productos productos = new Productos();
            productos.producto_codigo = 12345698764;
            productos.producto_nombre = "Miller";
            productos.producto_precio = 36.00f;

            richTextBox1.Text += productos.producto_codigo + " " + productos.producto_nombre + " " + productos.producto_precio;
            */

        }
    }

  
}
