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
    public partial class Form2 : Form
    {
        private string Codigo = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, 10);
            label2.Location = new Point(this.Width / 2 - label2.Width / 2, label1.Height + 11);
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, label1.Height + label2.Height + 11);
            label4.Location = new Point(this.Width / 2 - label4.Width / 2, label1.Height + label2.Height + label3.Height + 11);

            timer1.Enabled = true;


        }

        private void buscar(String texto)
        {
            string[] infoproducto;
            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader("Productos.csv");

            while ((line = file.ReadLine())!= null)
            {
                infoproducto = line.Split(',');
                if(texto == infoproducto[0])
                {
                    label4.Text = "Nombre: " + infoproducto[1] + "  Precio $ " + infoproducto[2];
                    label4.Location = new Point(this.Width / 2 - label4.Width / 2, label1.Height + label2.Height + label3.Height + 11);
                    label4.ForeColor = Color.Green;
                    break;
                }
                else
                {
                    label4.Text = "Producto no encontrado";
                    label4.ForeColor = Color.Red;
                    
                }
            }

            file.Close();



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                Codigo += e.KeyChar;

            if (e.KeyChar == 13)
            {
                buscar(Codigo.Trim());
                Codigo = "";


            }
        }
    }
}   
