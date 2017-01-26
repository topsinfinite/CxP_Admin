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
    public partial class ManageArea : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                error.Visible = false; success.Visible = false;
                if (!IsPostBack)
                {
                    BindGrid();
                    Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlorgfilter);
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
            gvDefault.DataSource = BranchBLL.GetAreaList();
            gvDefault.DataBind();
        }
        protected void gvDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";

                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();
                Utility.BindRegionList(ddlRegion, int.Parse(key));
                Area rg = BranchBLL.GetArea(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                    txtName.Text = rg.Name;
                    ddlRegion.SelectedValue = rg.RegionalID.ToString();
                    ddlOrg.SelectedValue = rg.Region.OrganisationID.ToString();
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
                gvDefault.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch
            { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    Area b = null; bool rst = false;
                    b = BranchBLL.GetArea(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.Name = txtName.Text.ToUpper();
                        b.RegionalID = int.Parse(ddlRegion.SelectedValue);
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        rst = BranchBLL.UpdateArea(b);
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
                    bool result = false;
                    Area b = new Area();
                    b.Name = txtName.Text.ToUpper();
                    b.RegionalID = int.Parse(ddlRegion.SelectedValue);
                    if (chk.Checked)
                        b.isActive = true;
                    else
                        b.isActive = false;
                    result = BranchBLL.AddArea(b);
                    if (result)
                    {
                        BindGrid();
                        txtName.Text = "";
                        //ddlRegion.SelectedValue = "";
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
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>A problem has occurred while submitting your data. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int orgId=int.Parse(ddlOrg.SelectedValue);
                ddlRegion.Items.Clear();
                 Utility.BindRegionList(ddlRegion,orgId);
            }catch
            {
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlorgfilter.SelectedValue != "")
                {
                    int srch = int.Parse(ddlorgfilter.SelectedValue);
                    gvDefault.DataSource = BranchBLL.GetAreaList(srch);
                    gvDefault.DataBind();
                }
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