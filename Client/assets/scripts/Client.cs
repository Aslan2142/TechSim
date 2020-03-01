using Godot;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class Client : Node
{

    private TcpClient client;
    private NetworkStream stream;
    
    public int bufferSize = 1024;
    
    public void ConnectToServer(string hostname, int port)
    {
        // Connect
        client = new TcpClient(hostname, port);
        stream = client.GetStream();

        // Authorize
        // TO-DO

        // Check compatibility
        // TO-DO
    }

    public void DisconnectFromServer()
    {
        // Send disconnect request
        byte[] buffer = Helpers.SerializeRequest(new Request(RequestType.Disconnect));
        stream.Write(buffer, 0, buffer.Length);

        client.Close();
    }

    // Send request to the server and get response
    public Response<T> GetData<T>(Request request)
    {
        // Send request
        byte[] buffer = Helpers.SerializeRequest(request);
        stream.Write(buffer, 0, buffer.Length);

        // Get response
        buffer = new byte[bufferSize];
        stream.Read(buffer, 0, buffer.Length);
        
        return Helpers.DeserializeResponse<T>(buffer);
    }

    public Response<T> GetData<T>(RequestType type, System.Object data = null)
    {
        return GetData<T>(new Request(type, data));
    }

}