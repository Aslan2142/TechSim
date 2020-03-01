using System;

[Serializable]
public class TimeDate
{
    public int Hour { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    public TimeDate() {}

    public TimeDate(int _hour, int _day, int _month, int _year)
    {
        Hour = _hour;
        Day = _day;
        Month = _month;
        Year = _year;
    }
}

[Serializable]
public class Request
{
    public RequestType Type { get; set; }
    public Object Data { get; set; }

    public Request() {}

    public Request(RequestType _type, Object _data = null)
    {
        Type = _type;
        Data = _data;
    }
}

[Serializable]
public class Response<T>
{
    public ResponseType Type { get; set; }
    public T Data { get; set; }

    public Response() {}

    public Response(ResponseType _type, T _data)
    {
        Type = _type;
        Data = _data;
    }
}