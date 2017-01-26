using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class ManageLabels : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string OrgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private int orgId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                error.Visible = false; success.Visible = false;
                 
                if (!IsPostBack)
                   {
                     orgId = (int)Session["orgid"];
                     BindGrid(orgId);
                    CustomerFeedbackSoln.BLL.Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlorgfilter);
                    if (User.IsInRole(AdminRole))
                    {
                        dvfilter.Visible = true;
                        ddlOrg.Enabled = false;
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
                Utility.WriteError("Error: " + ex.InnerException);
            }
        }
        private void BindGrid(int orgId = 0)
        {
            if (User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            gvDefault.DataSource = LookupBLL.GetLabelList(orgId);
            gvDefault.DataBind();
        }
        protected void gvDefault_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";
                dvMain.Visible = true;
                 
                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                LabelSetting rg = LookupBLL.GetLabelSetting(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                    txtHome.Text = rg.HomeLabel;
                    txtCatAwe.Text = rg.CategoryLabelAwesome;
                    txtCatBad.Text = rg.CategoryLabelBad;
                    txtSerAwe.Text = rg.ServiceLabelAwesome;
                    txtSerBad.Text = rg.ServiceLabelBad;
                    txtSerInd.Text = rg.ServiceLabelIndifferent;
                    txtStfAwe.Text = rg.StaffLabelAwesome;
                    txtStfBad.Text = rg.StaffLabelBad;

                    ddlOrg.SelectedValue = rg.OrgId.HasValue ? rg.OrgId.ToString() : "";
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong> An error occured. Kindly try again. If error persist contact Administrator!!.";
                Utility.WriteError("Error: " + ex.InnerException);
            }
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
                LabelSetting b = null; bool rst = false;
                b = LookupBLL.GetLabelSetting(Convert.ToInt32(hidkey.Value));
                if (b != null)
                {
                    b.HomeLabel = txtHome.Text.ToUpper();
                    b.CategoryLabelAwesome = txtCatAwe.Text.ToUpper();
                    b.CategoryLabelBad = txtCatBad.Text.ToUpper();
                    b.ServiceLabelAwesome = txtSerAwe.Text.ToUpper();
                    b.ServiceLabelBad = txtSerBad.Text.ToUpper();
                    b.ServiceLabelIndifferent = txtSerInd.Text.ToUpper();
                    b.StaffLabelAwesome = txtStfAwe.Text.ToUpper();
                    b.StaffLabelBad = txtStfBad.Text.ToUpper();
                    rst = LookupBLL.UpdateLabelSetting(b);
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
            catch (Exception ex)
            {
            }
        }
    }
}