using System;

namespace TechSimServer
{

    [Serializable]
    public struct TimeDate
    {
        public int Hour;
        public int Day;
        public int Month;
        public int Year;

        public TimeDate(int _hour, int _day, int _month, int _year)
        {
            Hour = _hour;
            Day = _day;
            Month = _month;
            Year = _year;
        }
    }

    [Serializable]
    public struct Request
    {
        public long UserID { get; set; }
        public RequestType Type { get; set; }
        public object Data { get; set; }

        public Request(long _userID, RequestType _type, object _data = null)
        {
            UserID = _userID;
            Type = _type;
            Data = _data;
        }
    }

    [Serializable]
    public struct Response
    {
        public ResponseType Type { get; set; }
        public object Data { get; set; }

        public Response(ResponseType _type, object _data = null)
        {
            Type = _type;
            Data = _data;
        }
    }

}