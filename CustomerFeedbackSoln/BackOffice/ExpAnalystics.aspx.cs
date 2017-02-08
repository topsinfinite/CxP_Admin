using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class ExpAnalystics : System.Web.UI.Page
    {
        private string orgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private string hoAdminRole = ConfigurationManager.AppSettings["HoAdminRole"].ToString();
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        ReportParameter[] rpt;
        ReportParameter[] rptbar;
        ReportParameter[] rptelembar;
        ReportParameter[] rptDetail; 
        CultureInfo culture = new CultureInfo("en-GB");
        DBAccess db = new DBAccess();
        DBAccess db2 = new DBAccess();
        DBAccess db3 = new DBAccess();
        DBAccess db4 = new DBAccess();
        AppUser usr = new AppUser();
        int orgId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                error.Visible = false;
                if (!User.Identity.IsAuthenticated || Session["user"] == null)
                {
                    Response.Redirect("~/Login.aspx?sessionId=1", false);
                    return;
                }
                if (!IsPostBack)
                {
                    usr = (AppUser)Session["user"];
                    orgId = (int)Session["orgid"];
                    if (User.IsInRole(AdminRole))
                    {
                        dvOrg.Visible = true;
                        dvEvent.Visible = true;
                        Utility.BindOrgList(ddlOrg);
                    }
                    if (User.IsInRole(orgAdminRole))
                    {
                        dvEvent.Visible = true;
                        Utility.BindOrgList(ddlOrg);
                        
                        ddlOrg.SelectedValue = orgId.ToString();
                        ddlOrg.Enabled = false;
                        EventBLL.BindEventList(ddlEvent, orgId);
                         
                        
                    }

                   // BindDashBoard();
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>An error occurred. Please try again";
                Utility.WriteError("Error: " + ex.Message + " \n StackTrace: "+ ex.StackTrace);
            }
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int orgId=int.Parse(ddlOrg.SelectedValue);
                ddlEvent.Items.Clear();
                ddlEvent.Items.Add(new ListItem { Text = "..Select Event..", Value = "" });
                EventBLL.BindEventList(ddlEvent, orgId);
              
            }
            catch
            {
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindDashBoard();
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>An error occurred. Please try again";
                Utility.WriteError("Error: " + ex.Message + " \n StackTrace: " + ex.StackTrace);
            }
        }

        protected void BindDashBoard()
        {
            try {
                usr = (AppUser)Session["user"];
                rpt = new ReportParameter[2];
                rptbar = new ReportParameter[2];
                rptelembar = new ReportParameter[3];
                rptDetail = new ReportParameter[1];
                
                rpt[0] = new ReportParameter("orgId", "1");
                rpt[1] = new ReportParameter("eventId", "1");

                rptelembar[0] = new ReportParameter("orgId", "1");
                rptelembar[1] = new ReportParameter("eventId", "1");
                rptelembar[2] = new ReportParameter("metricId", "1");

                rptbar[0] = new ReportParameter("orgId", "1");
                rptbar[1] = new ReportParameter("eventId", "1");

                rptDetail[0] = new ReportParameter("eventId", "1");
                if (User.IsInRole(AdminRole))
                {

                    if (ddlOrg.SelectedValue != "" && ddlEvent.SelectedValue != "")
                    {
                        lbview.Text = ddlEvent.SelectedItem.Text + " Report & Analystic View";
                        int orgId = int.Parse(ddlOrg.SelectedValue);
                        int eventId = int.Parse(ddlEvent.SelectedValue);
                        db.AddParameter("@orgId", orgId);
                        db.AddParameter("@eventID", eventId);

                        db2.AddParameter("@orgId", orgId);
                        db2.AddParameter("@eventID", eventId);

                        if (ddlMetrics.SelectedValue != "")
                        {
                            int metriId = int.Parse(ddlMetrics.SelectedValue);
                            db3.AddParameter("@orgId", orgId);
                            db3.AddParameter("@eventID", eventId);
                            db3.AddParameter("@metricId", metriId);
                        }

                        db4.AddParameter("@eventId", eventId);
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>kindly select organisation and event to generate report.";
                        return;
                    }
                }
                if (User.IsInRole(orgAdminRole))
                {
                    if(Session["orgId"]==null)
                    {
                        Response.Redirect("~/Login.aspx?sessionId=1", false);
                        return;
                    }
                    orgId = int.Parse(Session["orgId"].ToString());
                    ddlOrg.SelectedValue =orgId.ToString() ;
                    ddlOrg.Enabled = false;
                    if (ddlEvent.SelectedValue != "")
                    {
                        lbview.Text = ddlEvent.SelectedItem.Text + " Report & Analystic View";
                       
                        int eventId = int.Parse(ddlEvent.SelectedValue);
                        db.AddParameter("@orgId", orgId);
                        db.AddParameter("@eventID", eventId);

                        db2.AddParameter("@orgId", orgId);
                        db2.AddParameter("@eventID", eventId);

                        if (ddlMetrics.SelectedValue != "")
                        {
                            int metriId = int.Parse(ddlMetrics.SelectedValue);
                            db3.AddParameter("@orgId", orgId);
                            db3.AddParameter("@eventID", eventId);
                            db3.AddParameter("@metricId", metriId);
                        }

                        db4.AddParameter("@eventId", eventId);
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>kindly select event to generate report.";
                        return;
                    }
                }
                    DataSet ds = new DataSet();
                    ds = db.ExecuteDataSet("EventOverallRating");

                    DataSet ds1 = new DataSet();
                    ds1 = db.ExecuteDataSet("EventMetricRating");

                    DataSet ds2 = new DataSet();
                    ds2 = db3.ExecuteDataSet("EventMetricElementRpt");

                    DataSet ds3 = new DataSet();
                    ds3 = db4.ExecuteDataSet("EventFeedbackComment");

                    ReportDataSource datasource = new ReportDataSource ("dsFeedback",ds.Tables[0]);
                    ReportDataSource datasource1 = new ReportDataSource("dsFeedback", ds1.Tables[0]);
                    ReportDataSource datasource2 = new ReportDataSource("dsFeedback", ds2.Tables[0]);
                    ReportDataSource datasource3 = new ReportDataSource("dsFeedback", ds3.Tables[0]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.SetParameters(rpt);
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.Refresh();

                    ReportViewer2.LocalReport.DataSources.Clear();
                    ReportViewer2.LocalReport.SetParameters(rptbar);
                    ReportViewer2.LocalReport.DataSources.Add(datasource);
                    ReportViewer2.LocalReport.Refresh();

                    ReportViewer3.LocalReport.DataSources.Clear();
                    ReportViewer3.LocalReport.SetParameters(rptDetail);
                    ReportViewer3.LocalReport.DataSources.Add(datasource3);
                    ReportViewer3.LocalReport.Refresh();


                    ReportViewer4.LocalReport.DataSources.Clear();
                    ReportViewer4.LocalReport.SetParameters(rptbar);
                    ReportViewer4.LocalReport.DataSources.Add(datasource1);
                    ReportViewer4.LocalReport.Refresh();

                    ReportViewer5.LocalReport.DataSources.Clear();
                    ReportViewer5.LocalReport.SetParameters(rptelembar);
                    ReportViewer5.LocalReport.DataSources.Add(datasource2);
                    ReportViewer5.LocalReport.Refresh();

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>Sorry no record found for the selected event.";
                    }
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>Sorry no record found for the selected event.";
                    }
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>kindly select event metric value to view dashboard.";
                    }
              
            
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>An error occurred. Please try again";
                Utility.WriteError("Error: " + ex.Message + " \n StackTrace: " + ex.StackTrace);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ReportViewer2.LocalReport.Refresh();
            ReportViewer3.LocalReport.Refresh();
            ReportViewer4.LocalReport.Refresh();
            ReportViewer5.LocalReport.Refresh();
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int eventId = int.Parse(ddlEvent.SelectedValue);
                ddlMetrics.Items.Clear();
                ddlMetrics.Items.Add(new ListItem { Text = "..Select Event Metrics..", Value = "" });
                EventBLL.BindEventMetricList(ddlMetrics,eventId);

            }
            catch
            {
            }
        }
    }
}  