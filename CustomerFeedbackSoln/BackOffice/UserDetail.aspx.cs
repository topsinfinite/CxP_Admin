using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int Id = Convert.ToInt32(Request.QueryString["id"].ToString());
                        hid.Value = Id.ToString();
                        BindGrid(Id);
                        Utility.BindBranchList(ddlBranch);
                        Utility.BindOrgList(ddlOrg);
                    }
                    else
                    {
                        Response.Redirect("UserSetup.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    error.Visible = true;
                    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong></strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                    Utility.WriteError("Error: " + ex.Message);
                }
            }
        }
        private void BindGrid(int Id)
        {
            gvheader.DataSource = UserBLL.GetUserByID(Id);
            gvheader.DataBind();
        }
        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int orgId = int.Parse(ddlOrg.SelectedValue);
                ddlBranch.Items.Clear();
                Utility.BindBranchList(ddlBranch, orgId);
            }
            catch
            {
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hid.Value);
                AppUser usr = null;
                usr = UserBLL.GetUserByID(int.Parse(hid.Value)).FirstOrDefault();
                if (usr != null)
                {
                    if (ddlRole.SelectedValue != "")
                    {
                        //Label lbRole = gvheader.Rows[0].FindControl("lbrole") as Label;
                        string[] rol = Roles.GetRolesForUser(usr.UserName.Trim());
                        if (rol != null && rol.Count() > 0)
                            Roles.RemoveUserFromRole(usr.UserName.Trim(), rol[0]);
                        Roles.AddUserToRole(usr.UserName.Trim(), ddlRole.SelectedValue);
                        usr.RoleName = ddlRole.SelectedValue;
                    }
                    if (ddlBranch.SelectedValue != "")
                    {
                        usr.BranchID = int.Parse(ddlBranch.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(txtfname.Text))
                    {
                        usr.FullName = txtfname.Text;
                    }
                    if (!string.IsNullOrEmpty(txtEmail.Text))
                    {
                        usr.Email = txtEmail.Text;
                    }
                    if (User.Identity.IsAuthenticated)
                        usr.ModifiedBy = User.Identity.Name;
                    usr.LastModified = DateTime.Now;
                    if (UserBLL.Update(usr))
                    {
                        BindGrid(id);
                        success.Visible = true;
                        success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> User updated successfully!!.";
                        return;
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong></strong> An error occured. User could not be updated. Kindly try again.";
                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong></strong> An error occured while loading user detail. Kindly try again";
                    return;
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong></strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                AppUser usr = null;
                usr = UserBLL.GetUserByID(int.Parse(hid.Value)).FirstOrDefault();
                if (usr != null)
                {
                    dvDet.Visible = true;
                    txtfname.Text = usr.FullName;
                    txtEmail.Text = usr.Email;
                    ddlBranch.SelectedValue = usr.BranchID.HasValue ? usr.BranchID.Value.ToString() : "";
                    ddlRole.SelectedValue = usr.RoleName;
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong></strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void btnAct_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(hid.Value);
                AppUser usr = null;
                usr = UserBLL.GetUserByID(int.Parse(hid.Value)).FirstOrDefault();
                if (usr != null)
                {
                    if (usr.IsActive.HasValue && usr.IsActive.Value)
                    {
                        usr.IsActive = false;
                    }
                    else
                    {
                        usr.IsActive = true;
                    }
                    if (UserBLL.Update(usr))
                    {
                        BindGrid(id);
                        success.Visible = true;
                        success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> User updated successfully!!.";
                        return;
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong></strong> An error occured. User could not be updated. Kindly try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong></strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
    }
}