using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;

public class FileHelper
{
    public static string GetFilePath(string name)
    {
        var path = HttpContext.Current.Server.MapPath(Path.Combine("~/Uploads/", name));
        return path;
    }

    public static Hashtable SaveFile(HttpPostedFile postedFile, long ticketId)
    {
        var fileName = ticketId.ToString() + "_" + DateTimeHelper.ToTimeStamp() + "_" + Path.GetFileName(postedFile.FileName);
        if (fileName.Count() > 499)
        {
            fileName = ticketId.ToString() + "_" + DateTimeHelper.ToTimeStamp() + "." + Path.GetExtension(postedFile.FileName);
        }
        var filePath = FileHelper.GetFilePath(fileName);
        postedFile.SaveAs(filePath);
        Hashtable _hash = new Hashtable();
        _hash.Add("FileName", fileName);
        _hash.Add("FilePath", filePath);
        return _hash;
    }
}