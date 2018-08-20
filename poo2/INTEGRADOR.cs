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
    public partial class INTEGRADOR : Form
    {
        

        public INTEGRADOR()
        {
            InitializeComponent();
            
    }

        private void INTEGRADOR_Load(object sender, EventArgs e)
        {
            
            int oP1 = 0;
            oP1 = Convert.ToInt32(pictureBox1.Width)  - 200;

            label3.Text = DateTime.Now.ToString();
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, 10);
            label2.Location = new Point(this.Width / 2 - label2.Width / 2, label1.Height + 11);
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, label1.Height + label2.Height + 11);
            pictureBox1.Location = new Point(this.Width / 2 - oP1 , label1.Height + label2.Height + label3.Height + 100);

            label5.Location = new Point(this.Width / 2 - label5.Width / 2, label1.Height + label2.Height + pictureBox1.Height + label3.Height + 150);
            label4.Location = new Point(this.Width / 2 - label4.Width / 2, label1.Height + label2.Height + pictureBox1.Height + label3.Height + label3.Height + 170);
            label6.Location = new Point(this.Width / 2 - label6.Width / 2, label1.Height + label2.Height + pictureBox1.Height + label3.Height + label4.Height + label5.Height + 190);
            label7.Location = new Point(this.Width / 2 - label7.Width / 2, label1.Height + label2.Height + pictureBox1.Height + label3.Height + label4.Height + label5.Height + label6.Height + 210);

            //pictureBox1.Width = this.Width - 2;
            //pictureBox1.Height = int.Parse(this.Height * 0.75 + "");

        }

        private void INTEGRADOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            int xxx = 0;
            xxx = e.KeyChar;
            switch (xxx)
            {
                case 80:
                    Punto_venta frm = new Punto_venta();
                    frm.Show(); 
                    break;
                case 112:
                    Punto_venta frm3 = new Punto_venta();
                    frm3.Show();
                    break;
                case 67:
                    ChekadorP frm1 = new ChekadorP();
                    frm1.Show();
                    break;
                case 99:
                    ChekadorP frm4 = new ChekadorP();
                    frm4.Show();
                    break;
                case 77:
                    Form1 frm2 = new Form1();
                    frm2.Show();
                    break;
                case 109:
                    Form1 frm5 = new Form1();
                    frm5.Show();
                    break;


            }

           
        }
    }
}
