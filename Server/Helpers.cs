using System;

namespace TechSimServer
{

    public static class Helpers
    {

        // Returns time and date by using time in hours as parameter
        public static TimeDate GetTimeDateString(int time)
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
        public static Request GetRequest(string data)
        {
            string[] arrayOfData = data.Split(Consts.REQUEST_DATA_SEPARATOR);

            // Bad request
            if (arrayOfData.Length != 3)
            {
                throw new Exception();
            }

            return new Request(
                Convert.ToInt32(arrayOfData[0]), // UserID
                (RequestType)Enum.Parse(typeof(RequestType), arrayOfData[1]), // Type
                arrayOfData[2] // Data
            );
        }

    }

}