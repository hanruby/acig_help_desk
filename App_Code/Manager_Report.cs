using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Manager_Report
{
    public long Ticket_Id { get; set; }
    public string Category { get; set; }
    public string Subject { get; set; }
    public string Created_At { get; set; }
    public string Resolved_At { get; set; }
    public string Status { get; set; }
    public string Created_By { get; set; }
    public string Resolved_By { get; set; }
}