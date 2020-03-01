using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TechSimServer
{

    class Server
    {

        protected TcpListener server;
        public int port { get; set; }
        public int bufferSize { get; set; }

        // Set default values
        public Server()
        {
            port = 2142;
            bufferSize = 128;
        }

        public Server(int _port, int _bufferSize)
        {
            port = _port;
            bufferSize = _bufferSize;
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
            NetworkStream stream = client.GetStream();
            Console.WriteLine("Incoming Connection!");

            // After accepting connection, start listening for other connections on a new thread
            Task.Run(() => WaitForConnection());

            // Authorize client
            //long userID;
            // TO-DO

            // Compatibility checks
            // TO-DO

            // Handle client
            Console.WriteLine("Client Connected!");
            while (true)
            {
                // Fill buffer with data from stream
                byte[] buffer = new byte[bufferSize];
                stream.Read(buffer, 0, buffer.Length);

                // Get request
                Request request;
                try
                {
                    request = Helpers.DeserializeRequest(buffer);
                }
                catch (Exception)
                {
                    // Respond with "InvalidData" on exception
                    buffer = Helpers.SerializeResponse(new Response(ResponseType.InvalidData));
                    stream.Write(buffer, 0, buffer.Length);
                    continue;
                }

                // Check if client wants to close connection
                if (request.Type == RequestType.Disconnect)
                {
                    Console.WriteLine("Client Disconnected!");
                    break;
                }

                // Handle client request
                Response response = Game.instance.HandleRequest(request);

                // Fill buffer with returned data and send it to client
                buffer = Helpers.SerializeResponse(response);
                stream.Write(buffer, 0, buffer.Length);
            }

            client.Close();
        }

    }

}