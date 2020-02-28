using System;

namespace TechSimServer
{

    public struct TimeDate
    {
        public int hour;
        public int day;
        public int month;
        public int year;
        public TimeDate(int _hour, int _day, int _month, int _year)
        {
            hour = _hour;
            day = _day;
            month = _month;
            year = _year;
        }
    }

    public struct Request
    {
        public long userID;
        public RequestType type;
        public string data;
        public Request(long _userID, RequestType _type, string _data)
        {
            userID = _userID;
            type = _type;
            data = _data;
        }
    }

}