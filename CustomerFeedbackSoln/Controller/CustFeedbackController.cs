using CustomerFeedbackSoln.BLL;
using CustomerFeedbackSoln.DAL;
using CustomerFeedbackSoln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerFeedbackSoln.Controller
{
    public class CustFeedbackController : ApiController
    {
        // GET api/custfeedback
        //[HttpGet]
         //[System.Web.Http.ActionName("Get")]
        private int orgId;
        //public IEnumerable<SmileyActionDTO> Get(string Id)
        public HttpResponseMessage Get(string Id)
        {
            //string devId = id;
            orgId = FeedbackRepository.GetOrgIdByDevice(Id);
            if (orgId != 0)
            {
                IEnumerable<SmileyActionDTO>  smileyList = FeedbackRepository.GetSmileyAction(orgId);
                return Request.CreateResponse<IEnumerable<SmileyActionDTO>>(HttpStatusCode.OK, smileyList);  
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found. Check that your device has been profiled");  
            }
           
        }
        public HttpResponseMessage GetOrgSetting(string Id)
        {
            orgId = FeedbackRepository.GetOrgIdByDevice(Id);

            if (orgId != 0)
            {
                OrgSettingDTO orgset = FeedbackRepository.GetOrgSetting(orgId);
                return Request.CreateResponse<OrgSettingDTO>(HttpStatusCode.OK, orgset);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found. Check that your device has been profiled");
            }
        }
        public HttpResponseMessage GetLabelSetting(string Id)
        {
            orgId = FeedbackRepository.GetOrgIdByDevice(Id);

            if (orgId != 0)
            {
                LabelSettingDTO orgset = FeedbackRepository.GetLabelSetting(orgId);
                return Request.CreateResponse<LabelSettingDTO>(HttpStatusCode.OK, orgset);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found. Check that your device has been profiled");
            }
        }
        public HttpResponseMessage GetCategory(string Id)
        {
            orgId = FeedbackRepository.GetOrgIdByDevice(Id);
            
            if(orgId!=0)
            {
                IEnumerable<CategoryDTO> catList=FeedbackRepository.GetSmileyCategory(orgId);
                return Request.CreateResponse<IEnumerable<CategoryDTO>>(HttpStatusCode.OK,catList);

            }else{
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found. Check that your device has been profiled"); 
            }
        }
        public HttpResponseMessage  GetServices(string Id)
        {
            orgId = FeedbackRepository.GetOrgIdByDevice(Id);
            if (orgId != 0)
            {
                IEnumerable<ServicesDTO> svrList = FeedbackRepository.GetServices(orgId);
                return Request.CreateResponse<IEnumerable<ServicesDTO>>(HttpStatusCode.OK, svrList);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found. Check that your device has been profiled"); 
            }
        }

        public bool GetConnection(string rand)
        {
            if (rand != null && rand.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // POST api/custfeedback
        public HttpResponseMessage PostFeedback([FromBody]FeedbackDTO fb)
        {
            try
            {
               // string usr = User.Identity.Name;
                string dvcID = fb.DeviceID;
                //FeedbackDTO fbo = new FeedbackDTO();
                //fbo.SmileyActionID = fb.SmileyActionID;
                //if (fb.CategoryID != null)
                //    fbo.CategoryID = fb.CategoryID;
                //if (fb.ServiceID != null)
                //    fbo.ServiceID = fb.ServiceID;
                
                DeviceTbl dvc = LookupBLL.GetByDeviceID(dvcID.Trim());
                if (dvc != null)
                {
                    fb.BranchID = dvc.BranchID;

                    if (fb.CommentInclude == "1")//1-for commentincluded
                    {
                        Customer cust = new Customer();
                        cust.CustomerName = fb.FullName;
                        cust.Email = fb.Email;
                        cust.MobileNo = fb.PhoneNo;
                        FeedbackRepository.AddFeedBackWithComment(fb, cust);
                    }
                    if (fb.CommentInclude == "0")//0---no comment
                    {
                        FeedbackRepository.AddFeedback(fb);
                    }
                    var response = Request.CreateResponse(HttpStatusCode.Created, "Successful");
                    response.Headers.Location = new Uri(Request.RequestUri, string.Format("custfeedback/Get/", fb.ID));
                    return response;
                }
                else
                {
           
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Error OCcured");
                }
            }catch( Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message+ "Inner Exception"+ ex.InnerException.StackTrace+" Detail "+ ex.GetBaseException().Message);
                return  Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Error OCcured");
                //return new HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
        }

        public HttpResponseMessage PostFeedbackList([FromBody]List<FeedbackDTO> fbs)
        {
            try
            {
                if (fbs == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Error OCcured");
                }
                foreach (FeedbackDTO fb in fbs)
                {
                    string dvcID = fb.DeviceID;
                    //FeedbackDTO fbo = new FeedbackDTO();
                    //fbo.SmileyActionID = fb.SmileyActionID;
                    //if (fb.CategoryID != null)
                    //    fbo.CategoryID = fb.CategoryID;
                    //if (fb.ServiceID != null)
                    //    fbo.ServiceID = fb.ServiceID;

                    DeviceTbl dvc = LookupBLL.GetByDeviceID(dvcID.Trim());
                    if (dvc != null)
                    {
                        fb.BranchID = dvc.BranchID;

                        if (fb.CommentInclude == "1")//1-for commentincluded
                        {
                            Customer cust = new Customer();
                            cust.CustomerName = fb.FullName;
                            cust.Email = fb.Email;
                            cust.MobileNo = fb.PhoneNo;
                            FeedbackRepository.AddFeedBackWithComment(fb, cust);
                        }
                        if (fb.CommentInclude == "0")//0---no comment
                        {
                            FeedbackRepository.AddFeedback(fb);
                        }
                       
                    }
                    else
                    {

                        return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Error OCcured");
                    }
                }
                var response = Request.CreateResponse(HttpStatusCode.Created, "Successful");
                response.Headers.Location = new Uri(Request.RequestUri, string.Format("custfeedback/Get/"));
                return response;
            }
            catch (Exception ex)
            {
                Utility.WriteError("Error: " + ex.Message + "Inner Exception" + ex.InnerException.StackTrace + " Detail " + ex.GetBaseException().Message);
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Error OCcured");
                //return new HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
        }

        // PUT api/custfeedback/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/custfeedback/5
        public void Delete(int id)
        {
        }
    }
}