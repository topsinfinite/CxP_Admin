using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerFeedbackSoln.DAL;
namespace CustomerFeedbackSoln.BLL
{
    public class UserBLL
    {
        public static bool AddUser(AppUser usr)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.AppUsers.Add(usr);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool Update(AppUser usr)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    usr.Branch = null;
                    db.AppUsers.Attach(usr);
                    db.Entry(usr).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                    rst = true;
                }
                return rst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<AppUser> GetUsersList(int orgId=0,string fname="")
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.AppUsers.Include("Branch").Include("branch.Area.Region.Organisation").OrderBy(o => o.FullName).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Branch.Area.Region.OrganisationID == orgId);
                    }
                    if (!string.IsNullOrEmpty(fname))
                    {
                        result = result.Where(b => b.FullName.Contains(fname));
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<AppUser> GetUserListLookup(int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.AppUsers.Include("Branch").Include("branch.Area.Region.Organisation").Where(b => b.IsActive == true).OrderBy(o => o.FullName).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Branch.Area.Region.OrganisationID == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AppUser GetUser(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.AppUsers.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<AppUser> GetUserByID(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var user = db.AppUsers.Include("Branch.Area.Region.Organisation").Where(b => b.ID == Id).ToList();
                    return user;
                }
                // return null;

            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }

        public static AppUser GetUserByUsername(string username)
        {
            try
            {
                AppUser user=null;
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    user = db.AppUsers.Include("Branch.Area.Region").Where(b => b.UserName.Trim() == username.Trim()).FirstOrDefault();
                    //return user;
                }
                return user;

            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }
        public static AppUser GetUserByEmail(string email)
        {
            try
            {
                AppUser user = null;
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    user = db.AppUsers.Include("Branch").Where(b => b.Email.Trim() == email.Trim()).FirstOrDefault();
                    //return user;
                }
                return user;

            }
            catch (Exception ex)
            {
                Utility.WriteError("Error Msg: " + ex.Message);
                throw ex;
            }
        }

    }
}