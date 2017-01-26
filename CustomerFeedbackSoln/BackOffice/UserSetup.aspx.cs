using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class UserSetup : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Visible = false; success.Visible = false;
            try
            {
                btnSubmit.Attributes.Add("onclick", " this.disabled = true; this.value='Processing'; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
                if (!IsPostBack)
                {
                    BindGrid();
                    Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlorgfilter);
                   // Utility.BindBranchList(ddlBranch);
                    if (User.IsInRole(AdminRole))
                    {
                        dvfilter.Visible = true;
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
        private void BindGrid()
        {
            gvUsers.DataSource = UserBLL.GetUsersList();
            gvUsers.DataBind();
        }
        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int orgId = int.Parse(ddlOrg.SelectedValue);
                ddlBranch.Items.Clear();
                Utility.BindBranchList(ddlBranch,orgId);
            }
            catch
            {
            }
        }
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            txtuserID.Text = ""; txtEmail.Text = "";
            txtfname.Text = ""; 
            //ddlBranch.SelectedValue = "";
            //ddlRole.SelectedValue = "";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string usrname = txtuserID.Text.Trim();
                MembershipUserCollection usercol = Membership.FindUsersByName(usrname);
                if (usercol.Count != 0)
                {
                    error.Visible = true;
                    error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong> This user already exist, pls add another user";
                    return;
                }
                string email = txtEmail.Text;

                string fname = txtfname.Text;
                AppUser usr = new AppUser();
                usr.FullName = fname;
                usr.UserName = usrname;
                usr.BranchID = Convert.ToInt32(ddlBranch.SelectedValue);
                usr.Email = email;
                usr.IsActive=true;
                usr.HasChangePwd = false;
                usr.RoleName = ddlRole.SelectedValue;
                usr.DateAdded = DateTime.Now;
                
                if (User.Identity.IsAuthenticated)
                    usr.AddedBy = User.Identity.Name;
                MembershipUser user = Membership.CreateUser(usrname, txtPwd.Text, email);
                if (UserBLL.AddUser(usr))
                {
                    Roles.AddUserToRole(usrname, ddlRole.SelectedValue);
                     
                    //sending mail
                    string body = "";
                    string from = ConfigurationManager.AppSettings["exUser"].ToString();
                    string siteUrl = ConfigurationManager.AppSettings["siteUrl"].ToString();
                    string appLogo = ConfigurationManager.AppSettings["appLogoUrl"].ToString();
                    string subject = "User Creation Notification";
                    string FilePath = Server.MapPath("EmailTemplates/");
                    if (File.Exists(FilePath + "NewUserCreated.htm"))
                    {
                        FileStream f1 = new FileStream(FilePath + "NewUserCreated.htm", FileMode.Open);
                        StreamReader sr = new StreamReader(f1);
                        body = sr.ReadToEnd();
                        body = body.Replace("@add_appLogo", appLogo);
                        body = body.Replace("@add_siteUrl", siteUrl);
                        body = body.Replace("@add_fname", fname);
                        body = body.Replace("@add_password", txtPwd.Text);
                        body = body.Replace("@add_username", usrname); //Replace the values from DB or any other source to personalize each mail.  
                        f1.Close();
                    }
                    string rst="";
                    try
                    {
                        rst = Utility.SendMail(email, from, "", subject, body);
                    }
                    catch { }
                    if (rst.Contains("Successful"))
                    {
                        Reset(); BindGrid();
                        success.Visible = true;
                        success.InnerHtml = "  <a href='#' class='close' data-dismiss='alert'>&times;</a> User was successfully created!!. A mail has been sent to the user";
                        return;
                    }
                    else
                    {
                        Reset(); 
                        BindGrid();
                        success.Visible = true;
                        success.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a> User was successfully created!!. A mail could NOT be sent to the user at this time";
                        return;
                    }
                }
                else
                {
                    Membership.DeleteUser(usrname);
                    error.Visible = true;
                    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>  User Account could not be created. Kindly try again. If error persist contact Administrator!!.";
                    return;
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>An unexpected error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }


        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvUsers.SelectedDataKey.Value.ToString());
                Response.Redirect(string.Format("UserDetail.aspx?id={0}", id), false);
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int srch = 0; string fname = "";
                if (ddlorgfilter.SelectedValue != "")
                {
                    srch = int.Parse(ddlorgfilter.SelectedValue);
                }
                if (!string.IsNullOrEmpty(txtsrchname.Value))
                {
                    fname = txtsrchname.Value;
                }
                    gvUsers.DataSource = UserBLL.GetUsersList(srch,fname);
                    gvUsers.DataBind();
                
            }
            catch
            {
            }
        }
        protected void btnClr_Click(object sender, EventArgs e)
        {
            ddlorgfilter.SelectedValue = "";
            BindGrid();
        }
    }
     
}