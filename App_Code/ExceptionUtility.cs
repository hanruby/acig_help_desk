using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

public class ExceptionUtility
{
	public ExceptionUtility()
	{
	}

    public static void LogException(Exception exc, string source)
    {
        string body = string.Empty;
        string logFile = "~/App_Data/ErrorLog.txt";
        logFile = HttpContext.Current.Server.MapPath(logFile);

        StreamWriter sw = new StreamWriter(logFile, true);
        //StreamWriter sw = new StreamWriter(@"I:\it-Backup2\ubaid\ErrorLog.txt", true);
        sw.WriteLine("********** {0} **********", DateTime.Now);
        if (exc.InnerException != null)
        {
            sw.Write("Inner Exception Type: ");
            sw.WriteLine(exc.InnerException.GetType().ToString());
            body += "Inner Exception Type: " + exc.InnerException.GetType().ToString() + "<br/>";
            sw.Write("Inner Exception: ");
            sw.WriteLine(exc.InnerException.Message);
            body += "Inner Exception: " + exc.InnerException.Message + "<br/>";
            sw.Write("Inner Source: ");
            sw.WriteLine(exc.InnerException.Source);
            body += "Inner Source: " + exc.InnerException.Source + "<br/>";
            if (exc.InnerException.StackTrace != null)
            {
                sw.WriteLine("Inner Stack Trace: ");
                sw.WriteLine(exc.InnerException.StackTrace);
                body += "Inner Stack Trace: " + "<br/>" + exc.InnerException.StackTrace + "<br/>";
            }
        }
        sw.Write("Exception Type: ");
        sw.WriteLine(exc.GetType().ToString());
        sw.WriteLine("Exception: " + exc.Message);
        sw.WriteLine("Source: " + source);
        sw.WriteLine("Stack Trace: ");
        if (exc.StackTrace != null)
        {
            sw.WriteLine(exc.StackTrace);
            sw.WriteLine();
        }
        sw.Close();

        Notifier.SendEmail("ubaidkhan88@gmail.com", "Acig It Help Desk Error", body, new List<string>(), false);
        Notifier.SendEmail("ubaid@acig.com.sa", "Acig It Help Desk Error", body, new List<string>(),  false);
    }
}