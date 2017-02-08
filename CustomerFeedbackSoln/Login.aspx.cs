using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;

namespace CustomerFeedbackSoln
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                string rolname = ""; string ursname = ""; string pwd = "";
                bool a = false; bool b = false; bool c = false; bool d = false;
                string val = "";
                ursname = username.Value.Trim();
                pwd = txtpwd.Value.Trim();
                if (Membership.ValidateUser(ursname, pwd))
                {
                    AppUser usr = new AppUser();
                    usr = UserBLL.GetUserByUsername(ursname);
                    if (usr != null)
                    {
                        if (usr.IsActive.HasValue && !usr.IsActive.Value)
                        {
                            error.Visible = true;
                            error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong> Your account has been disabled. Kindly contact Administrator!!.";
                            return;
                        }
                        int orgId = usr.Branch.Area.Region.OrganisationID.HasValue ? usr.Branch.Area.Region.OrganisationID.Value : 0;
                        Session["user"] = usr;
                        Session["orgid"] = orgId;
                        FormsAuthentication.SetAuthCookie(ursname, false);
                        string[] rol = Roles.GetRolesForUser(ursname);
                        if (rol.Length != 0)
                        {
                            rolname = rol[0];

                            if (rolname == ConfigurationManager.AppSettings["BranchOpsRole"].ToString())
                            { a = true; }
                            if(rolname==ConfigurationManager.AppSettings["orgAdminRole"].ToString())
                            {
                                b=true;
                            }
                        }
                        if (usr.HasChangePwd.HasValue && !usr.HasChangePwd.Value)
                        {
                            Response.Redirect("BackOffice/ChangePassword.aspx",false);
                            return;

                        }
                        if (a)
                        {
                            Response.Redirect("Default.aspx", false);
                            return;
                        }
                        else if (b)
                        {
                            Response.Redirect("BackOffice/ExpAnalystics.aspx", false);
                            return;
                        }else
                        {
                            Response.Redirect("BackOffice/Dashboard.aspx", false);
                            return;
                        }
                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong> Invalid credentials. kindly try again!!!";
                    return;
                }
            }
            catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void btnAppr_Click(object sender, EventArgs e)
        {
            try
            {
                modalErr.Visible = false;
                if (string.IsNullOrEmpty(txtemail.Value))
                {
                    modalErr.Visible = true;
                    modalErr.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong> Email cannot be empty. kindly try again!!!";
                    mpeAppr.Show();
                    return;
                }
                if (!Regex.IsMatch(txtemail.Value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    modalErr.Visible = true;
                    modalErr.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong> Invalid email. kindly try again!!!";
                    mpeAppr.Show();
                    return;
                }
                string email = txtemail.Value; ;
                string newPwd = "";  string fname = "";
                AppUser userdata = new AppUser(); MembershipUser usr=null;
                userdata = UserBLL.GetUserByEmail(email.Trim());
                if (userdata != null)
                {
                    fname = userdata.FullName;
                    usr = Membership.GetUser(userdata.UserName.Trim());
                }
                if (usr != null)
                {
                    newPwd = usr.ResetPassword();
                    
                    //sending mail
                    string body = "";
                    string from = ConfigurationManager.AppSettings["exUser"].ToString();
                    string subject = "Password Recovery Notification";
                    string FilePath = Server.MapPath("EmailTemplates/");
                    if (File.Exists(FilePath + "ForgotPassword.htm"))
                    {
                        FileStream f1 = new FileStream(FilePath + "ForgotPassword.htm", FileMode.Open);
                        StreamReader sr = new StreamReader(f1);
                        body = sr.ReadToEnd();
                        
                        body = body.Replace("@add_fname", fname);
                        body = body.Replace("@add_username", email); //Replace the values from DB or any other source to personalize each mail.  
                        body = body.Replace("@add_password", newPwd);
                        f1.Close();
                    }
                    string rst = Utility.SendMail(email, from, "", subject, body);
                    if (rst.Contains("Successful"))
                    {
                        success.Visible = true;
                        success.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a> A new password has been sent to your mailbox. Kindly check your mail and logon with your new password";
                        return;
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a> Sorry...Problem Sending Mail. Kindly try again later!!!";
                        return;
                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a> Sorry...The email you supplied does not exist!!!";
                    return;
                }

            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
    }
}