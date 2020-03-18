using System;
using System.Text.Json;

public static class Helpers
{

    // Return response based on data
    public static Response<T> DeserializeResponse<T>(byte[] bytes)
    {
        // Remove leading zeros
        int size = Array.FindLastIndex(bytes, b => b != 0);
        Array.Resize(ref bytes, size + 1);

        // Deserialize JSON data
        return JsonSerializer.Deserialize(bytes, typeof(Response<T>)) as Response<T>;
    }

    public static T Deserialize<T>(string json)
    {
        // Deserialize JSON data
        return (T)JsonSerializer.Deserialize(json, typeof(T));
    }

    public static byte[] SerializeRequest(Request request)
    {
        return JsonSerializer.SerializeToUtf8Bytes(request, typeof(Request));
    }

}