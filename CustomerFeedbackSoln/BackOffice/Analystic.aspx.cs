using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using Microsoft.Reporting.WebForms;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class Analystic : System.Web.UI.Page
    {
        private string orgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private string hoAdminRole = ConfigurationManager.AppSettings["HoAdminRole"].ToString();
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string RgMgrRole = ConfigurationManager.AppSettings["RegMgrRole"].ToString();
        private string AreaMgrRole = ConfigurationManager.AppSettings["AreaMgrRole"].ToString();
        private string BrchMgrRole = ConfigurationManager.AppSettings["BranchMgrRole"].ToString();
        ReportParameter[] rpt; CultureInfo culture = new CultureInfo("en-GB");
        ReportParameter[] rptbar;
        ReportParameter[] rptcmt;
        ReportParameter[] rptstf;
        ReportParameter[] rptcat;
        DBAccess db = new DBAccess();
        DBAccess db2 = new DBAccess();
        DBAccess db3 = new DBAccess();
        DBAccess db4 = new DBAccess();
        DBAccess db5 = new DBAccess();
        AppUser usr = new AppUser(); 
          int orgId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try{
               if (!User.Identity.IsAuthenticated || Session["user"] == null)
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }
                usr = (AppUser)Session["user"];
                orgId = (int)Session["orgid"];
                DateTime sDAte=DateTime.Now.AddDays(-30);
                DateTime eDate=DateTime.Now;
                if (!IsPostBack)
                {
                    BindDashBoard(sDAte, eDate);
                    if (User.IsInRole(orgAdminRole) || User.IsInRole(RgMgrRole) || User.IsInRole(AreaMgrRole) || User.IsInRole(BrchMgrRole))
                    {
                        Utility.BindRegionList(ddlRegion, orgId);
                        Utility.BindAreaList(ddlArea, orgId);
                        Utility.BindBranchList(ddlBranch, orgId);
                    }
                    if (User.IsInRole(AdminRole))
                    {
                        Utility.BindRegionList(ddlRegion);
                        Utility.BindAreaList(ddlArea);
                        Utility.BindBranchList(ddlBranch);
                    }
                    lbrange.Text = "Report From: " + sDAte.ToLongDateString() + " To: " + eDate.ToLongDateString();
                }
            }catch(Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>An error occurred. Please try again";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime sdate;
                DateTime edate;
                if (DateTime.TryParseExact(txtfrom.Text, "dd/MM/yyyy", culture, DateTimeStyles.None, out sdate) && DateTime.TryParseExact(txtto.Text, "dd/MM/yyyy", culture, DateTimeStyles.None, out edate))
                {
                    BindDashBoard(sdate, edate);
                }
                else
                {
                    error.Visible = true;
                    error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong> Invalid date format. Acceptable formt(dd/MM/yyyy). Please try again";
                }
            }
            catch (Exception ex)
            {
                error.Visible = true;
                error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a>An error occurred. Please try again";
                Utility.WriteError("Error: " + ex.Message);
            }
        }

       protected void BindDashBoard(DateTime sDAte,DateTime eDate)
        {
            try
            {
                
                usr = (AppUser)Session["user"];
                rpt = new ReportParameter[6]; 
                rpt[0] = new ReportParameter("fromDate", sDAte.ToShortDateString());
                rpt[1] = new ReportParameter("toDate", eDate.ToShortDateString());
                rpt[2] = new ReportParameter("branchId", "1");
                rpt[3] = new ReportParameter("areaId", "1");
                rpt[4] = new ReportParameter("regionId", "1");
                rpt[5] = new ReportParameter("orgId", "1");

                rptbar = new ReportParameter[6];
                rptbar[0] = new ReportParameter("fromDate", sDAte.ToShortDateString());
                rptbar[1] = new ReportParameter("toDate", eDate.ToShortDateString());
                rptbar[2] = new ReportParameter("branchId", "1");
                rptbar[3] = new ReportParameter("areaId", "1");
                rptbar[4] = new ReportParameter("regionId", "1");
                rptbar[5] = new ReportParameter("orgId", "1");

                rptcmt = new ReportParameter[6];
                rptcmt[0] = new ReportParameter("fromDate", sDAte.ToShortDateString());
                rptcmt[1] = new ReportParameter("toDate", eDate.ToShortDateString());
                rptcmt[2] = new ReportParameter("branchId", "1");
                rptcmt[3] = new ReportParameter("areaId", "1");
                rptcmt[4] = new ReportParameter("regionId", "1");
                rptcmt[5] = new ReportParameter("orgId", "1");

                rptstf = new ReportParameter[6];
                rptstf[0] = new ReportParameter("fromDate", sDAte.ToShortDateString());
                rptstf[1] = new ReportParameter("toDate", eDate.ToShortDateString());
                rptstf[2] = new ReportParameter("branchId", "1");
                rptstf[3] = new ReportParameter("areaId", "1");
                rptstf[4] = new ReportParameter("regionId", "1");
                rptstf[5] = new ReportParameter("orgId", "1");

                rptcat = new ReportParameter[6];
                rptcat[0] = new ReportParameter("fromDate", sDAte.ToShortDateString());
                rptcat[1] = new ReportParameter("toDate", eDate.ToShortDateString());
                rptcat[2] = new ReportParameter("branchId", "1");
                rptcat[3] = new ReportParameter("areaId", "1");
                rptcat[4] = new ReportParameter("regionId", "1");
                rptcat[5] = new ReportParameter("orgId", "1");

                db.AddParameter("@fromDate", sDAte);
                db.AddParameter("@toDate", eDate);
                db.AddParameter("@orgId", orgId);

                db2.AddParameter("@fromDate", sDAte);
                db2.AddParameter("@toDate", eDate);
                db2.AddParameter("@orgId", orgId);

                db3.AddParameter("@fromDate", sDAte);
                db3.AddParameter("@toDate", eDate);
                db3.AddParameter("@orgId", orgId);

                db4.AddParameter("@fromDate", sDAte);
                db4.AddParameter("@toDate", eDate);
                db4.AddParameter("@orgId", orgId);

                db5.AddParameter("@fromDate", sDAte);
                db5.AddParameter("@toDate", eDate);
                db5.AddParameter("@orgId", orgId);

                if (User.IsInRole(orgAdminRole) || User.IsInRole(AdminRole))
                {
                    lbview.Text = "Organisation Wide Customer Feedback View";
                    dvRegion.Visible = true;
                    dvArea.Visible = true;
                    dvBranch.Visible = true;
                    int regionId = 0; int areaId = 0; int branchId = 0;
                    if (ddlRegion.SelectedValue != "")
                    {
                        regionId = int.Parse(ddlRegion.SelectedValue);
                        db.AddParameter("@RegionID", regionId);
                        db2.AddParameter("@RegionID", regionId);
                        db3.AddParameter("@RegionID", regionId);
                        db4.AddParameter("@RegionID", regionId);
                        db5.AddParameter("@RegionID", regionId);
                    }
                    if (ddlArea.SelectedValue != "")
                    {
                        areaId = int.Parse(ddlArea.SelectedValue);
                        db.AddParameter("@AreaID", areaId);
                        db2.AddParameter("@AreaID", areaId);
                        db3.AddParameter("@AreaID", areaId);
                        db4.AddParameter("@AreaID", areaId);
                        db5.AddParameter("@AreaID", areaId);
                    }
                    if (ddlBranch.SelectedValue != "")
                    {
                        branchId = int.Parse(ddlBranch.SelectedValue);
                        db.AddParameter("@branchID", branchId);
                        db2.AddParameter("@branchID", branchId);
                        db3.AddParameter("@branchID", branchId);
                        db4.AddParameter("@branchID", branchId);
                        db5.AddParameter("@branchID", branchId);
                    }
                }
                if (User.IsInRole(RgMgrRole))
                {
                    lbview.Text = usr.Branch.Area.Region.Name + " Region Customer Feedback View";
                    dvArea.Visible = true;
                    dvBranch.Visible = true;
                    int areaId = 0; int branchId = 0;
                    if (ddlArea.SelectedValue != "")
                    {
                        areaId = int.Parse(ddlArea.SelectedValue);
                        db.AddParameter("@AreaID", areaId);
                        db2.AddParameter("@AreaID", areaId);
                        db3.AddParameter("@AreaID", areaId);
                        db4.AddParameter("@AreaID", areaId);
                        db5.AddParameter("@AreaID", areaId);
                    }
                    if (ddlBranch.SelectedValue != "")
                    {
                        branchId = int.Parse(ddlBranch.SelectedValue);
                        db.AddParameter("@branchID", branchId);
                        db2.AddParameter("@branchID", branchId);
                        db3.AddParameter("@branchID", branchId);
                        db4.AddParameter("@branchID", branchId);
                        db5.AddParameter("@branchID", branchId);

                    }
                    db.AddParameter("@RegionID", usr.Branch.Area.RegionalID);
                    db2.AddParameter("@RegionID", usr.Branch.Area.RegionalID);
                    db3.AddParameter("@RegionID", usr.Branch.Area.RegionalID);
                    db4.AddParameter("@RegionID", usr.Branch.Area.RegionalID);
                    db5.AddParameter("@RegionID", usr.Branch.Area.RegionalID);
                }
                if (User.IsInRole(AreaMgrRole))
                {
                    lbview.Text = usr.Branch.Area.Name + " Area Customer Feedback View";
                    dvBranch.Visible = true;
                    int branchId = 0;
                    if (ddlBranch.SelectedValue != "")
                    {
                        branchId = int.Parse(ddlBranch.SelectedValue);
                        db.AddParameter("@branchID", branchId);
                        db2.AddParameter("@branchID", branchId);
                        db3.AddParameter("@branchID", branchId);
                        db4.AddParameter("@branchID", branchId);
                        db5.AddParameter("@branchID", branchId);
                    }
                    db.AddParameter("@AreaID", usr.Branch.AreaID);
                    db2.AddParameter("@AreaID", usr.Branch.AreaID);
                    db3.AddParameter("@AreaID", usr.Branch.AreaID);
                    db4.AddParameter("@AreaID", usr.Branch.AreaID);
                    db5.AddParameter("@AreaID", usr.Branch.AreaID);
                }
                if (User.IsInRole(BrchMgrRole))
                {
                    lbview.Text = usr.Branch.Name + " Branch Customer Feedback View";
                   
                    db.AddParameter("@branchID", usr.BranchID);
                    db2.AddParameter("@branchID", usr.BranchID);
                    db3.AddParameter("@branchID", usr.BranchID);
                    db4.AddParameter("@branchID", usr.BranchID);
                    db5.AddParameter("@branchID", usr.BranchID);
                }

                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet("SmileyActionRating");
                 
                DataSet ds2 = new DataSet();
                ds2 = db2.ExecuteDataSet("SmileyActionServiceRating");

                DataSet ds3 = new DataSet();
                ds3 = db3.ExecuteDataSet("FeedbackComment");

                DataSet ds4 = new DataSet();
                ds4 = db4.ExecuteDataSet("SmileyActionStaffRating");


                DataSet ds5 = new DataSet();
                ds5 = db5.ExecuteDataSet("SmileyActionCategoryRating");


                ReportDataSource datasource = new
                  ReportDataSource("dsFeedback",
                  ds.Tables[0]);

                ReportDataSource datasource2 = new
                  ReportDataSource("dsFeedback",
                  ds2.Tables[0]);

                ReportDataSource datasource3 = new
                 ReportDataSource("dsFeedback",
                 ds3.Tables[0]);

                ReportDataSource datasource4 = new
                ReportDataSource("dsFeedback",
                ds4.Tables[0]);

                ReportDataSource datasource5 = new
               ReportDataSource("dsFeedback",
               ds5.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(rpt);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
              
                ReportViewer2.LocalReport.DataSources.Clear();
                ReportViewer2.LocalReport.SetParameters(rptbar);
                ReportViewer2.LocalReport.DataSources.Add(datasource2);
                ReportViewer2.LocalReport.Refresh();

                ReportViewer3.LocalReport.DataSources.Clear();
                ReportViewer3.LocalReport.SetParameters(rptcmt);
                ReportViewer3.LocalReport.DataSources.Add(datasource3);
                ReportViewer3.LocalReport.Refresh();

                ReportViewer4.LocalReport.DataSources.Clear();
                ReportViewer4.LocalReport.SetParameters(rptstf);
                ReportViewer4.LocalReport.DataSources.Add(datasource4);
                ReportViewer4.LocalReport.Refresh();

                ReportViewer5.LocalReport.DataSources.Clear();
                ReportViewer5.LocalReport.SetParameters(rptcat);
                ReportViewer5.LocalReport.DataSources.Add(datasource5);
                ReportViewer5.LocalReport.Refresh();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    // lbmsg.Text = "Sorry, no products under this category!";
                }
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    // lbmsg.Text = "Sorry, no products under this category!";
                }
                if (ds3.Tables[0].Rows.Count == 0)
                {
                    // lbmsg.Text = "Sorry, no products under this category!";
                }
                if (ds4.Tables[0].Rows.Count == 0)
                {
                    // lbmsg.Text = "Sorry, no products under this category!";
                }
                if (ds5.Tables[0].Rows.Count == 0)
                {
                    // lbmsg.Text = "Sorry, no products under this category!";
                }
                lbrange.Text = "Report From: " + sDAte.ToLongDateString() + " To: " + eDate.ToLongDateString();
                return;
            }
            catch (Exception ex)
            {
                throw ex;
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
    }
}