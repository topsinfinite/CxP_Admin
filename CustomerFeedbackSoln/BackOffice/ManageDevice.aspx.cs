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
    public partial class ManageDevice : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            error.Visible = false;
            success.Visible = false;
            try
            {
                btnSubmit.Attributes.Add("onclick", " this.disabled = true; this.value='Processing'; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
                if (!IsPostBack)
                {
                   BindGrid();
                    Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlorgfilter);
                    if (User.IsInRole(AdminRole))
                    {
                        dvfilter.Visible = true;
                    }
                   // Utility.BindBranchList(ddlBranch);
                    //Utility.BindUserList(ddluser);

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
            gvDefault.DataSource = LookupBLL.GetDeviceList();
            gvDefault.DataBind();
        }
        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int orgId = int.Parse(ddlOrg.SelectedValue);
                ddlBranch.Items.Clear(); ddluser.Items.Clear();
                Utility.BindBranchList(ddlBranch,orgId);
                Utility.BindUserList(ddluser,orgId);
               
            }
            catch
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
                    gvDefault.DataSource = LookupBLL.GetDeviceList(srch);
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
        private void Reset()
        {
            txtDevNo.Text="";
             ddlBranch.SelectedValue = "";
            ddluser.SelectedValue = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hid.Value == "Update")
                {
                    DeviceTbl b = null; bool rst = false;
                    b = LookupBLL.GetDevice(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.DeviceID = txtDevNo.Text.ToUpper();
                        b.BranchID = int.Parse(ddlBranch.SelectedValue);
                        if (ddluser.SelectedValue != "")
                        {
                            b.UserAssigned = int.Parse(ddluser.SelectedValue);
                        }
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        rst = LookupBLL.UpdateDevice(b);
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
                    DeviceTbl b = new DeviceTbl();
                    b.DeviceID = txtDevNo.Text.ToUpper();
                    b.BranchID = int.Parse(ddlBranch.SelectedValue);
                    if (ddluser.SelectedValue != "")
                    {
                        b.UserAssigned = int.Parse(ddluser.SelectedValue);
                    }
                    if (chk.Checked)
                        b.isActive = true;
                    else
                        b.isActive = false;
                    b.DateAdded = DateTime.Now;
                    result = LookupBLL.AddDevice(b);
                    if (result)
                    {
                        BindGrid();
                        txtDevNo.Text = "";
                        ddlBranch.SelectedValue = "";
                        ddluser.SelectedValue = "";
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

                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                DeviceTbl dvc = LookupBLL.GetDevice(int.Parse(key));
                if (dvc != null)
                {
                    hidkey.Value = dvc.ID.ToString();
                    txtDevNo.Text = dvc.DeviceID;
                    ddlBranch.SelectedValue = dvc.BranchID.HasValue ? dvc.BranchID.ToString() : "";
                    ddluser.SelectedValue = dvc.UserAssigned.HasValue ? dvc.UserAssigned.ToString() : "";
                    if (dvc.isActive.HasValue && dvc.isActive.Value)
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
    }
}