using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Custom_Logs
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public string Incident_Date { get; set; }
    public string Error_Long_Desc { get; set; }
    public string Error_Short_Desc { get; set; }
    public string System { get; set; }
    public string Resolved_Date { get; set; }
    public string Resolved_Description { get; set; }
    public string Time_Difference { get; set; }
}