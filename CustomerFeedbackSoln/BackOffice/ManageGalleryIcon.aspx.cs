using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class ManageGalleryIcon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!IsPostBack)
                {
                    BindGrid();
                  
                    Utility.BindIconTypeList(ddlorgfilter);
                    Utility.BindIconTypeList(ddlType);
                   
                        dvfilter.Visible = true;
                    
                   
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>An unexpected error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        private void BindGrid(int type=0)
        {
           //type = 0;
            gvDefault.DataSource = LookupBLL.GetGalleryIcons(type);
            gvDefault.DataBind();
        }
        protected string GetType(object o)
        {
            return Utility.GetIconType(o);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    IconGallery b = null; bool rst = false;
                    b = LookupBLL.GetIcon(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        //b.Name = txtName.Text.ToUpper();
                        b.Type = int.Parse(ddlType.SelectedValue);
                        //b.Code = txtcode.Text;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        rst = LookupBLL.UpdateGalleryIcon(b);
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
                    string FileUploadName = Utility.UploadFile(fileUploadDoc, path);
                    if (!string.IsNullOrEmpty(FileUploadName))
                    {
                        bool result = false;
                        IconGallery b = new IconGallery();
                        b.Name = FileUploadName;
                        b.Type = int.Parse(ddlType.SelectedValue);
                    
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        result = LookupBLL.AddGalleryIcon(b);
                        if (result)
                        {
                            BindGrid();
                            //txtName.Text = "";
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

        protected void gvDefault_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                
                gvDefault.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch
            { }
        }

        protected void gvDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";
                dvEdit.Visible = true;
                dvUpload.Visible = false;
                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                IconGallery rg = LookupBLL.GetIcon(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                  
                    if (rg.Type.HasValue)
                    {
                        ddlType.SelectedValue = rg.Type.Value.ToString();
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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/Upload/");
                string FileUploadName = Utility.UploadFile(fileUpload1, path);
                if (!string.IsNullOrEmpty(FileUploadName))
                {
                    IconGallery b = null; bool rst = false;
                    b = LookupBLL.GetIcon(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.Name = FileUploadName;
                  
                        rst = LookupBLL.UpdateGalleryIcon(b);
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
    }
   
}