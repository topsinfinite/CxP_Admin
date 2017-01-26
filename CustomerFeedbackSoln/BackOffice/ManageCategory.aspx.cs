using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using System.Configuration;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class ManageCategory : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string OrgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private int orgId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                orgId = (int)Session["orgid"];
                if (!IsPostBack)
                {
                    BindGrid(orgId);
                    ListViewUpdate.DataSource = LookupBLL.GetGalleryIcons((int)Utility.IconType.Category);
                    ListViewUpdate.DataBind();
                    Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlorgfilter);
                    Utility.BindCategoryTypeList(ddlType);
                    if (User.IsInRole(AdminRole))
                    {
                        dvfilter.Visible = true;
                    }
                    if (User.IsInRole(OrgAdminRole))
                    {
                        ddlOrg.SelectedValue = orgId.ToString();
                        ddlOrg.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>An unexpected error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }

        }

        private void BindGrid(int orgId=0)
        {
            if(User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            gvDefault.DataSource = LookupBLL.GetCategoryList(orgId);
            gvDefault.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    Category b = null; bool rst = false;
                    b = LookupBLL.GetCategories(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.Name = txtName.Text.ToUpper();
                        b.CategoryType = int.Parse(ddlType.SelectedValue);
                        b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        b.ModifiedBy = User.Identity.Name;
                        b.DateModified = DateTime.Now;
                        //b.Code = txtcode.Text;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        rst = LookupBLL.UpdateCategory(b);
                        if (rst != false)
                        {
                            BindGrid();
                            success.Visible = true;
                            success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record updated successfully!!.";
                            return;
                        }
                    }

                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button>Record could Not updated. Kindly try again. If error persist contact Administrator!!.";
                    }
                }
                else
                {
                    string path = Server.MapPath("~/Upload/");
                   // string FileUploadName = Utility.UploadFile(fileUploadDoc, path);
                    //if (!string.IsNullOrEmpty(FileUploadName))
                   string iconName = Utility.getIconName(ListViewUpdate);
                   if (!string.IsNullOrEmpty(iconName))
                    {
                        bool result = false;
                        Category b = new Category();
                        b.Name = txtName.Text.ToUpper();
                        b.CategoryType = int.Parse(ddlType.SelectedValue);
                        b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        b.CategoryIcon = iconName;
                        b.AddedBy = User.Identity.Name;
                        b.DatedAdded = DateTime.Now;

                        // b.Code = txtcode.Text;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        result = LookupBLL.AddCategory(b);
                        if (result)
                        {
                            BindGrid();
                            txtName.Text = "";
                            //txtcode.Text = "";
                            success.Visible = true;
                            success.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong> Record added successfully!!.";
                            return;
                        }
                        else
                        {
                            error.Visible = true;
                            error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record could Not added. Kindly try again. If error persist contact Administrator!!.";
                        }
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record could Not added.Error while uploading icon. Kindly try again. If error persist contact Administrator!!.";
                    }
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>A problem has occurred while submitting your data. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/Upload/");
                //string FileUploadName = Utility.UploadFile(fileUpload1, path);
                string iconName = Utility.getIconName(ListViewUpdate);
                if (!string.IsNullOrEmpty(iconName))
                {
                    Category b = null; bool rst = false;
                    b = LookupBLL.GetCategories(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.CategoryIcon = iconName;
                        b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        b.DateModified = DateTime.Now;
                        b.ModifiedBy = User.Identity.Name;
                        //b.Code = txtcode.Text;

                        rst = LookupBLL.UpdateCategory(b);
                        if (rst != false)
                        {
                            BindGrid();
                            success.Visible = true;
                            success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record updated successfully!!.";
                            return;
                        }
                        else
                        {
                            error.Visible = true;
                            error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>An error occurred while updating smiley. kindly try again";
                            return;
                        }
                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure that icon is attached and the image is in correct format. Smiley should not excess 4MB. kindly try again";
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>A problem has occurred while submitting your data. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void gvDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";
                dvEdit.Visible = true;
                btnUpdate.Visible = true;
                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                Category rg = LookupBLL.GetCategories(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                    txtName.Text = rg.Name;
                    ddlOrg.SelectedValue = rg.OrganisationId.HasValue ? rg.OrganisationId.ToString() : "";
                    if (rg.CategoryType.HasValue)
                    {
                        ddlType.SelectedValue = rg.CategoryType.Value.ToString();
                    }
                    else
                    {
                        ddlType.SelectedValue = "";
                    }
                    if (rg.isActive.HasValue && rg.isActive.Value)
                        chk.Checked = true;
                    else
                        chk.Checked = false;
                }

            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void gvDefault_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                orgId = (int)Session["orgid"];
                gvDefault.PageIndex = e.NewPageIndex;
                BindGrid(orgId);
            }
            catch
            { }
        }
        protected string GetType(object o)
        {
            return Utility.GetCategoryType(o);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlorgfilter.SelectedValue != "0")
                {
                    int srch = int.Parse(ddlorgfilter.SelectedValue);
                    BindGrid(srch);
                }
                else
                {
                    BindGrid();
                }
            }
            catch
            {
            }
        }
        protected void btnClr_Click(object sender, EventArgs e)
        {
            ddlorgfilter.SelectedValue = "0";
            BindGrid();
        }
    }
}