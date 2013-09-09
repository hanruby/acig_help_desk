using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Globalization;

public static class DateTimeHelper
{
    public static string ConvertToString(string inputDateTime)
    {
        DateTime outputDateTime;
        string result = "";
        result = DateTime.TryParse(inputDateTime, out outputDateTime) ? DateTime.Parse(inputDateTime).ToString("dd-MM-yyyy") : inputDateTime;
        return result;
    }

    public static DateTime? ConvertToDate(string inputDateTime)
    {
        DateTime outputDateTime;
        DateTime? result = null;
        if (DateTime.TryParseExact(inputDateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out outputDateTime))
        {
            result = outputDateTime;
        }
        return result;
    }

    public static DateTime ConvertToDateWithoutNull(string inputDateTime)
    {
        return DateTime.ParseExact(inputDateTime, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
    }

    public static string ToTimeStamp()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    public static void GetStartAndEndTime(string inputDate, out DateTime startTime, out DateTime endTime)
    {
        var start = inputDate.Split('-')[2] + "-" + inputDate.Split('-')[1] + "-" + inputDate.Split('-')[0] + " 00:00:00";
        var end = inputDate.Split('-')[2] + "-" + inputDate.Split('-')[1] + "-" + inputDate.Split('-')[0] + " 23:59:59";
        startTime = DateTime.Parse(start);
        endTime = DateTime.Parse(end);
    }

    public static Hashtable GetStartAndEndDateByTwoValues(string FromDate, string ToDate)
    {
        var fromDateArray = FromDate.Split('-');
        var toDateArray = ToDate.Split('-');
        DateTime StartDate = DateTime.Parse(fromDateArray[2] + "-" + fromDateArray[1] + "-" + fromDateArray[0] + " 00:00:00");
        DateTime EndDate = DateTime.Parse(toDateArray[2] + "-" + toDateArray[1] + "-" + toDateArray[0] + " 23:59:59");
        Hashtable h = new Hashtable();
        h.Add("StartDate", StartDate);
        h.Add("EndDate", EndDate);
        return h;
    }

    public static string To24Hours(DateTime dateTime)
    {
        return dateTime.ToString("dd-MM-yyyy hh:mm tt");
    }

    public static Hashtable GetSartAndEndWeekDay(DateTime dt)
    {
        int deltaStart = DayOfWeek.Sunday - dt.DayOfWeek;
        int deltaEnd = DayOfWeek.Saturday - dt.DayOfWeek;
        DateTime start = dt.AddDays(deltaStart);
        DateTime end = dt.AddDays(deltaEnd);
        Hashtable h = new Hashtable();
        h.Add("StartDate", start);
        h.Add("EndDate", end);
        return h;
    }
}