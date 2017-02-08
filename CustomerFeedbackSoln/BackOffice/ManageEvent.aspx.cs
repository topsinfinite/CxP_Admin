using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomerFeedbackSoln.BackOffice
{
    public partial class ManageEvent : System.Web.UI.Page
    {
        private string AdminRole = ConfigurationManager.AppSettings["adminRole"].ToString();
        private string OrgAdminRole = ConfigurationManager.AppSettings["orgAdminRole"].ToString();
        private int orgId; CultureInfo culture = new CultureInfo("en-GB");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                error.Visible = false; success.Visible = false;
                btnSubmit.Attributes.Add("onclick", " this.disabled = true; this.value='Processing'; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
                if (!IsPostBack)
                {
                    orgId = (int)Session["orgid"];
                    BindGrid(orgId);

                    Utility.BindOrgList(ddlOrg);
                    EventBLL.BindEventList(ddlEventfilter);
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
                //BindGrid();
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
            gvDefault.DataSource = EventBLL.GetEventList(orgId);
            gvDefault.DataBind();
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
                    Event b = null; bool rst = false; DateTime validdate;
                    b = EventBLL.GetEvent(Convert.ToInt32(hidkey.Value));
                    if (b != null)
                    {
                        b.Title = txtTitle.Text;
                        b.Note = txtNote.Text;
                        b.Question = txtQuestion.Text;
                        b.Code = txtEvtCode.Text.Trim();
                        b.OrganisationID = int.Parse(ddlOrg.SelectedValue);
                        if (chk.Checked)
                            b.isActive = true;
                        else
                            b.isActive = false;
                        if (!DateTime.TryParseExact(txtValidDate.Text, "dd/MM/yyyy", culture, DateTimeStyles.None, out validdate))
                        {
                            error.Visible = true;
                            error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong> Invalid date format. Acceptable formt(dd/MM/yyyy). Please try again";
                        }
                        else
                        {
                            b.ValidTill = validdate;
                        }
                        rst = EventBLL.Update(b);
                        if (rst != false)
                        {
                           
                            success.Visible = true;
                            success.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button> Record updated successfully!!.";
                            txtNote.Text = "";
                            txtTitle.Text = "";
                            txtQuestion.Text = "";
                            txtValidDate.Text = "";
                            //ddlOrg.ClearSelection();
                            chk.Checked = false;
                            btnSubmit.Text = "Add";
                            BindGrid();

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
                    bool eventExist;
                    Event b = new Event();
                    b.Title = txtTitle.Text;
                    b.Note = txtNote.Text;
                    b.Question = txtQuestion.Text;
                    b.Code = txtEvtCode.Text.Trim();

                    b.OrganisationID = int.Parse(ddlOrg.SelectedValue);
                    if (chk.Checked)
                        b.isActive = true;
                    else
                        b.isActive = false;
                    DateTime validdate;

                    if (!DateTime.TryParseExact(txtValidDate.Text, "dd/MM/yyyy", culture, DateTimeStyles.None, out validdate))
                    {
                        error.Visible = true;
                        error.InnerHtml = "<a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Error!</strong> Invalid date format. Acceptable formt(dd/MM/yyyy). Please try again";
                    }
                    else
                    {
                        b.ValidTill = validdate;
                    }
                    
                    eventExist = EventBLL.EventExist(txtTitle.Text);
                    if (eventExist)
                    {
                        error.Visible = true;
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Event title already exists!";
                        return;
                    }
                    else
                    {
                        result = EventBLL.AddEvent(b);
                        if (result)
                        {
                            BindGrid();
                            txtTitle.Text = "";
                            txtNote.Text = "";
                            txtQuestion.Text = "";
                            txtValidDate.Text = "";
                            ddlOrg.ClearSelection();
                            chk.Checked = false;
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
                txtEvtCode.Enabled = false;
                btnCheck.Visible = false;
                btnSubmit.Enabled = true;
                btnSubmit.Text = "Update";
                string key = gvDefault.SelectedDataKey.Value.ToString();

                Event evt = EventBLL.GetEvent(int.Parse(key));
                if (evt != null)
                {
                    hidkey.Value = evt.ID.ToString();
                    txtTitle.Text = evt.Title;
                    txtQuestion.Text = evt.Question;
                    txtNote.Text = evt.Note;
                    txtEvtCode.Text = evt.Code;
                    txtValidDate.Text = evt.ValidTill.HasValue ? evt.ValidTill.Value.ToString("dd/MM/yyyy") : "";
                    ddlOrg.SelectedValue = evt.OrganisationID.HasValue ? evt.OrganisationID.ToString() : "";
                    if (evt.isActive.HasValue && evt.isActive.Value)
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlEventfilter.SelectedValue != "")
                {
                    int srch = int.Parse(ddlEventfilter.SelectedValue);
                    gvDefault.DataSource = EventBLL.GetEventList(srch);
                    gvDefault.DataBind();
                }
            }
            catch
            {
            }
        }
        protected void btnClr_Click(object sender, EventArgs e)
        {
            ddlEventfilter.SelectedValue = "";
            BindGrid();
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                
                string code = txtEvtCode.Text;
                if (!string.IsNullOrEmpty(code))
                {
                    if (!EventBLL.CheckEventExist(code))
                    {
                        success.Visible = true;
                        success.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong> Event code validated. kindly proceed";
                        btnSubmit.Enabled = true;
                        return;
                    }
                    else
                    {
                        error.Visible = true;
                        error.InnerHtml = " <a href='#' class='close' data-dismiss='alert'>&times;</a><strong>Event code already exists. kindly use another code";
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
            }
        }
    }
}