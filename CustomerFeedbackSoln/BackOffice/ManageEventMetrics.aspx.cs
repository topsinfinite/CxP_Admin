using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class ManageEventMetrics : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string OrgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private int orgId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    orgId = (int)Session["orgid"];
                    BindGrid(orgId);
                    //lstSrvIcon.DataSource = LookupBLL.GetGalleryIcons((int)Utility.IconType.Service);
                    //lstSrvIcon.DataBind();
                    ListViewUpdate.DataSource = LookupBLL.GetGalleryIcons((int)Utility.IconType.EventMetric);
                    ListViewUpdate.DataBind();
                    Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlorgfilter);
                    EventBLL.BindEventList(ddlEvt, orgId);
                    if (User.IsInRole(AdminRole))
                    {
                        dvfilter.Visible = true;
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
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        private void BindGrid(int orgId = 0)
        {
            if (User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            gvDefault.DataSource = EventBLL.GetEventMetricList(orgId);
            gvDefault.DataBind();
        }
        private string getIconName(ListView lst)
        {
            string val1 = "";
            foreach (ListViewItem _item in lst.Items)
            {
                HtmlInputCheckBox c1 = (HtmlInputCheckBox)_item.FindControl("c1");
                if (c1.Checked)
                {
                    //Get the individual values from DataKeyNames array
                    //instead of checkbox value.
                    val1 = c1.Value;
                    break;
                    //val1 = lstSrvIcon.DataKeys[_item.DataItemIndex].Values[0].ToString();
                    // string val2 = lv1.DataKeys[_item.DataItemIndex].Values[1].ToString();
                }
            }

            return val1;
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
            success.Visible = false;
            success.InnerHtml = "";
            error.Visible = false;
            error.InnerHtml = "";
            try
            {
                if (hid.Value == "Update")
                {
                    string iconName = Utility.getIconName(ListViewUpdate);
                    //if (!string.IsNullOrEmpty(iconName))
                    //{
                        EventMetric b = null; bool rst = false;
                        b = EventBLL.GetEventMetric(Convert.ToInt32(hidkey.Value));
                        if (b != null)
                        {
                            b.Label = txtName.Text.ToUpper();
                            b.Title = txtTitle.Text;
                            b.EventID = int.Parse(ddlEvt.SelectedValue);
                            //b.Icon = iconName;
                            //b.ModifiedBy = User.Identity.Name;
                            //b.DateModified = DateTime.Now;
                            //b.Code = txtcode.Text;
                            if (chk.Checked)
                                b.isActive = true;
                            else
                                b.isActive = false;
                            rst = EventBLL.UpdateEventMetric(b);
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
                    //}
                    //else
                    //{
                    //    error.Visible = true;
                    //    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure an icon is select. kindly try again";
                    //}
                }
                    
                else
                {
                    string path = Server.MapPath("~/Upload/");
                    //string FileUploadName = Utility.UploadFile(fileUploadDoc, path);
                    string iconName = getIconName(ListViewUpdate);
                    if (!string.IsNullOrEmpty(iconName))
                    {
                        bool result = false;
                        bool metricLimit = false;
                        EventMetric b = new EventMetric();
                        b.Label = txtName.Text.ToUpper();
                        b.Icon = iconName;
                        b.Title = txtTitle.Text;
                        //b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        b.EventID = int.Parse(ddlEvt.SelectedValue);
                        //b.AddedBy = User.Identity.Name;
                        //b.DatedAdded = DateTime.Now;
                        // b.Code = txtcode.Text;
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;

                        metricLimit = EventBLL.LimitingEventMetric(int.Parse(ddlEvt.SelectedValue));

                        if (metricLimit)
                        {
                            error.Visible = true;
                            error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Maximum number exceeded for metric! You cannot have more than 4 metrics per event.";
                        }
                        else
                        {
                            result = EventBLL.AddEventMetric(b);
                            if (result)
                            {
                                BindGrid();
                                txtName.Text = "";
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
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Record could Not added. Kindly select an icon and try again. If error persist contact Administrator!!.";
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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Server.MapPath("~/Upload/");
                //string FileUploadName = Utility.UploadFile(fileUpload1, path);
                string iconName = getIconName(ListViewUpdate);
                if (!string.IsNullOrEmpty(iconName))
                {
                    EventMetric b = null; bool rst = false;
                     b = EventBLL.GetEventMetric(int.Parse(hidkey.Value));
                    if (b != null)
                    {
                        b.Icon = iconName;
                        //b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        //b.ModifiedBy = User.Identity.Name;
                        //b.DateModified = DateTime.Now;

                        rst = EventBLL.UpdateEventMetric(b);
                        if (rst != false)
                        {
                            BindGrid();
                            success.Visible = true;
                            success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record updated successfully!!.";
                            return;
                        }
                        else
                        {
                            error.Visible = true;
                            error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>An error occurred while updating smiley. kindly try again";
                            return;
                        }
                    }
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure that icon is selected from gallery. kindly try again";
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>A problem has occurred while submitting your data. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void gvDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                hid.Value = "Update";
                dvEdit.Visible = true;
                btnUpdate.Visible = true;
                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                EventMetric rg = EventBLL.GetEventMetric(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                    txtName.Text = rg.Label;
                    txtTitle.Text = rg.Title;
                    //ddlOrg.SelectedValue = rg.OrganisationId.HasValue ? rg.OrganisationId.ToString() : "";
                    ddlEvt.SelectedValue = rg.EventID.HasValue ? rg.EventID.ToString() : "";
                    //Image1.ImageUrl = "~/Upload/" + rg.ServiceIcon;
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
    }
}