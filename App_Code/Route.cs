using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Route
{
    public static string GetRootPath(string path)
    {
        string returnUrl = "/acig_help_desk/" + path;
        return returnUrl;
    }
}