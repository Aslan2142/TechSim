using Godot;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

public class Client : Node
{

    private TcpClient client;
    private NetworkStream stream;
    
    public int bufferSize = 1024;

    public string serverMessage { get; private set; }
    
    public bool ConnectToServer(string hostname, int port, string username, string password)
    {
        // Connect
        try {
            client = new TcpClient(hostname, port);
            stream = client.GetStream();
        }
        catch (Exception)
        {
            serverMessage = "Can't connect to the server";
            return false;
        }
        
        // Check compatibility
        Response<object> compatibilityResponse = GetData(RequestType.CheckCompatibility, Consts.VERSION);
        if (compatibilityResponse.Type == ResponseType.NotCompatible)
        {
            // Close connection if not compatible
            client.Close();
            return true;
        }
        
        // Get public key
        byte[] buffer = new byte[bufferSize];
        stream.Read(buffer, 0, buffer.Length);
        RSAParameters2 par = Helpers.DeserializeResponse<RSAParameters2>(buffer).Data;
        RSAParameters parameters = new RSAParameters() { D = par.D, DP = par.DP, DQ = par.DQ, P = par.P, Q = par.Q, InverseQ = par.InverseQ, Exponent = par.Exponent, Modulus = par.Modulus };
        
        RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
        cryptoServiceProvider.ImportParameters(parameters);
        
        // Hash password
        SHA512 sha256 = new SHA512Managed();
        byte[] passwordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        password = Convert.ToBase64String(passwordHash);
        
        // Encrypt Credentials
        byte[] encryptedUsername = cryptoServiceProvider.Encrypt(Encoding.UTF8.GetBytes(username), false);
        byte[] encryptedPassword = cryptoServiceProvider.Encrypt(Encoding.UTF8.GetBytes(password), false);
        username = Convert.ToBase64String(encryptedUsername);
        password = Convert.ToBase64String(encryptedPassword);
        
        // Authorize
        AccountInformation accountInformation = new AccountInformation(username, password);
        Response<object> authorizationResponse = GetData(RequestType.Authorization, accountInformation);
        if (authorizationResponse.Type == ResponseType.NotAuthorized)
        {
            // Close connection if not authorized
            client.Close();
            serverMessage = authorizationResponse.Data.ToString();
            return true;
        }

        serverMessage = "Successfully connected to the server";

        return true;
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

    public Response<object> GetData(Request request)
    {
        return GetData<object>(request);
    }

    public Response<T> GetData<T>(RequestType type, System.Object data = null)
    {
        return GetData<T>(new Request(type, data));
    }
    
    public Response<object> GetData(RequestType type, System.Object data = null)
    {
        return GetData<object>(new Request(type, data));
    }

}