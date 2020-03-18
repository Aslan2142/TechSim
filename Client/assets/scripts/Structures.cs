/*
    Structures have to be as classes for now because of the bug in mono version that godot uses
    https://github.com/mono/mono/issues/14871
*/

using System;

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

[Serializable]
public class Response
{
    public ResponseType Type { get; set; }
    public Object Data { get; set; }

    public Response() {}

    public Response(ResponseType _type, Object _data = null)
    {
        Type = _type;
        Data = _data;
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
public class AccountInformation
{
    public string Username { get; set; }
    public string Password { get; set; }

    public AccountInformation() {}

    public AccountInformation(string _username, string _password)
    {
        Username = _username;
        Password = _password;
    }
}

[Serializable]
public class RSAParameters2 // Clone of RSAParameters struct as a class, needed because of the bug in mono version that godot uses (https://github.com/mono/mono/issues/14871)
{
    public byte[] D { get; set; }
    public byte[] DP { get; set; }
    public byte[] DQ { get; set; }
    public byte[] P { get; set; }
    public byte[] Q { get; set; }
    public byte[] InverseQ { get; set; }
    public byte[] Exponent { get; set; }
    public byte[] Modulus { get; set; }

    public RSAParameters2() {}

    public RSAParameters2(byte[] _d, byte[] _dp, byte[] _dq, byte[] _p, byte[] _q, byte[] _inverseQ, byte[] _exponent, byte[] _modulus)
    {
        D = _d;
        DP = _dp;
        DQ = _dq;
        P = _p;
        Q = _q;
        InverseQ = _inverseQ;
        Exponent = _exponent;
        Modulus = _modulus;
    }
}