using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Admin_Categories_index : MasterAppPage
{
    Acig_Help_DeskEntities _acig_Help_DeskEntities;
    Category _category;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDataToGridView();
        }
    }

    protected void EditCategory(object sender, GridViewEditEventArgs e)
    {
        gvCategories.EditIndex = e.NewEditIndex;
        BindDataToGridView();
    }
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvCategories.EditIndex = -1;
        BindDataToGridView();
    }

    protected void UpdateCategory(object sender, GridViewUpdateEventArgs e)
    {
        currentUserId = CurrentUser.Id();
        var id = long.Parse(((HiddenField)(gvCategories.Rows[e.RowIndex].FindControl("hdnCategoryId"))).Value.ToString());
        _acig_Help_DeskEntities = GetEntity();
        _category = _acig_Help_DeskEntities.Categories.Where(x => x.Id == id).First();
        _category.Updated_At = DateTime.Now;
        _category.Updated_By = currentUserId;
        _category.Name = ((TextBox)gvCategories.Rows[e.RowIndex].FindControl("txtCategoryNameEdit")).Text;
        _acig_Help_DeskEntities.SaveChanges();
        gvCategories.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        _acig_Help_DeskEntities = GetEntity();
        gvCategories.DataSource = _acig_Help_DeskEntities.Categories.OrderBy(x => x.Created_At).ToList();
        gvCategories.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }

    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        _acig_Help_DeskEntities = GetEntity();
        _category = new Category
        {
            Name = txtCategoryName.Text,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,
            Created_By = currentUserId,
            Updated_By = currentUserId
        };
        _acig_Help_DeskEntities.AddToCategories(_category);
        _acig_Help_DeskEntities.SaveChanges();
        BindDataToGridView();
        txtCategoryName.Text = string.Empty;
    }
}