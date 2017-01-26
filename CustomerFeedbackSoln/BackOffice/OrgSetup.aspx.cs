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
    public partial class OrgSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Visible = false; success.Visible = false;
            try
            {
                dvEdit.Visible = false;
                btnSubmit.Attributes.Add("onclick", " this.disabled = true; this.value='Processing'; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
                if (!IsPostBack)
                {
                    BindGrid();
                    Utility.BindStateList(ddlState);
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>An unexpected error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }

        }
        private void BindGrid()
        {
            gvDefault.DataSource = LookupBLL.GetOrganisationList();
            gvDefault.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                AppUser usr=(AppUser)Session["user"];
                if (hid.Value == "Update")
                {
                    dvUpload.Visible = false;

                    Organisation b = null; bool rst = false;
                    b = LookupBLL.GetOrganisation(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.OrgID = txtorgId.Text.ToUpper();
                        b.OrganisationName = txtorgname.Text;
                        b.ContactEmail = txtEmail.Text;
                        b.ContactMobileNo = txtmobile.Text;
                        b.ContactPhoneNo = txtphoneno.Text;
                        b.Address = txtaddress.Text;
                        b.City = txtCity.Text;
                        b.StateID = int.Parse(ddlState.SelectedValue);
                        if (chkDefault.Checked)
                            b.HomeLabel = "How Did we make you feel today ?";
                        else
                            b.HomeLabel = txtHomeLb.Text;
                        //b.Logo = fileUploadDoc.FileName;
                        b.DateAdded = DateTime.Now;
                        if (User.Identity.IsAuthenticated)
                            b.AddedBy = usr.ID;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        rst = LookupBLL.UpdateOrganisation(b);
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
                    string fileuploadBg=Utility.UploadFile(fileUploadBg,path);
                    if (!string.IsNullOrEmpty(FileUploadName))
                    {
                        bool result = false;
                        Organisation b = new Organisation();
                        b.OrgID = txtorgId.Text.ToUpper();
                        b.OrganisationName = txtorgname.Text;
                        b.ContactEmail = txtEmail.Text;
                        b.ContactMobileNo = txtmobile.Text;
                        b.ContactPhoneNo = txtphoneno.Text;
                        b.Address = txtaddress.Text;
                        b.City = txtCity.Text;
                        if (chkDefault.Checked)
                            b.HomeLabel = "How Did we make you feel today ?";
                        else
                            b.HomeLabel = txtHomeLb.Text;
                        b.StateID = int.Parse(ddlState.SelectedValue);
                        if (!string.IsNullOrEmpty(fileuploadBg))
                            b.Background = fileuploadBg;
                        b.Logo = FileUploadName;
                        b.DateAdded = DateTime.Now;
                        if (User.Identity.IsAuthenticated)
                            b.AddedBy = usr.ID;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        LabelSetting lb = new LabelSetting();
                        lb.HomeLabel = "How Did we make you feel today?";
                        lb.CategoryLabelAwesome = "Nice! What did we do to make you happy today?";
                        lb.CategoryLabelBad = "OH NO! WHAT DID WE DO WRONG?";
                        lb.ServiceLabelAwesome = "WHICH OF OUR SERVICES BLEW YOU AWAY?";
                        lb.ServiceLabelBad = "WHICH SERVICE NEEDS IMPROVEMENT?";
                        lb.ServiceLabelIndifferent = "OH, GOODNESS! WHAT CAN WE DO BETTER?";
                        lb.StaffLabelAwesome = "WHICH STAFF ATTRIBUTE BLEW YOU AWAY?";
                        lb.StaffLabelBad = "WHICH STAFF ATTRIBUTE NEEDS IMPROVEMENT?";
                        b.LabelSettings.Add(lb); bool ret = false;
                        using (FeedBackDBEntities db = new FeedBackDBEntities())
                        {
                            db.Organisations.Add(b);
                            db.SaveChanges();
                            ret = true;
                        }
                        //result = LookupBLL.AddOrganization(b);
                        if (ret)
                        {
                            BindGrid();
                            txtorgname.Text = ""; txtorgId.Text = ""; txtEmail.Text = "";
                            txtmobile.Text = ""; txtCity.Text = ""; txtaddress.Text = "";
                            txtphoneno.Text = "";
                            ddlState.SelectedValue = "";
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/Upload/");
                string FileUploadName = Utility.UploadFile(fileUpload1, path);
                if (!string.IsNullOrEmpty(FileUploadName))
                {
                    Organisation b = null; bool rst = false;
                    b = LookupBLL.GetOrganisation(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.Logo = FileUploadName;
                        //b.Code = txtcode.Text;

                        rst = LookupBLL.UpdateOrganisation(b);
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
                    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure that Logo is attached and the image is in correct format. Logo should not excess 4MB. kindly try again";
                }
                string FileUploadBG = Utility.UploadFile(fileUpload2, path);
                if (!string.IsNullOrEmpty(FileUploadBG))
                {
                    Organisation b = null; bool rst = false;
                    b = LookupBLL.GetOrganisation(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.Background = FileUploadBG;
                        //b.Code = txtcode.Text;

                        rst = LookupBLL.UpdateOrganisation(b);
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
                    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure that Logo is attached and the image is in correct format. Logo should not excess 4MB. kindly try again";
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
                dvEdit.Visible = true;
                dvUpload.Visible = false;
                dvBackgd.Visible = false;
                hid.Value = "Update";
                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                Organisation rg = LookupBLL.GetOrganisation(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                    Image1.ImageUrl = "~/Upload/" + rg.Logo;
                    Image2.ImageUrl = "~/Upload/" + rg.Background;
                    txtorgId.Text = rg.OrgID;
                    txtorgname.Text = rg.OrganisationName;
                    txtEmail.Text = rg.ContactEmail;
                    txtmobile.Text = rg.ContactMobileNo;
                    txtphoneno.Text = rg.ContactPhoneNo;
                    txtaddress.Text = rg.Address;
                    txtCity.Text = rg.City;
                    txtHomeLb.Text = rg.HomeLabel;
                    if (rg.StateID.HasValue)
                    {
                        ddlState.SelectedValue = rg.StateID.Value.ToString();
                    }
                    else
                    {
                        ddlState.SelectedValue = "";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtSea.Value))
                {
                    string srch = txtSea.Value;
                    gvDefault.DataSource = LookupBLL.GetOrganisationListByName(srch);
                    gvDefault.DataBind();
                }
            }
            catch
            {
            }
        }

        protected void btnClr_Click(object sender, EventArgs e)
        {
            txtSea.Value = "";
            BindGrid();
        }

        protected void chkDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefault.Checked)
            {
                txtHomeLb.Enabled = false;
            }
            else
            {
                txtHomeLb.Enabled = true;
            }
        }
    }
}