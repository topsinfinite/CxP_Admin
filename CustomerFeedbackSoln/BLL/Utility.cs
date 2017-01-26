using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Configuration; 
using System.IO;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using CustomerFeedbackSoln.DAL;
using System.Web.UI.HtmlControls;
//using System.Data.OracleClient;
namespace CustomerFeedbackSoln.BLL
{
     
    public class RequestStatus
    {
        public string DataValue { get; set; }
        public string DataField { get; set; }
    }
    public class SmileyTypeEnum
    {
        public string DataValue { get; set; }
        public string DataField { get; set; }
    }
    public class CategoryTypeEnum
    {
        public string DataValue { get; set; }
        public string DataField { get; set; }
    }
    public class IconTypeEnum
    {
        public string DataValue { get; set; }
        public string DataField { get; set; }
    }
   public class Utility
    {
       //private static string DeptApprRoleName = ConfigurationManager.AppSettings["DeptApprverRole"].ToString();
       public enum FleetRequestStatus
       {
           Pending_Supervisor_Approval=1,
           Pending_FleetManager_Approval=2,
           Pending_Vehicle_Assignment=3,
           Enroute=4,
           Completed=5,
           OnHold=6,
           Declined=7,
           Cancelled=8
       }
       
       public enum ServiceType
       {
           OurService = 2,
           OurStaff = 1
       }
       public enum SmileyType
       {
           Good = 1,
           Indifferent = 2,
           Bad=3
       }
       public enum IconType
       {
           Smiley = 1,
           Service = 2,
           Category = 3,
           EventMetric = 4
       }
       public static void BindStateList(DropDownList ddlList)
       {
           ddlList.DataValueField = "ID";
           ddlList.DataTextField = "Name";
           ddlList.DataSource = LookupBLL.GetStateList();
           ddlList.DataBind();
       }
       public static void BindIconTypeList(DropDownList ddlList)
       {
           ddlList.DataValueField = "DataValue";
           ddlList.DataTextField = "DataField";
           ddlList.DataSource = Utility.IconTypeList();
           ddlList.DataBind();
       }
       public static void BindSmileyTypeList(DropDownList ddlList)
       {
           ddlList.DataValueField = "DataValue";
           ddlList.DataTextField = "DataField";
           ddlList.DataSource = Utility.SmileyTypeList();
           ddlList.DataBind();
       }
       public static void BindCategoryTypeList(DropDownList ddlList)
       {
           ddlList.DataValueField = "DataValue";
           ddlList.DataTextField = "DataField";
           ddlList.DataSource = Utility.CategoryTypeList();
           ddlList.DataBind();
       }
       public static void BindOrgList(DropDownList ddlList)
       {
           ddlList.DataValueField = "ID";
           ddlList.DataTextField = "OrganisationName";
           ddlList.DataSource = LookupBLL.GetOrganisationListLookup();
           ddlList.DataBind();
       }
       public static void BindRegionList(DropDownList ddlList,int orgId=0)
       {
           ddlList.DataValueField = "ID";
           ddlList.DataTextField = "Name";
           if (orgId == 0)
           {
               ddlList.DataSource = BranchBLL.GetRegionListLookup();
               ddlList.DataBind();
           }
           else
           {
               ddlList.DataSource = BranchBLL.GetRegionListLookup(orgId);
               ddlList.DataBind();
           }
       }
       public static void BindAreaList(DropDownList ddlList,int orgId=0)
       {
           ddlList.DataValueField = "ID";
           ddlList.DataTextField = "Name";
           if (orgId == 0)
           {
               ddlList.DataSource = BranchBLL.GetAreaListLookup();
               ddlList.DataBind();
           }
           else
           {
               ddlList.DataSource = BranchBLL.GetAreaListLookup(orgId);
               ddlList.DataBind();
           }
       }
       public static void BindBranchList(DropDownList ddlList, int orgId = 0)
       {
           ddlList.DataValueField = "ID";
           ddlList.DataTextField = "Name";
           if (orgId == 0)
           {
               ddlList.DataSource = BranchBLL.GetBranchListLookup();
               ddlList.DataBind();
           }
           else
           {
               ddlList.DataSource = BranchBLL.GetBranchListLookup(orgId);
               ddlList.DataBind();
           }
       }
       public static void BindUserList(DropDownList ddlList,int orgId=0)
       {
           ddlList.DataValueField = "ID";
           ddlList.DataTextField = "FullName";
           if (orgId == 0)
           {
               ddlList.DataSource = UserBLL.GetUserListLookup();
               ddlList.DataBind();
           }
           else
           {
               ddlList.DataSource = UserBLL.GetUserListLookup(orgId);
               ddlList.DataBind();
           }
       }
       public static string GetIconType(object o)
       {
           try
           {
               string type = "";
               if (o.ToString() == "1") { type = "Smiley"; }
               if (o.ToString() == "2") { type = "Service"; }
               if (o.ToString() == "3") { type = "Category"; }
               if (o.ToString() == "4") { type = "EventMetric"; }
               return type;
           }
           catch
           {
               return string.Empty;
           }
       }
       public static string GetSmileyType(object o)
       {
           try
           {
               string type = "";
               if (o.ToString() == "1") { type = "Good"; }
               if (o.ToString() == "2") { type = "Indifferent"; }
               if (o.ToString() == "3") { type = "Bad"; }
               return type;
           }
           catch
           {
               return string.Empty;
           }
       }
       public static string GetCategoryType(object o)
       {
           try
           {
               string type = "";
               if (o.ToString() == "1") { type = "Staff"; }
               if (o.ToString() == "2") { type = "Service"; }
               
               return type;
           }
           catch
           {
               return string.Empty;
           }
       }
       public static string GetRequestStatus(object o)
       {
           try
           {
               string status = "";
               if (o != null)
               {
                   if (o.ToString() == "1")
                       status = "Pending Supervisor Approval";
                   if (o.ToString() == "2")
                       status = "Pending FleetManager Approval";
                   if (o.ToString() == "3")
                       status = "Pending Vehicle Assignment";
                   if (o.ToString() == "4")
                       status = "Enroute";
                   if (o.ToString() == "5")
                       status = "Completed";
                   if (o.ToString() == "6")
                       status = "OnHold";
                   if (o.ToString() == "7")
                       status = "Declined";
                   if (o.ToString() == "8")
                       status = "Cancelled";
               }
               return status;
           }
           catch
           {
               return "";
           }
       }
       public static List<IconTypeEnum> IconTypeList()
       {
           List<IconTypeEnum> lst = new List<IconTypeEnum>()
           {
               new IconTypeEnum{DataField="Smiley",DataValue="1"},
               new IconTypeEnum{DataField="Service", DataValue="2"},
               new IconTypeEnum{DataField="Category",DataValue="3"},
              new IconTypeEnum{DataField="Event Metric",DataValue="4"}
           };
           return lst;
       }
       public static List<SmileyTypeEnum> SmileyTypeList()
       {
           List<SmileyTypeEnum> lst = new List<SmileyTypeEnum>()
           {
               new SmileyTypeEnum{DataField="Good",DataValue="1"},
               new SmileyTypeEnum{DataField="Indifferent", DataValue="2"},
               new SmileyTypeEnum{DataField="Bad",DataValue="3"}
           };
           return lst;
       }
       public static List<CategoryTypeEnum> CategoryTypeList()
       {
           List<CategoryTypeEnum> lst = new List<CategoryTypeEnum>()
           {
               new CategoryTypeEnum{DataField="Staff",DataValue="1"},
               new CategoryTypeEnum{DataField="Service", DataValue="2"},
               
           };
           return lst;
       }
       public static List<RequestStatus> StatusList()
       {
           List<RequestStatus> range = new List<RequestStatus>
               {
                   new RequestStatus{DataField="Pending FleetManager Approval",DataValue="2"},
                   new RequestStatus{DataField="Pending Vehicle Assignment",DataValue="3"},
                   new RequestStatus{DataField="Enroute",DataValue="4"},
                   new RequestStatus{DataField="Completed",DataValue="5"},
                   new RequestStatus{DataField="OnHold",DataValue="6"},
                   new RequestStatus{DataField="Declined",DataValue="7"},
                   new RequestStatus{DataField="Cancelled",DataValue="8"}

               };
           return range;
       }
       public static string getIconName(ListView lst)
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
       public static string ConnectionStr()//all remember to set this to point to live Finacle
       {
           //string connection = ConfigurationManager.ConnectionStrings["finLiveConnectionString"].ConnectionString;
           string connection = ConfigurationManager.ConnectionStrings["OraConnection"].ConnectionString;
           return connection;
       }

    
       public static decimal GetTripDuration(int stHr, int stMin, int rtHr, int rtMin)
       {
           try
           {
               decimal rtTotalSecs = 0; decimal stTotalSecs = 0; decimal totalDurationInSecs = 0;
               rtTotalSecs = (rtHr * 60 * 60) + (rtMin * 60);
               rtTotalSecs = rtTotalSecs / 86400;
               stTotalSecs = (stHr * 60 * 60) + (stMin * 60);
               stTotalSecs = stTotalSecs / 86400;
               totalDurationInSecs = rtTotalSecs - stTotalSecs;
               return totalDurationInSecs * 24;//convert to hours
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    
       public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
       {
           System.Drawing.Bitmap bmpOut = null;
           try
           {

               Bitmap loBMP = new Bitmap(lcFilename);
               ImageFormat loFormat = loBMP.RawFormat;
               decimal lnRatio;
               int lnNewWidth = 0;
               int lnNewHeight = 0;

               //*** If the image is smaller than a thumbnail just return it

               if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)

                   return loBMP;

               if (loBMP.Width > loBMP.Height)
               {

                   lnRatio = (decimal)lnWidth / loBMP.Width;
                   lnNewWidth = lnWidth;
                   decimal lnTemp = loBMP.Height * lnRatio;
                   lnNewHeight = (int)lnTemp;

               }
               else
               {
                   lnRatio = (decimal)lnHeight / loBMP.Height;
                   lnNewHeight = lnHeight;
                   decimal lnTemp = loBMP.Width * lnRatio;
                   lnNewWidth = (int)lnTemp;

               }

               // System.Drawing.Image imgOut =
               //      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
               //                              null,IntPtr.Zero);
               // *** This code creates cleaner (though bigger) thumbnails and properly
               // *** and handles GIF files better by generating a white background for
               // *** transparent images (as opposed to black)

               bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
               Graphics g = Graphics.FromImage(bmpOut);
               g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
               g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
               g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
               loBMP.Dispose();

           }

           catch
           {
               return null;
           }
           return bmpOut;

       }
       public static string UploadFile(FileUpload fp,string serverpath)
       {
           try
           {
               Boolean fileOK = false; string isFileUploaded = ""; int maxSize = 4097151;
              // String path = Server.MapPath("Upload/");
               String path = serverpath;
               if (fp.HasFile && fp.PostedFile.ContentLength <= maxSize)
               {
                   //if(fp.ID.Contains("fileUpload_PP"))
                   //{
                   //    hidFile_PP.Value=fp.FileName;
                   //}
                   String fileExtension =
                   System.IO.Path.GetExtension(fp.FileName).ToLower();
                   string[] allowedExtensions = ConfigurationManager.AppSettings["fileExt"].ToString().Split(new char[] { ',' });
                   for (int i = 0; i < allowedExtensions.Length; i++)
                   {
                       if (fileExtension.Trim() == allowedExtensions[i].Trim())
                       {
                           fileOK = true;
                       }
                   }
                   if (fileOK)
                   {
                       try
                       {
                           string filename = Path.GetFileNameWithoutExtension(fp.FileName);
                           filename = filename + DateTime.Now.ToString("ddMMyyhhmmss") + fileExtension;
                           filename = filename.Replace("'", "");
                           fp.PostedFile.SaveAs(path + filename);
                           isFileUploaded = filename;
                          // fp.PostedFile.SaveAs(path + fp.FileName);
                          // isFileUploaded = true;
                       }
                       catch 
                       {
                           isFileUploaded = string.Empty;
                       }
                   }
                   else
                   {
                       isFileUploaded = string.Empty;
                   }
                   return isFileUploaded;
               }
               else
               {
                   //error.Visible = true;
                   //error.InnerHtml = "<button type='button' class='close' data-dismiss='alert'>&times;</button> No File has been specified for upload Or File size is too large. File size must be less than 4MB. Kindly upload the supporting document and try again";
                   return string.Empty;
               }
           }
           catch (Exception ex)
           {
               //error.Visible = true;
               //error.InnerHtml = " <button type='button' class='close' data-dismiss='alert'>&times;</button>An error occured!! . Kindly review all the input and try again";
               return string.Empty;
           }
       }
       public static string GetUserIPAdress(HttpContext context)
       {
           string UserIPAddress = "";
           UserIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
           if (string.IsNullOrEmpty(UserIPAddress))
           {
               UserIPAddress = context.Request.ServerVariables["REMOTE_ADDR"];
           }

           return UserIPAddress;
       }
       public static string SendMail(string toList, string from, string ccList, string subject, string body)
       {
           MailMessage message = new MailMessage();
           message.Headers.Add("content-type", "text/html;");
           SmtpClient smtpClient = new SmtpClient();
           string msg = string.Empty;
           try
           {
               MailAddress fromAddress = new MailAddress(from);
               message.From = fromAddress;
               //ccList = "temitope.fatayo@amcon.com.ng";
               //toList = "temitope.fatayo@amcon.com.ng";
               message.To.Add(toList);
               if (ccList != null && ccList != string.Empty)
                   message.CC.Add(ccList);
               message.Subject = subject;
               message.IsBodyHtml = true;
               message.Body = body;
               smtpClient.Host = ConfigurationManager.AppSettings["smtpServer"].ToString();
               // smtpClient.Host = "lac-la1-s024.leadway.com.ng";
               // smtpClient.Port = 25;
               smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());
               smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
               smtpClient.UseDefaultCredentials = false;
               string exUser = ConfigurationManager.AppSettings["exUser"].ToString(); ;
               string exPwd = ConfigurationManager.AppSettings["exPwd"].ToString(); ;
               smtpClient.Credentials = new System.Net.NetworkCredential(exUser, exPwd);
               string requiredssl = ConfigurationManager.AppSettings["EnableSSL"].ToString();
               if (requiredssl == "1")
               {
                   smtpClient.EnableSsl = true;
               }
               else
               {
                   smtpClient.EnableSsl = false;
               }
               smtpClient.Send(message);
               msg = "Successful";
           }
           catch (SmtpException ex)
           {
               msg = ex.Message;
               return msg;
           }
           return msg;
       }
       public static void WriteError(string errorMessage)
       {
           try
           {
               string path = "~/ErrorLog/" + DateTime.Today.ToString("dd-MMM-yy") + ".txt";
               if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
               {
                   File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
               }
               using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
               {
                   w.WriteLine("\r\nLog Entry : ");
                   w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                   string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                                 ". Error Message:" + errorMessage;
                   w.WriteLine(err);
                   w.WriteLine("__________________________");
                   w.Flush();
                   w.Close();
               }
           }
           catch (Exception ex)
           {
               WriteError(ex.Message);
           }

       }

