using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acig_Help_DeskModel;

public class MasterAppPage : System.Web.UI.Page
{
    protected Acig_Help_DeskEntities _acig_Help_DeskEntities;
    protected Acig_Help_DeskEntities GetEntity()
    {
        if (_acig_Help_DeskEntities == null)
        {
            _acig_Help_DeskEntities = new Acig_Help_DeskEntities();
        }
        return _acig_Help_DeskEntities;
    }
}