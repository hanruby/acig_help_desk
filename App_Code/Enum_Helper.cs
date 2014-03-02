using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Enum_Helper
{
    public enum States
    {
        Pending, Clarification, Clarified, Resolved, Closed
    };

    public enum AccountTypes
    {
        ACIG,
        NON_ACIG
    }
}