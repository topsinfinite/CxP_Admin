using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerFeedbackSoln.DAL;
namespace CustomerFeedbackSoln.BLL
{
   
    public class FeedbackBLL
    {
        public static bool AddFeedback(DAL.Feedback fb)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.Feedbacks.Add(fb);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Feedback> GetFeedback(DateTime st, DateTime et,int branchId=0,int areaID=0,int regionId=0, int orgId=0)
        {
            try
            {
                var today = et.AddDays(1);
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var fb = from f in db.Feedbacks.Include("SmileyAction").Include("Branch").Include("Branch.Area").Include("Branch.Area.Region")
                             where f.DateAdded.Value>=st.Date && f.DateAdded.Value<today
                             select f;
                        if (branchId != 0)
                        {
                            fb = fb.Where(b => b.BranchID == branchId);
                        }
                        if(areaID!=0)
                        {
                           fb = fb.Where(b => b.Branch.AreaID == areaID);
                        }
                        if (regionId != 0)
                        {
                            fb = fb.Where(b => b.Branch.Area.RegionalID == regionId);
                        }
                        if (orgId != 0)
                        {
                            fb = fb.Where(b => b.Branch.Area.Region.OrganisationID == orgId);
                        }

                    return fb.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}