       public static void ExporttoExcel(DataTable table, GridView GridView_Result, string RptName)
       {
           HttpContext.Current.Response.Clear();
           HttpContext.Current.Response.ClearContent();
           HttpContext.Current.Response.ClearHeaders();
           HttpContext.Current.Response.Buffer = true;
           HttpContext.Current.Response.ContentType = "application/ms-excel";
           HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
           HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + RptName + ".xls");

           HttpContext.Current.Response.Charset = "utf-8";
           HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
           //sets font
           HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
           HttpContext.Current.Response.Write("<BR><BR><BR>");
           //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
           HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
             "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
             "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
           //am getting my grid's column headers

           //int columnscount = GridView_Result.Columns.Count;
           int columnscount = table.Columns.Count;

           for (int j = 0; j < columnscount; j++)
           {      //write in new column
               HttpContext.Current.Response.Write("<Td>");
               //Get column headers  and make it as bold in excel columns
               HttpContext.Current.Response.Write("<B>");
               //HttpContext.Current.Response.Write(GridView_Result.Columns[j].HeaderText.ToString());
               HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());
               HttpContext.Current.Response.Write("</B>");
               HttpContext.Current.Response.Write("</Td>");
           }
           HttpContext.Current.Response.Write("</TR>");
           foreach (DataRow row in table.Rows)
           {//write in new row
               HttpContext.Current.Response.Write("<TR>");
               for (int i = 0; i < table.Columns.Count; i++)
               {
                   HttpContext.Current.Response.Write("<Td>");
                   HttpContext.Current.Response.Write(row[i].ToString());
                   HttpContext.Current.Response.Write("</Td>");
               }

               HttpContext.Current.Response.Write("</TR>");
           }
           HttpContext.Current.Response.Write("</Table>");
           HttpContext.Current.Response.Write("</font>");
           HttpContext.Current.Response.Flush();
           HttpContext.Current.Response.End();
       }


   }
 
 }
    


