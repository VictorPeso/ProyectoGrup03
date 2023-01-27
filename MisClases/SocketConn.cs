using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MisClases
{
    public class SocketConn
    {
        Socket server { get; set; }

        public SocketConn()
        { }

        public void IniciarConnexión()
        {
            ////Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            ////al que deseamos conectarnos
            //IPAddress direc = IPAddress.Parse("192.168.56.102");
            //IPEndPoint ipep = new IPEndPoint(direc, 9754);

            ////Creamos el socket 
            //server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //try
            //{
            //    server.Connect(ipep);
            //}
            //catch (SocketException ex)
            //{
            //    MessageBox.Show("No se ha podido conectar con el servidor");
            //    return;
            //}
        }
    }
}
