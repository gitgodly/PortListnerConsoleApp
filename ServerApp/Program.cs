/*---SERVER-----*/

using System.Net;
using System.Net.Sockets;

internal class TCPServer
{
    public static void Main()
    {
        Console.WriteLine("Server");
        TcpListener server = null;
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(localAddr, port);
        server.Start();
        byte[] bytes = new byte[256];
        string data = null;
        while (true)        {
            Console.Write("Waiting for a connection… ");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Connected!");
            data = null;
            int i;
            NetworkStream stream = client.GetStream();
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);
                data = data.ToUpper();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
            client.Close();
            Console.WriteLine("Prss Key");
            Console.Read();
        }
    }
}