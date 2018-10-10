using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server1
{
    public class Server
    {

        public static void Main()
        {


            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, 3333);

            Byte[] bytes = new Byte[256];
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                String data = null;
                int i;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }

                // Shutdown and end connection
                client.Close();

            }
        }

    }
}
