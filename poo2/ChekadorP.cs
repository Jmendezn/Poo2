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
using System.Net;  // se agregan las clases para poder utilizar el
using System.Net.Mail; // servicio de correo

namespace poo2
{
    public partial class ChekadorP : Form
    {
        public ChekadorP()
        {
            InitializeComponent();
        }

        private String Codigo = "";
        private int Con_1 = 0;
        private Boolean sTs_1 = false;
        private Boolean sTs_2 = false;
        string ruta = Application.StartupPath;

        private void ChekadorP_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            ruta = ruta.Replace("bin\\Debug", "");
            label3.Text = DateTime.Now.ToString();
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, 10);
            label2.Location = new Point(this.Width / 2 - label2.Width / 2, label1.Height + 11);
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, label1.Height + label2.Height + 11);
            pictureBox1.Location = new Point(100, label1.Height + label2.Height + label3.Height + 21);
            //pictureBox1.Width = int.Parse(this.Height * 0.50 + "");
            //pictureBox1.Height = int.Parse(this.Height * 0.75 + "");
            groupBox1.Visible = false;
            groupBox1.Location = new Point(300, label1.Height + label2.Height + label3.Height + 21);
            label4.Location = new Point(300, label1.Height + label2.Height + label3.Height + 21);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();

            if (sTs_1 == true)
            {
                if (Con_1 == 20)
                {
                    label6.Text = "";
                    label8.Text = "";
                    label11.Text = "";
                    Con_1 = Con_1 + 1;
                }
                else
                {
                    Con_1 = Con_1 + 1;

                    if (Con_1 == 22)
                    {
                        groupBox1.Visible = false;
                        sTs_1 = false;
                        Con_1 = 0;
                        pictureBox1.Image = Image.FromFile(ruta + "ANONIMO.BMP");
                        this.Close();

                    }
                }


            }

            if (sTs_2 == true)
            {
                label4.Text = "USUARIO NO ENCONTRADO";
                // CAMBIAR A COLOR ROJO

                Con_1 = Con_1 + 1;
                if (Con_1 == 20)
                {
                    label4.Text = "FAVOR DE CHECAR";
                    // CAMBIAR A COLOR VERDE
                    sTs_2 = false;
                    Con_1 = 0;

                }
            }


        }

        private void buscar(String texto)
        {
            string[] Usuarios;
            string line;
            string EMai = "";
            System.IO.StreamReader file =
                new System.IO.StreamReader("Empleados.csv");

            /*
             0 matricula
             1 nombre
             2 ruta foto
             3 correo
             4 status
             */
            int Cc = 0;
            int Vc = 0;
            
            while ((line = file.ReadLine()) != null)
            {
                Usuarios = line.Split(',');
                if (texto == Usuarios[0])
                {
                    label6.Text = Usuarios[1];
                    label8.Text = Usuarios[0];
                    label11.Text = Usuarios[4];
                    EMai = Usuarios[3];
                    //MessageBox.Show(EMai);

                    Vc = 1;
                    if (Usuarios[4]=="SALIDA")
                    {
                        label11.BackColor = Color.Red;
                    }
                    else
                    {
                       label11.BackColor = Color.Green;
                    }
                    
                    groupBox1.Visible = true;
                    sTs_1 = true;
                    
                    pictureBox1.Image = Image.FromFile(ruta + Usuarios[0] +".JPG");

                    if (Usuarios[4] == "ENTRADA")
                    {
                        file.Close();
                        label9.Text = label3.Text;
                        lineChanger(Usuarios[0] + "," + Usuarios[1] + "," + Usuarios[2] + "," + Usuarios[3] + "," + "SALIDA", "Empleados.csv", Cc);

                        // EMULA EN EL LABEL EL ENVIO DE CORREO, SI LE PONE USUARIO Y PASS CORRECTO SI FUNCIONA CON HOTMAIL 
                        // SendMail(label6.Text, "SALIDA", EMai);
                        label13.Text ="ya mande el correo de ENTRADA";


                        break;
                    }
                    else
                    {
                        file.Close();
                        label9.Text = label3.Text;
                        lineChanger(Usuarios[0] + "," + Usuarios[1] + "," + Usuarios[2] + "," + Usuarios[3] + "," + "ENTRADA", "Empleados.csv", Cc);
                        // EMULA EN EL LABEL EL ENVIO DE CORREO, SI LE PONE USUARIO Y PASS CORRECTO SI FUNCIONA CON HOTMAIL 
                        //SendMail(label6.Text, "ENTRADA", EMai);
                        label13.Text = "ya mande el correo de SALIDA";
                        break;
                    }
                }
                else
                {
                    

                }
                Cc = Cc + 1;
            }

            file.Close();

            if (Vc == 0)
            {
                sTs_2 = true;
            }

        }

        static void lineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit] = newText;
            File.WriteAllLines(fileName, arrLine);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buscar(textBox1.Text);
        }


        private void SendMail(string uSr, string Stat, string Corr)        // funcion para enviar correo
        { // declaracion de variable del correo
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            try
            {
                msg.To.Add(Corr); // se agrega un correo receptor 
                msg.DeliveryNotificationOptions = // se agrega las condiciones del correo
                DeliveryNotificationOptions.OnSuccess;
                msg.Priority = MailPriority.High;
                // se agrega correo remitente 
                msg.From = new MailAddress("jhmn_@hotmail.com", "PASSWORD",
                System.Text.Encoding.UTF8);
                msg.Subject = "checador"; // asunto del correo
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                // se escribe el mensaje que contenga el correo
                msg.Body = "informandole que a las" + System.DateTime.Now.ToString()
                + "el usuario " + uSr+  " checo la " + Stat;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = false;
                // comando del servidor de correo
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.live.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new
                // se agrega correo remitente y su contrseña
                System.Net.NetworkCredential("jhmn_@hotmail.com", "PASSWORD");
                client.Send(msg);
               // MessageBox.Show("ya mande el correo!!!");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //manda mensaje con el tipo de error detectadp
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK,
                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK,
                MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void ChekadorP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Codigo += e.KeyChar;

            if (e.KeyChar == 13)
            {
                buscar(textBox1.Text);
                Codigo = "";


            }
        }
    }

}
