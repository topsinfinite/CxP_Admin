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
    public partial class ManageSmiley : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string OrgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private int orgId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //dvEdit.Visible = false;
                btnSubmit.Attributes.Add("onclick", " this.disabled = true; this.value='Processing'; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
              
                orgId = (int)Session["orgid"];
                if (!IsPostBack)
                {
                    BindGrid(orgId);
                    ListViewUpdate.DataSource = LookupBLL.GetGalleryIcons((int)Utility.IconType.Smiley);
                    ListViewUpdate.DataBind();
                    Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlorgfilter);
                    Utility.BindSmileyTypeList(ddlType);
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
            if (User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            gvDefault.DataSource = LookupBLL.GetSmileyActionList(orgId);
            gvDefault.DataBind();
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
         protected void btnUpdate_Click(object sender, EventArgs e)
         {
             try
             {
                  string path = Server.MapPath("~/Upload/");
                 // string FileUploadName = Utility.UploadFile(fileUpload1, path);
                  string iconName = Utility.getIconName(ListViewUpdate);
                  if (!string.IsNullOrEmpty(iconName))
                    {
                        SmileyAction b = null; bool rst = false;
                        b = LookupBLL.GetSmileyAction(Convert.ToInt32(hidkey.Value));
                        if (b != null)
                        {
                            b.SmileyImage = iconName;
                            b.ModifiedBy = User.Identity.Name;
                            b.DateModified = DateTime.Now;
                            //b.Code = txtcode.Text;
                            
                            rst = LookupBLL.UpdateSmileyAction(b);
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
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure that smiley is attached and the image is in correct format. Smiley should not excess 4MB. kindly try again";
                    }
             }
             catch (Exception ex)
             {
                 error.Visible = true;
                 error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>A problem has occurred while submitting your data. kindly try again!!!";
                 Utility.WriteError("Error: " + ex.Message);
             }
         }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    //dvUpload.Visible = false;

                    SmileyAction b = null; bool rst = false;
                    b = LookupBLL.GetSmileyAction(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.Name = txtName.Text.ToUpper();
                        b.SmileyType = int.Parse(ddlType.SelectedValue);
                        b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        b.AddedBy = User.Identity.Name;
                        b.DatedAdded = DateTime.Now;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        rst = LookupBLL.UpdateSmileyAction(b);
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
                    string iconName = Utility.getIconName(ListViewUpdate);
                    if (!string.IsNullOrEmpty(iconName))
                    {
                        bool result = false;
                        SmileyAction b = new SmileyAction();
                        b.Name = txtName.Text.ToUpper();
                        b.SmileyType = int.Parse(ddlType.SelectedValue);
                        b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        b.SmileyImage = iconName;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        result = LookupBLL.AddSmileyAction(b);
                        if (result)
                        {
                            BindGrid();
                            txtName.Text = "";
                            ddlType.SelectedValue = "";
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
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure that smiley is attached and the image is in correct format. Smiley should not excess 4MB. kindly try again";
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

        protected void gvDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dvEdit.Visible = true;
                btnUpdate.Visible = true;
               
                hid.Value = "Update";
                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                SmileyAction rg = LookupBLL.GetSmileyAction(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                    //Image1.ImageUrl = "~/Upload/" + rg.SmileyImage;
                    txtName.Text = rg.Name;
                    ddlOrg.SelectedValue = rg.OrganisationId.HasValue ? rg.OrganisationId.Value.ToString() : "";
                    if (rg.SmileyType.HasValue)
                    {
                        ddlType.SelectedValue = rg.SmileyType.Value.ToString();
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
            return Utility.GetSmileyType(o);
        }
    }
}