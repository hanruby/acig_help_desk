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
    Sub_Categories _subCategory;
    long categoryId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HideSubCategory();
            BindDataToGridView();
        }
    }

    protected void EditCategory(object sender, GridViewEditEventArgs e)
    {
        HideSubCategory();
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
        HideSubCategory();
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

    protected void ViewSubCategories(object sender, EventArgs e)
    {
        LinkButton lnkRemove = (LinkButton)sender;
        hdnCategoryId.Value = lnkRemove.CommandArgument;
        subCategoryDiv.Visible = true;
        BindDataToGvSubCategory();
    }

    protected void EditSubCategory(object sender, GridViewEditEventArgs e)
    {
        gvSubCategories.EditIndex = e.NewEditIndex;
        BindDataToGvSubCategory();
    }
    protected void CancelSubCategoryEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSubCategories.EditIndex = -1;
        BindDataToGvSubCategory();
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
        BindDataToGvSubCategory();
    }

    protected void BindDataToGvSubCategory()
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
        BindDataToGvSubCategory();
        txtSubCategoryName.Text = string.Empty;
    }

    protected void HideSubCategory()
    {
        subCategoryDiv.Visible = false;
    }
}