# ServerApp

/************/
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


//-------SERVE



static void Connect(String server, String message)
{
  try
  {
    // Create a TcpClient.
    // Note, for this client to work you need to have a TcpServer
    // connected to the same address as specified by the server, port
    // combination.
    Int32 port = 13000;

    // Prefer a using declaration to ensure the instance is Disposed later.
    using TcpClient client = new TcpClient(server, port);

    // Translate the passed message into ASCII and store it as a Byte array.
    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

    // Get a client stream for reading and writing.
    NetworkStream stream = client.GetStream();

    // Send the message to the connected TcpServer.
    stream.Write(data, 0, data.Length);

    Console.WriteLine("Sent: {0}", message);

    // Receive the server response.

    // Buffer to store the response bytes.
    data = new Byte[256];

    // String to store the response ASCII representation.
    String responseData = String.Empty;

    // Read the first batch of the TcpServer response bytes.
    Int32 bytes = stream.Read(data, 0, data.Length);
    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
    Console.WriteLine("Received: {0}", responseData);

    // Explicit close is not necessary since TcpClient.Dispose() will be
    // called automatically.
    // stream.Close();
    // client.Close();
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

# APP

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


