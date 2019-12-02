using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    { 
        string[] vectorconectados = new string[40]; // Vector para guardar los conectados
        Socket server;
        Thread atender;
        
        delegate void DelegadoParaEscribir2(string sms);
        delegate void DelegadoParaMostrar();
        delegate void DelegadoParaEscribir(string[] text);//Lo invoco y le envio un delegado, creo un delegado(objeto) y le asigno la funcion que tiene que hacer y al thread que invoco le envio el delegado
        public Form1()
        {
            InitializeComponent();
        }
        private void EscribeBox2(string mensajito)
        {
            textBox2.Text = mensajito;
        }
        private void EscribeBox3(string mensajito)
        {
            InvitadortextBox3.Text = mensajito;
        }

        private void EscribeConectados(string[] mensaje2)//escribe en el datagridview
        {
            dataGridView1.Visible = true;
            Jugarbutton.Visible = true;
            dataGridView1.Rows.Clear();//limpiamos para que no se nos vuelva a repeir la lista en el data

            for (int i = 0; i < (mensaje2.Length - 1); i++)
            {
                dataGridView1.Rows.Add(mensaje2[i + 1]);
                vectorconectados[i] = mensaje2[i + 1];
            }      
        }
        private void NoVisibleData()
        {
            dataGridView1.Visible = false; 
        }

        private void NoVisibleLbl()
        {
            label3.Visible = false;
            labcolor.Visible = false;
            numeroLbl.Visible = false;  
        }

        private void NoVisibleTxt()
        {
            Fecha.Visible = false;
            color_dame.Visible = false;
            textBox2.Visible = false;
            InvitadortextBox3.Visible = false;
        }

        private void NoVisibleBoton()
        {
            Ganador.Visible = false;
            Edad.Visible = false;
            Tantoporciento.Visible = false;
            button4.Visible = false;
            Jugarbutton.Visible = false;
        }

        private void NoVisiblePanel() 
        
        { 
           tablero.Visible = false;
           panel1.Visible = false;
           panel2.Visible = false;  
        }

        private void VisiblelblCase1()
        {
            label3.Visible = true;
            labcolor.Visible = true;
        
        }
        private void VisibleTxtCase1()
        {
            Fecha.Visible = true;
            color_dame.Visible = true;

        }
        private void VisibleBotonCase1()
        {
            Ganador.Visible = true;
            Edad.Visible = true;
            Tantoporciento.Visible = true;
        }
        private void VisibleTxt()
        {
            Fecha.Visible = true;
            color_dame.Visible = true;
            textBox2.Visible = true;
            InvitadortextBox3.Visible = true;

        }

        private void VisibleBoton()
        {
            Ganador.Visible = true;
            Edad.Visible = true;
            Tantoporciento.Visible = true;
            button4.Visible = true;
            Jugarbutton.Visible = true;


        }

        private void VisiblePanel()
        {
            tablero.Visible = true;
        
        }
        private void VisiblePanelIf()
        {
            panel2.Visible = true;
            panel1.Visible = true;
            tablero.Visible = true;
            textBox2.Visible = true;

        }
        private void VisibleCase7()
        {
           
            panel1.Visible = true;
            InvitadortextBox3.Visible = true;

        }
        private void NoVisiblePanel1()
        {
            panel1.Visible = false;

        }
        private void CrearChatVisible()
        {
            chatlabel.Visible = true;
            Chatlbl.Visible = true;
            Chattxt.Visible = true;
            Chatbutton.Visible = true;
        }
        private void CrearChatNoVisible()
        {
            chatlabel.Visible = false;
            Chatlbl.Visible = false;
            Chattxt.Visible = false;
            Chatbutton.Visible = false;
        }
        private void EscribeChat(string chat)
        {
            Chatlbl.Text = chat;
        }

       
       
        

        private void Form1_Load(object sender, EventArgs e)
        {
            DelegadoParaMostrar d1 = new DelegadoParaMostrar(NoVisibleLbl);
            label3.Invoke(d1, new Object[] {});
            labcolor.Invoke(d1, new Object[] {});
            numeroLbl.Invoke(d1, new Object[] { });

            DelegadoParaMostrar d2 = new DelegadoParaMostrar(NoVisibleTxt);
            Fecha.Invoke(d2, new Object[] { });
        
            DelegadoParaMostrar d3 = new DelegadoParaMostrar(NoVisibleBoton);
            Ganador.Invoke(d3, new Object[] { });
           
            DelegadoParaMostrar d4 = new DelegadoParaMostrar(NoVisiblePanel);
            tablero.Invoke(d4, new Object[] { });
            
            DelegadoParaMostrar d5 = new DelegadoParaMostrar(NoVisibleData);
            dataGridView1.Invoke(d5, new Object[] { });

            DelegadoParaMostrar d15 = new DelegadoParaMostrar(CrearChatNoVisible);
            chatlabel.Invoke(d15, new Object[] { });
 
        }

        string acepta;
        int id;
        string NombreInvitador;
        int idposible;
        int activo;
        string nombreact;
        
        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1].Split('\0')[0];
                string mensajevuelta;
                string Nombreconv;

                switch (codigo)
                {

                    case 1:  // LogIn
                         DelegadoParaMostrar d6 = new DelegadoParaMostrar(VisiblelblCase1);
                         label3.Invoke(d6, new Object[] {});
                         DelegadoParaMostrar d7 = new DelegadoParaMostrar(VisibleTxtCase1);
                         Fecha.Invoke(d7, new Object[] {});
                         DelegadoParaMostrar d8 = new DelegadoParaMostrar(VisibleBotonCase1);
                         Ganador.Invoke(d8, new Object[] {});


                        MessageBox.Show("Estas dentro: " + mensaje);
                        break;
                    case 2:  // Edad media de los ganadores con ese color 

                        MessageBox.Show("Edad media: " + mensaje);
                        break;
                    case 3:  // Ganador en esa fecha 

                        MessageBox.Show("El ganador es: " + mensaje);
                        break;

                    case 4:  // Tanto por ciento de partidas ganadas

                        MessageBox.Show("Tanto por ciento de partidas ganadas con ese color es: " + mensaje);
                        break;

                    case 5: // listadeconectados
                        
                          string[] partes = Encoding.ASCII.GetString(msg2).Split('/');
                          string mensajefinal = partes[1].Split('\0')[0];
                          string[] mensaje2 = mensajefinal.Split('|');
                          
                        DelegadoParaEscribir dele = new DelegadoParaEscribir(EscribeConectados);
                        dataGridView1.Invoke(dele, new Object[]{mensaje2});
                       
                        break;

                    case 6: //Muestra si se ha aceptado la peticion

                        mensajevuelta = trozos[1].Split('\0')[0];
                        acepta = trozos[2].Split('\0')[0];

                        DelegadoParaMostrar d9 = new DelegadoParaMostrar(VisiblePanelIf);
                        DelegadoParaEscribir2 d10 = new DelegadoParaEscribir2(EscribeBox2);
                        

                        if (mensajevuelta == "Acepta")
                        {    
                            panel2.Invoke(d9, new Object[] {});

                            textBox2.Invoke(d10, new Object[] { acepta + " acepta la inviacion" });
                        }
                        else
                        {
                            panel2.Invoke(d9, new Object[] { });
                            textBox2.Invoke(d10, new Object[] { acepta + " rechaza la inviacion" });
                        }

                        break;

                    case 7: //Invitar

                        mensajevuelta = trozos[1].Split('\0')[0];
                        NombreInvitador = trozos[2].Split('\0')[0];
                        

                        DelegadoParaMostrar d11 = new DelegadoParaMostrar(VisibleCase7);
                        DelegadoParaEscribir2 d12 = new DelegadoParaEscribir2(EscribeBox3);
                        DelegadoParaMostrar d13 = new DelegadoParaMostrar(NoVisiblePanel1);
                        DelegadoParaMostrar d17 = new DelegadoParaMostrar(VisiblePanel);


                        if (mensajevuelta == "Invitado")
                        {
                            tablero.Invoke(d17, new Object[] { });
                            panel1.Invoke(d11, new Object[] { });
                            InvitadortextBox3.Invoke(d12, new Object[] {NombreInvitador});
                       
                        }
                        else
                        {
                            tablero.Invoke(d17, new Object[] { });
                            panel1.Invoke(d13, new Object[] { });
                         
                        }
                        break;

                    case 8: // Recibo una frase chat
                        mensajevuelta = trozos[1].Split('\0')[0];
                        Nombreconv = trozos[2].Split('\0')[0];
                        DelegadoParaEscribir2 d16 = new DelegadoParaEscribir2(EscribeChat);
                        Chatlbl.Invoke(d16, new Object[] {Chatlbl.Text + Nombreconv + ":" + mensajevuelta + "\n"});
                        
                        break;

                    case 9: //Mostramos CHAT

                        id = idposible;
                        mensajevuelta = trozos[1].Split('\0')[0];
                        
                        if (mensajevuelta == "crear")
                        {
                            DelegadoParaMostrar d14 = new DelegadoParaMostrar(CrearChatVisible);
                            chatlabel.Invoke(d14, new Object[] { });
                        }

                        else

                            MessageBox.Show("No se ha podido enviar nah :V");

                        break;

                    case 10: //Enviamos posibe IDP

                        idposible = Convert.ToInt32(trozos[1].Split('\0')[0]);

                        break;
                    
                    
                }



            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102"); //147.83.117.22 ip de shiva //192.168.56.102
            IPEndPoint ipep = new IPEndPoint(direc, 9050); // 50084 //9101


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

            //pongo en marcha el thread que atenderá los mensajes del servidor
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

            private void button2_Click(object sender, EventArgs e)
        {
            if (LOGIN.Checked)
            {
                //Enviamos nombre y contraseña 
                string mensaje = "1/" + nombre.Text + "/" + contraseña.Text;
                // Enviamos al servidor el nombre tecleado y  la contraseña
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (Edad.Checked)
            {
               string mensaje = "2/" + color_dame.Text;
              // Enviamos al servidor el nombre tecleado
              byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
              server.Send(msg);

            }
            else if (Ganador.Checked)
            {
                string mensaje = "3/" + Fecha.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else if (Tantoporciento.Checked)
            {
                
                  string mensaje = "4/" + color_dame.Text;
                
                  // Enviamos al servidor el nombre tecleado
                  byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                  server.Send(msg);

                     
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/"+ nombre.Text + "/";
        
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
       
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();


        }
    
        private void Jugarbutton_Click(object sender, EventArgs e) //botnon para invitar 
        {
            if (vectorconectados.Length == 0)
            {
                MessageBox.Show("Debe esperar a que se conecte al menos un usuario");
                return;
            }

            int invitados = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
            if (invitados > 0)
            {

                string mensaje = "7/" + nombre.Text + "/" + invitados + "/";
                for (int i = 0; i < invitados; i++)
                {
                    int row = dataGridView1.SelectedCells[i].RowIndex;
                    mensaje = mensaje + vectorconectados[row] + ",";

                }


                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
            else
                MessageBox.Show("Debe seleccionar al menos un jugador al que invitar");


        }
        private void Aceptarbtn_Click_1(object sender, EventArgs e)
        {
            id = idposible;
            string mensaje = "6/" + nombre.Text + "/" + NombreInvitador + "/1"; //Envio mi nombre y el nombre de quien me invita
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            panel1.Visible = false;

        }

        private void Rechazarbtn_Click_1(object sender, EventArgs e)
        {
            string mensaje = "6/" + nombre.Text + "/" + NombreInvitador + "/0";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);

            server.Send(msg);
            panel1.Visible = false;
        }

        private void Chatbutton_Click(object sender, EventArgs e)  // Boton Enviar mensajes
        {

            string mensaje = "8/" +Chattxt.Text + "/" +nombre.Text + "/"; //Enviamos el mensaje

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);

            server.Send(msg);


        }
        private void Nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void LOGIN_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Labcolor_Click(object sender, EventArgs e)
        {

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Contraseña_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void NumJugadores_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Conectados_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Chatbutton4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


      

      
        

       
    }
}
