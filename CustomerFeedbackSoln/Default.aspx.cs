using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;

namespace CustomerFeedbackSoln
{
    public partial class _Default : Page
    {
        private string happysmiley = ConfigurationManager.AppSettings["smiley_happy"].ToString();
        private string indifsmiley = ConfigurationManager.AppSettings["smiley_indef"].ToString();
        private string sadsmiley = ConfigurationManager.AppSettings["smiley_sad"].ToString();
        private string catstaff = ConfigurationManager.AppSettings["cat_staff"].ToString();
        private string catservice = ConfigurationManager.AppSettings["cat_service"].ToString();
        private CustomerFeedbackSoln.DAL.Feedback fb;
        private int counter = 0;
        private  System.Timers.Timer TimerCounter;
        //private  System.Timers.Timer TimerCheck;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                success.Visible = false;
                lbError.Visible = false;
                if (Request.QueryString["DeviceId"] == null)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        Response.Redirect("Login.aspx", false);
                        return;
                    }
                    if (Session["user"] == null)
                    {
                        Response.Redirect("Login.aspx?session=1", false);
                        return;
                    }
                }
                else
                {
                    string dvcID=Request.QueryString["DeviceId"].ToString();
                    if (Cache["Device"] == null)
                    {
                        Cache.Insert("DeviceDtl", LookupBLL.GetByDeviceID(dvcID));
                    }
                }
                if (!IsPostBack)
                {
                    Cache.Remove("smiley");
                    Cache.Remove("category");
                    Cache.Remove("service");
                    BindGrid();
                    BindCatGrid();
                    BindServiceGrid();
                   


                }
            }
            catch (Exception ex)
            {
                //error.Visible = true;
                //error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong>An unexpected error occurred. kindly try again!!!";
                Utility.WriteError("Error: " + ex.Message);
                return;
            }
        }
        private void OnTimedCounterEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                if (counter >= 40)//60sec
                {
                    TimerCounter.Enabled = false;
                    hidcounter.Value = "0";
                    SubmitFeedback();
                   // Response.Redirect("Default.aspx",false);
                    BindGrid();
                    ScriptManager.RegisterStartupScript(this, GetType(), "ReloadPage", "ReloadPage();", true);   
                    counter = 0;
                    mpeform.Hide();
                    mpehappy.Hide();
                    mpeService.Hide();
                    mpeStaff.Hide();
                  //  HttpContext.Current.Response.Redirect("Default.aspx", false);
                    return;

                }
                counter = counter + 1;

               
            }
            catch
            {
            }
        }
        private void OnTimerCheckEvent(object source, ElapsedEventArgs e)
        {
            int count = int.Parse(hidcounter.Value);
            if (count >= 30)
            {
                SubmitFeedback();
                TimerCounter.Enabled = false;
                hidcounter.Value = "0";
                counter = 0;
                mpeform.Hide();
                mpehappy.Hide();
                mpeService.Hide();
                mpeStaff.Hide();
            }
        }
        private void BindGrid()
        {
            if (Cache["smiley"] == null)
            {
                Cache.Insert("smiley", LookupBLL.GetSmileyActionListLookup());
                lstMain.DataSource = (IEnumerable<SmileyAction>)Cache["smiley"];
                lstMain.DataBind();
            }
            else
            {
                lstMain.DataSource = (IEnumerable<SmileyAction>)Cache["smiley"];
                lstMain.DataBind();
            }
           
        }
        private void BindCatGrid()
        {
            //Cache.Insert("category", LookupBLL.GetCategoryListLookup());
            if (Cache["category"] == null)
            {
                Cache.Insert("category", LookupBLL.GetCategoryListLookup());
                lstCat.DataSource = (IEnumerable<Category>)Cache["category"];
                lstCat.DataBind();
            }
            else
            {
                lstCat.DataSource = (IEnumerable<Category>)Cache["category"];
                lstCat.DataBind();
            }
        }
        private void BindServiceGrid()
        {
            if (Cache["service"] == null)
            {
                Cache.Insert("service", LookupBLL.GetServiceListLookup());
                lstService.DataSource = (IEnumerable<Service>)Cache["service"];
                lstService.DataBind();
            }
            else
            {
                lstService.DataSource = (IEnumerable<Service>)Cache["service"];
                lstService.DataBind();
            }
        }
        protected void lstMain_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                hidcounter.Value = "0"; counter = 0;
                TimerCounter = new System.Timers.Timer(6000);
                // Hook up the Elapsed event for the timer.
                TimerCounter.Elapsed += new ElapsedEventHandler(OnTimedCounterEvent);
                // Set the Interval to 1 secs 
                TimerCounter.Interval = 6000;
                TimerCounter.Enabled = true;

                fb = new Feedback(); Session["fb"] = null;
                if (Request.QueryString["DeviceId"] == null)//check if request is coming from device
                {
                    AppUser usr = new AppUser();
                    usr = (AppUser)Session["user"];
                    fb.BranchID = usr.BranchID;
                }
                else
                {
                    if (Cache["Device"] != null)
                    {
                        DeviceTbl dvc = (DeviceTbl)Cache["Device"];
                        fb.BranchID = dvc.BranchID;
                    }
                    else
                    {
                        string dvcID = Request.QueryString["DeviceId"].ToString();
                        DeviceTbl dvc = LookupBLL.GetByDeviceID(dvcID);
                        Cache.Insert("DeviceDtl", dvc);
                        fb.BranchID = dvc.BranchID;
                    }
                }
                fb.DateAdded = DateTime.Now;
                if (e.CommandArgument.ToString() == happysmiley)
                {
                    //smileyicon1.ImageUrl = "~/Images/smiley-icon.gif";
                   fb.SmileyActionID = int.Parse(e.CommandArgument.ToString());
                    hidHappy.Value = "1";
                    hidSad.Value = "0";
                    dvcategoryCont.InnerHtml = "<img src='Images/smiley-icon.gif' />Nice!<br />What did we do to make you happy today?";
                    BindCatGrid();
                    mpehappy.Show();
                }
                if (e.CommandArgument.ToString() == indifsmiley)
                {
                   // smileyicon1.ImageUrl = "~/Images/indif-icon.gif";
                    fb.SmileyActionID = int.Parse(e.CommandArgument.ToString());
                    hidIndif.Value = "1";
                    dvserviceCont.InnerHtml = "<img src='Images/indif-icon.gif' /> Oh, goodness! What can we do better?";
                    BindServiceGrid();
                    mpeService.Show();
                }
                if (e.CommandArgument.ToString() == sadsmiley)
                {
                    //smileyicon1.ImageUrl = "~/Images/sad-icon.gif";
                    fb.SmileyActionID = int.Parse(e.CommandArgument.ToString());
                    hidHappy.Value = "0";
                    hidSad.Value = "1";
                    dvcategoryCont.InnerHtml = "<img src='Images/sad-icon.gif' />Oh No!<br/> What did we do wrong?";
                    BindCatGrid();
                    mpehappy.Show();
                }
                Session["fb"] = fb;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void lstCat_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (Session["fb"] != null)
                {
                    fb = (Feedback)Session["fb"];
                }
                else
                {
                    return;
                }
                if (e.CommandArgument.ToString() == catstaff)
                {
                    hidstaff.Value = "1";
                    fb.CategoryID = int.Parse(e.CommandArgument.ToString());
                    if (hidSad.Value == "1")
                    {
                        dvStaffCont.InnerHtml = "<img src='Images/sad-icon.gif' /> Which of our staff are you not happy with ?";
                    }
                    if (hidHappy.Value == "1")
                    {
                        dvStaffCont.InnerHtml = "<img src='Images/smiley-icon.gif' /> Which of our staff are you happy with ?";
                    }
                    //BindServiceGrid();
                    mpehappy.Hide();
                    mpeService.Show();
                    //mpeStaff.Show();
                }
                if (e.CommandArgument.ToString() == catservice)
                {
                    fb.CategoryID = int.Parse(e.CommandArgument.ToString());
                    if (hidSad.Value == "1")
                    {
                        dvserviceCont.InnerHtml = "<img src='Images/sad-icon.gif' /> Which service needs improvement?";
                    }
                    if (hidHappy.Value == "1")
                    {
                        dvserviceCont.InnerHtml = "<img src='Images/smiley-icon.gif' /> Which of our services blew you away?";
                    }
                    Session["fb"] = fb;
                    BindServiceGrid();
                    mpehappy.Hide();
                    mpeService.Show();
                }
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void lstServices_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (Session["fb"] != null)
                {
                    fb = (Feedback)Session["fb"];
                }
                else
                {
                    return;
                }
                fb.ServiceID = int.Parse(e.CommandArgument.ToString());
                if (hidSad.Value == "1")
                {
                    dvComment.InnerHtml = "<img src='Images/sad-icon.gif' /> Mind Filling Our Feedback Form?";
                }
                if (hidHappy.Value == "1")
                {
                    dvComment.InnerHtml = "<img src='Images/smiley-icon.gif' /> Mind Filling Our Feedback Form? ";
                }
                if (hidIndif.Value == "1")
                {
                    dvComment.InnerHtml = "<img src='Images/indif-icon.gif' /> Mind Filling Our Feedback Form? ";
                }
                Session["fb"] = fb;
                mpeService.Hide();

                mpeform.Show();
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void btn_happy_Click(object sender, EventArgs e)
        {
            try
            {
                SubmitFeedback();
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }
        protected void btnAppr_Click(object sender, EventArgs e)
        {
        }

        protected void SubmitFeedback()
        {
            if (this.Session["fb"] != null)
            {
                this.fb = (Feedback)this.Session["fb"];
                FeedbackBLL.AddFeedback(this.fb);
                this.Session["fb"] = null;
                this.success.Visible = true;
                this.mpeAlert.Show();
            }

        }
        protected void btn_Service_Click(object sender, EventArgs e)
        {
            try
            {
                SubmitFeedback();
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }

        protected void lnkstaff_Click(object sender, EventArgs e)
        {
            try
            {
                hidstaff.Value = "1";
            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_CommentExit_Click(object sender, EventArgs e)
        {
            try
            {
                SubmitFeedback();
            }catch(Exception ex)
            {
            }
        }
        protected void btn_Comment_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Session["fb"] != null)
                {
                    this.fb = (Feedback)this.Session["fb"];
                }
                else
                {
                    return;
                }
                Customer entity = new Customer();
                //if (!string.IsNullOrEmpty(this.txtAcct.Value))
                //{
                //    entity.AccountNumber = this.txtAcct.Value;
                //}
                if (!string.IsNullOrEmpty(this.txtNameId.Value))
                {
                    entity.CustomerName = this.txtNameId.Value;
                }
                else
                {
                    this.lbError.Visible = true;
                    this.lbError.Text = "Field(s) mark with asterix(*) are required";
                    this.mpeform.Show();
                    return;
                }
                if (!string.IsNullOrEmpty(this.txtphoneId.Value))
                {
                    entity.MobileNo = this.txtphoneId.Value;
                }
                if (!string.IsNullOrEmpty(this.emailId.Value))
                {
                    entity.Email = this.emailId.Value;
                }
                if (!string.IsNullOrEmpty(this.txtCommentArea.Value))
                {
                    this.fb.Comment = this.txtCommentArea.Value;
                }
                //if (this.ddlGender.SelectedValue != "0")
                //{
                //    entity.Gender = this.ddlGender.SelectedItem.Text;
                //}
                entity.Feedbacks.Add(this.fb);
                using (FeedBackDBEntities entities = new FeedBackDBEntities())
                {
                    entities.Customers.Add(entity);
                    entities.SaveChanges();
                   // this.txtAcct.Value = "";
                    this.txtCommentArea.Value = "";
                    this.emailId.Value = "";
                    this.txtNameId.Value = "";
                    this.txtphoneId.Value = "";
                   // this.ddlGender.SelectedValue = "0";
                    this.success.Visible = true;
                    this.mpeAlert.Show();
                }
            }
            catch (Exception exception)
            {
                Utility.WriteError("Error: " + exception.Message);
            }

        }
        protected void lnkDept_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidSad.Value == "1")
                {
                    dvserviceCont.InnerHtml = "<img src='Images/sad-icon.gif' /> Which of department does he or she work?";
                }
                if (hidHappy.Value == "1")
                {
                    dvserviceCont.InnerHtml = "<img src='Images/smiley-icon.gif' /> Which of department does he or she work?";
                }
                
                BindServiceGrid();
                mpehappy.Hide();
                mpeService.Show();
            }catch(Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message);
            }
        }
    }
}