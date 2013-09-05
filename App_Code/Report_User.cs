using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Report_User
{
    public Report_User()
    {
        states = new List<Report_State>();
        states.Add(new Report_State { state = "Pending", count = 0 });
        states.Add(new Report_State { state = "Resolved", count = 0 });
        states.Add(new Report_State { state = "Closed", count = 0 });
    }
    public string email { get; set; }
    public List<Report_State> states { get; set; }

}