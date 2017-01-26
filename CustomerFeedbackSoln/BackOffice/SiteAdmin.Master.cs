using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class SiteAdmin : System.Web.UI.MasterPage
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string OrgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }
                if (Session["user"] == null)
                {
                    Response.Redirect("~/Login.aspx?session=1", false);
                    return;
                }
                if (!IsPostBack)
                {
                    if (HttpContext.Current.User.IsInRole(AdminRole))
                    {
                        dvAdminSetup.Visible = true;
                        dvLookup.Visible = true;
                    }
                    if (HttpContext.Current.User.IsInRole(OrgAdminRole))
                    {
                        dvLookup.Visible = true;
                    }
                    Label lblfnm = (Label)LoginView1.FindControl("lbFName");
                    Label lblBrch = (Label)LoginView1.FindControl("lnBranch");
                    Label lblRole = (Label)LoginView1.FindControl("lbRole");
                    AppUser userdata = new AppUser();
                    if (Session["user"] != null)
                    {
                        userdata = (AppUser)Session["user"];
                        lblfnm.Text = userdata.FullName.ToUpper();
                        lblBrch.Text = userdata.Branch.Name.ToUpper();
                        lblRole.Text = Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name).Count() > 0 ? Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name)[0] : "";

                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx?session=1", false);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
                return;
            }
        }

        protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
        {
            //Session.Abandon();
        }
    }
}