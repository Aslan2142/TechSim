using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace TechSimServer
{

    public class Server
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

            // Prepare variables
            byte[] buffer;
            Request request;

            // Compatibility checks
            {
                buffer = new byte[bufferSize];
                stream.Read(buffer, 0, buffer.Length);
                try
                {
                    request = Helpers.DeserializeRequest(buffer);
                    if (request.Data.ToString() == Consts.VERSION)
                    {
                        buffer = Helpers.SerializeResponse(new Response(ResponseType.IsCompatible));
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        throw new Exception(); // Exception will cause server to return ResponseType.NotCompatible
                    }
                }
                catch (Exception)
                {
                    buffer = Helpers.SerializeResponse(new Response(ResponseType.NotCompatible));
                    stream.Write(buffer, 0, buffer.Length);
                    client.Close();
                    Console.WriteLine("Client Compatibility Checks Failed! Closing Connection!");
                    return;
                }
            }


            // Authorize client
            {
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();

                // Send public key
                RSAParameters par = cryptoServiceProvider.ExportParameters(false);
                RSAParameters2 parameters2 = new RSAParameters2(par.D, par.DP, par.DQ, par.P, par.Q, par.InverseQ, par.Exponent, par.Modulus);
                buffer = Helpers.SerializeResponse(new Response(ResponseType.PublicKey, parameters2));
                stream.Write(buffer, 0, buffer.Length);

                // Start authorization process
                int userID = -1;
                buffer = new byte[bufferSize];
                stream.Read(buffer, 0, buffer.Length);
                try
                {
                    request = Helpers.DeserializeRequest(buffer);
                    AccountInformation accountInformation = Helpers.Deserialize<AccountInformation>(request.Data.ToString());
                    string username = accountInformation.Username;
                    string password = accountInformation.Password;

                    // Decrypt Credentials
                    byte[] decryptedUsername = cryptoServiceProvider.Decrypt(Convert.FromBase64String(username), false);
                    byte[] decryptedPassword = cryptoServiceProvider.Decrypt(Convert.FromBase64String(password), false);
                    username = Encoding.UTF8.GetString(decryptedUsername);
                    password = Encoding.UTF8.GetString(decryptedPassword);

                    List<PlayerData> playerData = Game.instance.data.playerData;
                    for (int i = 0; i < playerData.Count; i++)
                    {
                        if (playerData[i].username == username && playerData[i].password == password)
                        {
                            userID = i;
                            buffer = Helpers.SerializeResponse(new Response(ResponseType.Authorized));
                            stream.Write(buffer, 0, buffer.Length);
                            break;
                        }
                    }
                    if (userID < 0)
                    {
                        throw new Exception(); // Exception will cause server to return ResponseType.NotAuthorized
                    }
                }
                catch (Exception)
                {
                    buffer = Helpers.SerializeResponse(new Response(ResponseType.NotAuthorized));
                    stream.Write(buffer, 0, buffer.Length);
                    client.Close();
                    Console.WriteLine("Client Authorization Failed! Closing Connection!");
                    return;
                }
            }

            // Handle client
            Console.WriteLine("Client Connected!");
            while (true)
            {
                // Fill buffer with data from stream
                buffer = new byte[bufferSize];
                stream.Read(buffer, 0, buffer.Length);

                // Get request
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