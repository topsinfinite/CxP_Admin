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
    public partial class ManageEventDevice : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string OrgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private int orgId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                orgId = (int)Session["orgid"];
                BindGrid(orgId);
                Utility.BindOrgList(ddlOrg);

                EventBLL.BindEventList(ddlEvt);
                EventBLL.BindEventDeviceList(ddlDev, orgId);
            if (User.IsInRole(AdminRole))
            {
                dvfilter.Visible = true;
            }
            if (User.IsInRole(OrgAdminRole))
            {
                ddlOrg.SelectedValue = orgId.ToString();
                ddlOrg.Enabled = false;
                ddlDev.Items.Clear();
                ddlEvt.Items.Clear();
                int OrgId = 0;
                if (ddlOrg.SelectedValue != "")
                {
                    OrgId = int.Parse(ddlOrg.SelectedValue);
                }
                EventBLL.BindEventDeviceList(ddlDev, OrgId);
                EventBLL.BindEventList(ddlEvt, OrgId);

            }
                
            }
            
        }
        private void BindGrid(int orgId = 0)
        {
            if (User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            gvDefault.DataSource = EventBLL.GetEventDeviceList(orgId);
            gvDefault.DataBind();
        }
        protected void ddlDev_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int devId = int.Parse(ddlDev.SelectedValue);
                //ddlEvt.Items.Clear();
                //Utility.BindEventList(ddlEvt, devId);              
            }
            catch
            {

            }
        }

        protected void ddlEvt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                success.Visible = false;
                success.InnerHtml = "";
                 error.Visible = false;
                 error.InnerHtml = "";
                if (hid.Value == "Update")
                {
                    EventDevice b = null; bool rst = false;
                    bool existing = false;
                    //bool existing2;
                    b = EventBLL.GetEventDevice(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        existing = EventBLL.ExistingDevice(Convert.ToInt32(b.DeviceID)); 
                        int deviceID=Convert.ToInt32(ddlDev.SelectedValue);
                        if (existing && b.DeviceID !=deviceID)
                                //if (existing && b.isActive == true)
                            {
                                error.Visible = true;
                                error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record already exists. You cannot assign a device to another event!";
                                return;
                            }
                            else
                            {
                        b.DeviceID = int.Parse(ddlDev.SelectedValue);
                        b.EventID = int.Parse(ddlEvt.SelectedValue);
                        b.OrganizationID = int.Parse(ddlOrg.SelectedValue);

                        if (chk.Checked)
                            b.isActive = 1;
                        else
                            b.isActive = 0;
                        //existing2 = EventBLL.DeviceExistForEvent(Convert.ToInt32(b.EventID), Convert.ToInt32(b.DeviceID));
                        //if (existing2)
                        //{
                        //    error.Visible = true;
                        //    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record already exists. You cannot assign one device to an event twice!.";
                        //    return;
                        //}
                        //else
                        //{
                            
                                rst = EventBLL.UpdateEventDevice(b);
                                if (rst != false)
                                {
                                    BindGrid();
                                    success.Visible = true;
                                    success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record updated successfully!!.";
                                    ddlDev.ClearSelection();
                                    ddlEvt.ClearSelection();
                                    btnSubmit.Text = "Add Device";
                                    return;
                                }
                            //}
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
                    bool existing = false;
                    bool existing2 = false;
                    EventDevice b = new EventDevice();
                    b.DeviceID = int.Parse(ddlDev.SelectedValue);
                    b.EventID = int.Parse(ddlEvt.SelectedValue);
                    b.OrganizationID = int.Parse(ddlOrg.SelectedValue);

                    if (chk.Checked)
                        b.isActive = 1;
                    else
                        b.isActive = 0;
                    b.DateAdded = DateTime.Now;
                    existing2 = EventBLL.DeviceExistForEvent(Convert.ToInt32(b.EventID), Convert.ToInt32(b.DeviceID));
                    if (existing2)
                    {
                        error.Visible = true;
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record already exists. You cannot assign one device to an event twice!.";
                        return;

                    }
                    else
                    {
                        existing = EventBLL.ExistingDevice(Convert.ToInt32(b.DeviceID));
                        
                        //if (existing && b.isActive == true)
                        if (existing)
                        {
                            error.Visible = true;
                           // error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record already exists. You cannot have one active device for multiple events, deactivate the existing one.";
                            error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record exists: Device already assigned to an event!";
                            return;
                        }
                        else
                        {
                            result = EventBLL.AddDevice(b);
                            if (result)
                            {
                                //BindGrid(); 
                                ddlDev.Items.Clear();
                                ddlEvt.Items.Clear();

                                //ddlDev.SelectedValue = "";
                                //ddlEvt.SelectedValue = "";
                                //ddlOrg.SelectedValue = "";
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
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>A problem has occurred while submitting your data. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
         }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnClr_Click(object sender, EventArgs e)
        {

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

                EventDevice dvc = EventBLL.GetEventDevice(int.Parse(key));
                
                if (dvc != null)
                {
                    hidkey.Value = dvc.ID.ToString();

                    ddlOrg.SelectedValue = dvc.OrganizationID.HasValue ? dvc.OrganizationID.ToString() : "";
                    ddlEvt.SelectedValue = dvc.EventID.HasValue ? dvc.EventID.ToString() : "";
                    ddlDev.SelectedValue = dvc.DeviceID.HasValue ? dvc.DeviceID.ToString() : "";
                    if (dvc.isActive.HasValue && dvc.isActive==1)
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
        
        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {   
            try
            {
                //ddlDev.Items.Clear();
                //ddlEvt.Items.Clear();
                //int orgId = 0;
                //if(ddlOrg.SelectedValue!="")
                //{
                //    orgId=int.Parse(ddlOrg.SelectedValue);
                //}
                //EventBLL.BindEventDeviceList(ddlDev, orgId);
                //EventBLL.BindEventList(ddlEvt, orgId);

            }
            catch (Exception ex)
            {
            }
        }
    }
}