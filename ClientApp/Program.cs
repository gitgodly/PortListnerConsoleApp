/*---------CLIENT-------------*/
using System.Net;
using System.Net.Sockets;

internal class TCPServer
{
    public static void Main()
    {
        Console.WriteLine("Clinet");

        Int32 port = 13000;

       // TcpClient client = new TcpClient(server, port);


        Console.WriteLine("Your Message");
        var message = Console.ReadLine();

        Connect(message);
    }


    static void Connect(string message)
    {
        string server = "127.0.0.1";
        try
        {
            int port = 13000;
            using TcpClient client = new TcpClient(server, port);
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", message);
            data = new byte[256];
            String responseData = String.Empty;
            int bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\n Press Enter to continue...");
        Console.Read();
    }
}