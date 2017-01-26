using CustomerFeedbackSoln.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class OnlineUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            //    string totalUsers = Application["OnlineUsers"].ToString();
            //    lbusrTot.Text = totalUsers;

            //    var onlineUsers = (from a in Membership.GetAllUsers().Cast<MembershipUser>().ToList()
            //                       where a.IsOnline
            //                       select a.UserName).ToList();
            //    string users = "no one";
            //    for (int i = 0; i < onlineUsers.Count; i++)
            //        users = users + " - " + onlineUsers[i];
            //    OnlineNow.Text = users;

                BindGrid();
            }
            catch
            {
            }
        }
        private void BindGrid()
        {
            gvDefault.DataSource = LookupBLL.GetAllUsers();
            gvDefault.DataBind();
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
    }
}