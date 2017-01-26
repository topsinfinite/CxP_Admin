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
    public partial class ManageEventElement : System.Web.UI.Page
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
                    //int evtID = int.Parse(ddlEvt.SelectedValue);
                    BindGrid(orgId);
                    //lstSrvIcon.DataSource = LookupBLL.GetGalleryIcons((int)Utility.IconType.Service);
                    //lstSrvIcon.DataBind();
                    ListViewUpdate.DataSource = LookupBLL.GetGalleryIcons((int)Utility.IconType.EventMetric);
                    ListViewUpdate.DataBind();
                    Utility.BindOrgList(ddlOrg);
                    Utility.BindOrgList(ddlOrgFiler);
                    EventBLL.BindEventList(ddlEvt, orgId);
                    //EventBLL.BindEventMetricList(ddlMet, evtID, orgId);
                    if (User.IsInRole(AdminRole))
                    {
                        EventBLL.BindEventList(ddlEvtfilter);
                    }
                    else
                        EventBLL.BindEventList(ddlEvtfilter, orgId);
                    if (User.IsInRole(AdminRole))
                    {
                        dvfilter.Visible = true;
                        ddlOrgFiler.Visible = true;
                    }
                    if (User.IsInRole(OrgAdminRole))
                    {
                        ddlOrg.SelectedValue = orgId.ToString();
                        ddlOrg.Enabled = false;
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
        private void BindGrid(int orgId = 0)
        {
            if (User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            int evt = int.Parse(ddlEvtfilter.SelectedValue);
            int metID;
           if(ddlMetricFilter.SelectedValue=="..Select a Metric..")
           {
               metID = 0;
           }
            else
            metID = int.Parse(ddlMetricFilter.SelectedValue);

            gvDefault.DataSource = EventBLL.GetEventElementList(evt, metID, orgId);
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
                //if (ddlEvtfilter.SelectedValue != "0" && (ddlMetricFilter.SelectedValue != "0" || ddlMetricFilter.SelectedValue != "..Select a Metric.."))
                if (ddlEvtfilter.SelectedValue != "0" )
                {
                    int srch = int.Parse(ddlEvtfilter.SelectedValue);
                    //int metserch = int.Parse(ddlMetricFilter.SelectedValue);
                    BindGrid();
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

            ddlEvtfilter.SelectedValue = "0";

            ddlMetricFilter.SelectedValue = "0";
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
                   // string iconName = Utility.getIconName(ListViewUpdate);
                    string path = Server.MapPath("~/Upload/");
                    string FileUploadName = Utility.UploadFile(fileUploadDoc, path);

                    //if (!string.IsNullOrEmpty(iconName) || !string.IsNullOrEmpty(FileUploadName))
                    //{
                    
                        MetricElement b = null; bool rst = false;
                        b = EventBLL.GetEventElement(Convert.ToInt32(hidkey.Value));
                        if (b != null)
                        {
                            b.Title = txtTitle.Text.ToUpper();

                            b.EventID = int.Parse(ddlEvt.SelectedValue);
                            b.EventMetricID = int.Parse(ddlMet.SelectedValue);
                            b.Note = txtNote.Text;
                            if (rdIcon.SelectedValue == "1")
                            {
                                if (!string.IsNullOrEmpty(FileUploadName))
                                {
                                    b.Icon = FileUploadName;
                                }
                            }
                            //else
                              //  b.Icon = FileUploadName;
                           
                            rst = EventBLL.UpdateEventElement(b);
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
                    //    error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Ensure that smiley is attached and the image is in correct format. Smiley should not excess 4MB. kindly try again";
                    //}
                }
                else
                {
                    string path = Server.MapPath("~/Upload/");
                    string FileUploadName = Utility.UploadFile(fileUploadDoc, path);
                    string iconName = getIconName(ListViewUpdate);
                    if (!string.IsNullOrEmpty(iconName) || !string.IsNullOrEmpty(FileUploadName))
                    {
                        bool result = false;
                        MetricElement b = new MetricElement();
                        b.Title = txtTitle.Text.ToUpper();
                        
                        //b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        b.EventID = int.Parse(ddlEvt.SelectedValue);
                        b.EventMetricID = int.Parse(ddlMet.SelectedValue);
                        b.Note = txtNote.Text;
                        if (rdIcon.SelectedValue == "0")
                        {
                            b.Icon = iconName;
                        }
                        else
                            b.Icon = FileUploadName;
                        result = EventBLL.AddEventElement(b);
                        if (result)
                        {
                            BindGrid();
                            txtTitle.Text = "";
                            //txtcode.Text = "";
                            //ddlMet.Items.Clear();
                            //ddlEvt.Items.Clear();
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
               // string FileUploadName = Utility.UploadFile(fileUpload1, path);
                string iconName = getIconName(ListViewUpdate);
                if (!string.IsNullOrEmpty(iconName))
                {
                    MetricElement b = null; bool rst = false;
                    b = EventBLL.GetEventElement(Convert.ToInt32(hidkey.Value));
                     
                    if (b != null)
                    {
                        b.Icon = iconName;
                        //b.OrganisationId = int.Parse(ddlOrg.SelectedValue);
                        //b.ModifiedBy = User.Identity.Name;
                        //b.DateModified = DateTime.Now;

                        rst = EventBLL.UpdateEventElement(b);
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
                            error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>An error occurred while updating icon. kindly try again";
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

                //ddlMet.Items.Clear();
                //ddlEvt.Items.Clear();
                hid.Value = "Update";
                //dvEdit.Visible = true;
                btnUpdate.Visible = true;
                btnSubmit.Text = "Update";
                
                string key = gvDefault.SelectedDataKey.Value.ToString();
                //EventBLL.BindEventMetricList(ddlMet, key, orgId);
                MetricElement rg = EventBLL.GetEventElement(int.Parse(key));
                if (rg != null)
                {
                    hidkey.Value = rg.ID.ToString();
                    txtTitle.Text = rg.Title;
                    //ddlOrg.SelectedValue = rg.OrganisationId.HasValue ? rg.OrganisationId.ToString() : "";
                    ddlEvt.SelectedValue = rg.EventID.HasValue ? rg.EventID.ToString() : "";
                    int evtID = int.Parse(ddlEvt.SelectedValue);
                  
                     string metID= rg.EventMetricID.HasValue ? rg.EventMetricID.ToString() : "";

                    if (User.IsInRole(OrgAdminRole))
                        orgId = (int)Session["orgid"];
                    //var q = EventBLL.GetSelectedEventMetric(Convert.ToInt32(metID), orgId).SingleOrDefault();
                    var q = EventBLL.GetSelectedEventMetric(Convert.ToInt32(metID), orgId).SingleOrDefault();
                    string selectedValue = q.Label;
                    ddlMet.Items.Clear();
                   
                    ddlMet.Items.Insert(0, new ListItem(selectedValue, metID));
                    EventBLL.BindEventMetricList(ddlMet, evtID, orgId);
                    
                    txtNote.Text = rg.Note;
                   // rdIcon.SelectedValue = "0";
                    //ddlMet.Items.Clear();
                    
                }
                BindGrid();

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

        protected void rdIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdIcon.SelectedValue == "0")
            {
                dvIcon.Visible = true;
                dvUpload.Visible = false;
            }
            else
            {
                dvUpload.Visible = true;
                dvIcon.Visible = false;
            }
        }

        protected void ddlEvt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMet.Items.Clear();
            if (ddlEvt.SelectedItem.Text == "..Select an Event..")
            {
                ddlMet.Items.Clear();
            }
            else
            {
                int evtID = int.Parse(ddlEvt.SelectedValue);
                orgId = (int)Session["orgid"];
                ddlMet.Items.Insert(0, "Select a metric..");
                EventBLL.BindEventMetricList(ddlMet, evtID, orgId);
                
               string test = ddlMet.SelectedValue;

            }
        }

        protected void ddlEvtfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMetricFilter.Items.Clear();
            ddlMetricFilter.Items.Insert(0, "..Select a Metric..");

            BindMetric();
        }

        private void BindMetric()
        {
            if (User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            int evt = int.Parse(ddlEvtfilter.SelectedValue);
            EventBLL.BindEventMetricList(ddlMetricFilter, evt, orgId);
             
        }

        protected void ddlMetricFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOrgFiler_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEvtfilter.Items.Clear();
            ddlEvtfilter.Items.Insert(0, "..Select a Metric..");

            LoadEvents();
        }

        private void LoadEvents()
        {

            if (User.IsInRole(OrgAdminRole))
                orgId = (int)Session["orgid"];
            orgId = int.Parse(ddlOrgFiler.SelectedValue);
            
            EventBLL.BindEventList(ddlEvtfilter, orgId);
            
        }
    }
}