using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Admin_Sub_Categories_index : MasterAppPage
{
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
        _entity = GetEntity();
        _subCategory = _entity.Sub_Categories.Where(x => x.Id == id).First();
        _subCategory.Updated_At = DateTime.Now;
        _subCategory.Updated_By = currentUserId;
        _subCategory.Name = ((TextBox)gvSubCategories.Rows[e.RowIndex].FindControl("txtSubCategoryNameEdit")).Text;
        _entity.SaveChanges();
        gvSubCategories.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        categoryId = long.Parse(hdnCategoryId.Value);
        _entity = GetEntity();
        gvSubCategories.DataSource = _entity.Sub_Categories.Where(x => x.Category_Id == categoryId).OrderBy(x => x.Created_At).ToList();
        gvSubCategories.DataBind();
    }    

    protected void btnSaveSubCategory_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        categoryId = long.Parse(hdnCategoryId.Value);
        _entity = GetEntity();
        _subCategory = new Sub_Categories
        {
            Name = txtSubCategoryName.Text,
            Category_Id = categoryId,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,
            Created_By = currentUserId,
            Updated_By = currentUserId
        };
        _entity.AddToSub_Categories(_subCategory);
        _entity.SaveChanges();
        BindDataToGridView();
        txtSubCategoryName.Text = string.Empty;
    }
}