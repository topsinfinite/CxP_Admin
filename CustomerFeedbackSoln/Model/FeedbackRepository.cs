using CustomerFeedbackSoln.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerFeedbackSoln.Model
{
    public class FeedbackRepository
    {
        private static FeedBackDBEntities _feedbackDb;
        private static FeedBackDBEntities FeedbackDb
        {
            get { return _feedbackDb ?? (_feedbackDb = new FeedBackDBEntities()); }
        }
        public static int GetOrgIdByDevice(string deviceId)
        {
            try
            {
                int orgId = 0;
                using (var db = new FeedBackDBEntities())
                {
                    var query = db.DeviceTbls.Include("Branch.Area.Region").Where(d => d.DeviceID == deviceId && d.isActive==true).Select(b => b.Branch.Area.Region.OrganisationID).FirstOrDefault();
                    if (query.HasValue)
                    {
                        orgId = query.Value;
                    }
                    return orgId;
                }
            }
            catch
            {
                return 0;
            }
        }
        public static OrgSettingDTO GetOrgSetting(int orgId)
        {
            var query = from org in FeedbackDb.Organisations
                        where org.isActive == true && org.ID == orgId
                        select new OrgSettingDTO
                        {
                            ID = org.ID,
                            HomeLabel = org.HomeLabel,
                            Logo = org.Logo,
                            Background = org.Background,
                            isActive = org.isActive
                        };
            return query.FirstOrDefault();
        }
        public static LabelSettingDTO GetLabelSetting(int orgId)
        {
            var query = from org in FeedbackDb.LabelSettings
                        where org.OrgId == orgId
                        select new LabelSettingDTO
                        {
                            ID = org.ID,
                            HomeLabel = org.HomeLabel,
                            CategoryLabelAwesome=org.CategoryLabelAwesome,
                            CategoryLabelBad=org.CategoryLabelBad,
                            ServiceLabelAwesome=org.ServiceLabelAwesome,
                            ServiceLabelBad=org.ServiceLabelBad,
                            ServiceLabelIndifferent=org.ServiceLabelIndifferent,
                            StaffLabelAwesome=org.StaffLabelAwesome,
                            StaffLabelBad=org.StaffLabelBad
                            
                        };
            return query.FirstOrDefault();
        }
        public static IEnumerable<SmileyActionDTO> GetSmileyAction(int orgId)
        {
            var query = from sm in FeedbackDb.SmileyActions.Include("Organisation")
                        where sm.isActive == true && sm.OrganisationId==orgId
                        select new SmileyActionDTO
                        {
                            ID = sm.ID,
                            Name = sm.Name,
                            SmileyImage = sm.SmileyImage,
                            isActive = sm.isActive,
                            SmileyType = sm.SmileyType
                        };

            return query.ToList();
        }
        public static IEnumerable<CategoryDTO> GetSmileyCategory(int orgId)
        {
            var query = from cat in FeedbackDb.Categories.Include("Organisation")
                        where cat.isActive == true && cat.OrganisationId == orgId
                        select new CategoryDTO
                        {
                            ID = cat.ID,
                            Name = cat.Name,
                            CategoryIcon=cat.CategoryIcon,
                            CategoryType=cat.CategoryType,
                            isActive = cat.isActive
                        };
            return query.OrderBy(c => c.Name).ToList();
        }
        public static IEnumerable<ServicesDTO> GetServices(int orgId)
        {
            var query = from svr in FeedbackDb.Services.Include("Organisation")
                        where svr.isActive == true && svr.OrganisationId == orgId
                        select new ServicesDTO
                        {
                            ID = svr.ID,
                            Name = svr.Name,
                            ServiceIcon=svr.ServiceIcon,
                            ServiceCatId=svr.ServiceCatId.HasValue?svr.ServiceCatId.Value:0,
                            isActive = svr.isActive
                        };
            return query.OrderBy(s => s.Name).ToList();
        }

        public static void AddFeedback(FeedbackDTO fbobject)
        {
            try
            {
                Feedback fb = new Feedback();

                fb.SmileyActionID = fbobject.SmileyActionID;
                fb.BranchID = fbobject.BranchID;
                if (fbobject.CategoryID != null)
                    fb.CategoryID = fbobject.CategoryID;
                if (fbobject.ServiceID != null)
                    fb.ServiceID = fbobject.ServiceID;
                fb.DateAdded = fbobject.DateAdded;


                FeedbackDb.Feedbacks.Add(fb);
                FeedbackDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void AddFeedBackWithComment(FeedbackDTO fbobject, Customer c)
        {
            try
            {
                Feedback fb = new Feedback();

                fb.SmileyActionID = fbobject.SmileyActionID;
                fb.BranchID = fbobject.BranchID;
                if (fbobject.CategoryID != null)
                    fb.CategoryID = fbobject.CategoryID;
                if (fbobject.ServiceID != null)
                    fb.ServiceID = fbobject.ServiceID;
                fb.DateAdded = fbobject.DateAdded;
                fb.Comment = fbobject.Comment;
                Customer cust = new Customer();
                cust = c;
                cust.Feedbacks.Add(fb);
                using (FeedBackDBEntities entities = new FeedBackDBEntities())
                {
                    entities.Customers.Add(cust);
                    entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}