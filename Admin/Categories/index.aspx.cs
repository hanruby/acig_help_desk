using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Admin_Categories_index : MasterAppPage
{
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
        _entity = GetEntity();
        _category = _entity.Categories.Where(x => x.Id == id).First();
        _category.Updated_At = DateTime.Now;
        _category.Updated_By = currentUserId;
        _category.Name = ((TextBox)gvCategories.Rows[e.RowIndex].FindControl("txtCategoryNameEdit")).Text;
        _entity.SaveChanges();
        gvCategories.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        _entity = GetEntity();
        gvCategories.DataSource = _entity.Categories.OrderBy(x => x.Created_At).ToList();
        gvCategories.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }

    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        _entity = GetEntity();
        _category = new Category
        {
            Name = txtCategoryName.Text,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,
            Created_By = currentUserId,
            Updated_By = currentUserId
        };
        _entity.AddToCategories(_category);
        _entity.SaveChanges();
        BindDataToGridView();
        txtCategoryName.Text = string.Empty;
    }
}