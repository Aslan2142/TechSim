using System;
using System.Text.Json;

public static class Helpers
{

    // Returns time and date by using time in hours as parameter
    public static TimeDate GetTimeDate(int time)
    {
        int hour = time;
        int day = 0;
        int month = 0;
        int year = 1980;

        while (hour >= 24)
        {
            hour -= 24;
            day++;
        }

        while (day > 30)
        {
            day -= 30;
            month++;
        }

        while (month > 12)
        {
            month -= 12;
            year++;
        }

        return new TimeDate(hour, day, month, year);
    }

    // Return request based on data
    public static Request DeserializeRequest(byte[] bytes)
    {
        // Remove leading zeros
        int size = Array.FindLastIndex(bytes, b => b != 0);
        Array.Resize(ref bytes, size + 1);

        // Deserialize JSON data
        return JsonSerializer.Deserialize(bytes, typeof(Request)) as Request;
    }

    public static T Deserialize<T>(string json)
    {
        // Deserialize JSON data
        return (T)JsonSerializer.Deserialize(json, typeof(T));
    }

    public static byte[] SerializeResponse(Response response)
    {
        return JsonSerializer.SerializeToUtf8Bytes(response, typeof(Response));
    }

}