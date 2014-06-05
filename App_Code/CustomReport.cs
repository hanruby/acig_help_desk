using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CustomReport
{
    public long Id { get; set; }
    public string CreatedAt { get; set; }
    public string Subject { get; set; }
    public string Status { get; set; }
    public string Category { get; set; }
    public string CreatedBy { get; set; }
    public string AssignedTo { get; set; }
}