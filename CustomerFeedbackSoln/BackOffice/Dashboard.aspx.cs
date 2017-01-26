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
    public partial class Dashboard : System.Web.UI.Page
    {
        private string orgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string RgMgrRole = ConfigurationManager.AppSettings["RegMgrRole"].ToString();
        private string AreaMgrRole = ConfigurationManager.AppSettings["AreaMgrRole"].ToString();
        private string BrchMgrRole = ConfigurationManager.AppSettings["BranchMgrRole"].ToString();
        private string happysmiley = ConfigurationManager.AppSettings["smiley_happy"].ToString();
        private string indifsmiley = ConfigurationManager.AppSettings["smiley_indef"].ToString();
        private string sadsmiley = ConfigurationManager.AppSettings["smiley_sad"].ToString();
        //private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        ReportParameter[] rpt; CultureInfo culture = new CultureInfo("en-GB");
        AppUser usr = new AppUser(); DBAccess db = new DBAccess(); int orgId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
                int hpSmly=int.Parse(happysmiley);
                int indif=int.Parse(indifsmiley);
                int sadsmly=int.Parse(sadsmiley);
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
                    if (User.IsInRole(orgAdminRole)|| User.IsInRole(RgMgrRole)||User.IsInRole(AreaMgrRole)||User.IsInRole(BrchMgrRole))
                    {
                        Utility.BindRegionList(ddlRegion,orgId);
                        Utility.BindAreaList(ddlArea,orgId);
                        Utility.BindBranchList(ddlBranch,orgId);
                    }
                    if (User.IsInRole(AdminRole))
                    {
                        Utility.BindRegionList(ddlRegion);
                        Utility.BindAreaList(ddlArea);
                        Utility.BindBranchList(ddlBranch);
                    }
                    lbrange.Text = "Report From: " + sDAte.ToLongDateString() + " To: " + eDate.ToLongDateString();
                }
            }
            catch (Exception ex)
            {
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
            catch(Exception ex)
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
                int hpSmly = int.Parse(happysmiley);
                int indif = int.Parse(indifsmiley);
                int sadsmly = int.Parse(sadsmiley);
                usr = (AppUser)Session["user"];
                orgId = (int)Session["orgid"];
                rpt = new ReportParameter[6];
                rpt[0] = new ReportParameter("fromDate", sDAte.ToShortDateString());
                rpt[1] = new ReportParameter("toDate", eDate.ToShortDateString());
                rpt[2] = new ReportParameter("branchId", "1");
                rpt[3] = new ReportParameter("areaId", "1");
                rpt[4] = new ReportParameter("regionId", "1");
                rpt[5] = new ReportParameter("orgId", "1");
                db.AddParameter("@fromDate", sDAte);
                db.AddParameter("@toDate", eDate);
                db.AddParameter("@orgId", orgId);
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
                    }
                    if (ddlArea.SelectedValue != "")
                    {
                        areaId = int.Parse(ddlArea.SelectedValue);
                        db.AddParameter("@AreaID", areaId);
                    }
                    if (ddlBranch.SelectedValue != "")
                    {
                        branchId = int.Parse(ddlBranch.SelectedValue);
                        db.AddParameter("@branchID", branchId);
                    }
                    lbHappy.Text = FeedbackBLL.GetFeedback(sDAte, eDate,branchId,areaId,regionId,orgId).Where(h => h.SmileyAction.SmileyType == hpSmly).Count().ToString() + " Awesome!";
                    lbIndif.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, areaId, regionId,orgId).Where(h => h.SmileyAction.SmileyType == indif).Count().ToString() + " Not So Great!!"; ;
                    lbSad.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, areaId, regionId,orgId).Where(h => h.SmileyAction.SmileyType == sadsmly).Count().ToString() + " Bad!!!";


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
                    }
                    if (ddlBranch.SelectedValue != "")
                    {
                        branchId = int.Parse(ddlBranch.SelectedValue);
                        db.AddParameter("@branchID", branchId);
                    }
                    lbHappy.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, areaId, usr.Branch.Area.RegionalID.Value, orgId).Where(h => h.SmileyAction.SmileyType == hpSmly).Count().ToString() + " Awesome!";
                    lbIndif.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, areaId, usr.Branch.Area.RegionalID.Value, orgId).Where(h => h.SmileyAction.SmileyType == indif).Count().ToString() + " Not So Great!!"; ;
                    lbSad.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, areaId, usr.Branch.Area.RegionalID.Value, orgId).Where(h => h.SmileyAction.SmileyType == sadsmly).Count().ToString() + " Bad!!!"; ;

                    db.AddParameter("@RegionID", usr.Branch.Area.RegionalID);
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
                    }
                    lbHappy.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, usr.Branch.AreaID.Value, usr.Branch.Area.RegionalID.Value, orgId).Where(h => h.SmileyAction.SmileyType == hpSmly).Count().ToString() + " Awesome!";
                    lbIndif.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, usr.Branch.AreaID.Value, usr.Branch.Area.RegionalID.Value, orgId).Where(h => h.SmileyAction.SmileyType == indif).Count().ToString() + " Not So Great!!"; ;
                    lbSad.Text = FeedbackBLL.GetFeedback(sDAte, eDate, branchId, usr.Branch.AreaID.Value, usr.Branch.Area.RegionalID.Value, orgId).Where(h => h.SmileyAction.SmileyType == sadsmly).Count().ToString() + " Bad!!!";

                    db.AddParameter("@AreaID", usr.Branch.AreaID);
                    db.AddParameter("@RegionID", usr.Branch.Area.RegionalID);

                }
                if (User.IsInRole(BrchMgrRole))
                {
                    lbview.Text = usr.Branch.Name + " Branch Customer Feedback View";
                    lbHappy.Text = FeedbackBLL.GetFeedback(sDAte, eDate).Where(b => b.BranchID == usr.BranchID).Where(h => h.SmileyAction.SmileyType == hpSmly).Count().ToString() + " Awesome!";
                    lbIndif.Text = FeedbackBLL.GetFeedback(sDAte, eDate).Where(b => b.BranchID == usr.BranchID).Where(h => h.SmileyAction.SmileyType == indif).Count().ToString() + " Not So Great!!"; ;
                    lbSad.Text = FeedbackBLL.GetFeedback(sDAte, eDate).Where(b => b.BranchID == usr.BranchID).Where(h => h.SmileyAction.SmileyType == sadsmly).Count().ToString() + " Bad!!!";

                    db.AddParameter("@branchID", usr.BranchID);
                }

                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet("SmileyActionByBranch");

                ReportDataSource datasource = new
                  ReportDataSource("dsFeedback",
                  ds.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(rpt);
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    // lbmsg.Text = "Sorry, no products under this category!";
                }
                ReportViewer1.LocalReport.Refresh();
                lbrange.Text = "Report From: " + sDAte.ToLongDateString() + " To: " + eDate.ToLongDateString();
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}