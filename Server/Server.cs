using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TechSimServer
{

    class Server
    {

        protected TcpListener server;
        public int port { get; set; }
        public int bufferSize { get; protected set; }

        // Default port is 2142
        public Server()
        {
            port = 2142;
        }

        public Server(int _port)
        {
            port = _port;
        }

        // Start server
        public void Start(bool startListening = false)
        {
            Console.WriteLine("Starting Server on port " + port);
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine("Server Started!");

            if (startListening)
            {
                Task.Run(() => WaitForConnection());
            }
        }

        // Stop server
        public void Stop()
        {
            server.Server.Shutdown(SocketShutdown.Both);
            server.Stop();
        }

        // Start listening for incoming connections
        public void WaitForConnection()
        {
            // Accept incoming connection
            TcpClient client = server.AcceptTcpClient();

            // After accepting connection, start listening for other connections on a new thread
            Task.Run(() => WaitForConnection());
            
            // Handle client
            while (true)
            {
                NetworkStream stream = client.GetStream();

                // Fill buffer with data from stream
                byte[] buffer = new byte[128];
                stream.Read(buffer, 0, buffer.Length);

                // Get unicode data from buffer
                string data = Encoding.Unicode.GetString(buffer).Replace("\0", "");

                // Ignore if data is empty
                if (data.Length < 1)
                {
                    continue;
                }

                // Check if client wants to close connection
                if (data == "close")
                {
                    break;
                }

                // Handle client request
                data = Game.instance.HandleRequest(Helpers.GetRequest(data));

                // Fill buffer with returned data and send it to client
                buffer = Encoding.Unicode.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }

            client.Close();
        }

    }

}
