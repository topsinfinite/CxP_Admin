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
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Visible = false;
            success.Visible = false;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string pwd = ""; string oPwd = "";
                pwd = txtnPwd.Text;
                oPwd = txtcPwd.Text; bool isChanged = false;
                MembershipUser usr = Membership.GetUser(User.Identity.Name.Trim());
                if (usr != null)
                {
                    isChanged = usr.ChangePassword(oPwd, pwd);
                    if (isChanged)
                    {
                        AppUser usrAcct = new AppUser();
                        usrAcct = UserBLL.GetUserByUsername(User.Identity.Name.Trim());
                        if (usrAcct != null)
                        {
                            usrAcct.HasChangePwd = true;
                            UserBLL.Update(usrAcct);
                        }
                        txtnPwd.Text = ""; txtcPwd.Text = ""; txtpwdConfirmed.Text = "";
                        success.Visible = true;
                        success.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a> Your Password has been Reset successfully!!.";
                        return;
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a> Sorry...There was an issue while resetting your password. Ensure that password meet the required password strength!!!";
                        return;
                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a> Sorry...your data was not found!!!";
                    return;
                }

            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>  Sorry...There was an issue while resetting your password.Ensure that password meet the required password strength and kindly try again!!!";
                Utility.WriteError(ex.Message);
            }
        }
    }
}