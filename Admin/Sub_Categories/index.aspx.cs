using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Admin_Sub_Categories_index : MasterAppPage
{
    Acig_Help_DeskEntities _acig_Help_DeskEntities;
    Sub_Categories _subCategory;
    long categoryId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnCategoryId.Value = Request.Params["id"];
            BindDataToGridView();
        }
    }

    protected void EditSubCategory(object sender, GridViewEditEventArgs e)
    {
        gvSubCategories.EditIndex = e.NewEditIndex;
        BindDataToGridView();
    }
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSubCategories.EditIndex = -1;
        BindDataToGridView();
    }

    protected void UpdateSubCategory(object sender, GridViewUpdateEventArgs e)
    {
        currentUserId = CurrentUser.Id();
        var id = long.Parse(((HiddenField)(gvSubCategories.Rows[e.RowIndex].FindControl("hdnSubCategoryId"))).Value.ToString());
        _acig_Help_DeskEntities = GetEntity();
        _subCategory = _acig_Help_DeskEntities.Sub_Categories.Where(x => x.Id == id).First();
        _subCategory.Updated_At = DateTime.Now;
        _subCategory.Updated_By = currentUserId;
        _subCategory.Name = ((TextBox)gvSubCategories.Rows[e.RowIndex].FindControl("txtSubCategoryNameEdit")).Text;
        _acig_Help_DeskEntities.SaveChanges();
        gvSubCategories.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        categoryId = long.Parse(hdnCategoryId.Value);
        _acig_Help_DeskEntities = GetEntity();
        gvSubCategories.DataSource = _acig_Help_DeskEntities.Sub_Categories.Where(x => x.Category_Id == categoryId).OrderBy(x => x.Created_At).ToList();
        gvSubCategories.DataBind();
    }    

    protected void btnSaveSubCategory_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        categoryId = long.Parse(hdnCategoryId.Value);
        _acig_Help_DeskEntities = GetEntity();
        _subCategory = new Sub_Categories
        {
            Name = txtSubCategoryName.Text,
            Category_Id = categoryId,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,
            Created_By = currentUserId,
            Updated_By = currentUserId
        };
        _acig_Help_DeskEntities.AddToSub_Categories(_subCategory);
        _acig_Help_DeskEntities.SaveChanges();
        BindDataToGridView();
        txtSubCategoryName.Text = string.Empty;
    }
}