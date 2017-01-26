using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerFeedbackSoln.DAL;
using System.Web.Security;

namespace CustomerFeedbackSoln.BLL
{
    public class AppUserExtended : AppUser
    {
        public bool isOnline { get; set; }
        public DateTime LastActive { get; set; }
    }
    public class LookupBLL
    {
        public static bool AddDevice(DeviceTbl dev)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.DeviceTbls.Add(dev);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateDevice(DeviceTbl dev)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    // db.Categories.Attach(cat);
                    db.Entry(dev).State = System.Data.EntityState.Modified;
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
        public static IEnumerable<DeviceTbl> GetDeviceList(int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.DeviceTbls.Include("Branch.Area.Region.Organisation").Include("AppUser").OrderBy(o => o.DateAdded).AsQueryable();
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
        public static IEnumerable<DeviceTbl> GetDeviceListLookup()
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.DeviceTbls.Include("Branch").Where(b => b.isActive == true).OrderBy(o => o.DateAdded).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DeviceTbl GetDevice(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.DeviceTbls.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DeviceTbl GetByDeviceID(string DeviceId)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.DeviceTbls.Include("Branch").Where(b => b.isActive == true && b.DeviceID==DeviceId).OrderBy(o => o.DateAdded).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool AddCategory(Category cat)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.Categories.Add(cat);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateCategory(Category cat)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                   // db.Categories.Attach(cat);
                    db.Entry(cat).State = System.Data.EntityState.Modified;
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
        public static IEnumerable<Category> GetCategoryList(int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.Categories.Include("Organisation").OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrganisationId == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<Category> GetCategoryListLookup(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.Categories.Include("Organisation").Where(b => b.isActive == true).OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrganisationId == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Category GetCategories(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Categories.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddService(Service cat)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.Services.Add(cat);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateService(Service cat)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    // db.Categories.Attach(cat);
                    db.Entry(cat).State = System.Data.EntityState.Modified;
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
        public static IEnumerable<Service> GetServiceList(int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.Services.Include("Organisation").OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrganisationId == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<Service> GetServiceListLookup(int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.Services.Where(b => b.isActive == true).OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrganisationId == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Service GetService(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Services.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool AddGalleryIcon(IconGallery cat)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.IconGalleries.Add(cat);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateGalleryIcon(IconGallery cat)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    // db.Categories.Attach(cat);
                    db.Entry(cat).State = System.Data.EntityState.Modified;
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
        public static IconGallery GetIcon(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.IconGalleries.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<IconGallery> GetGalleryIcons(int type=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {

                    var result = db.IconGalleries.Where(i=>i.isActive==true).AsQueryable();
                    if (type != 0)
                        result = result.Where(i => i.Type == type);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool AddSmileyAction(SmileyAction cat)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.SmileyActions.Add(cat);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateSmileyAction(SmileyAction cat)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    // db.Categories.Attach(cat);
                    db.Entry(cat).State = System.Data.EntityState.Modified;
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
        public static IEnumerable<SmileyAction> GetSmileyActionList(int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.SmileyActions.Include ("Organisation").OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrganisationId == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<SmileyAction> GetSmileyActionListLookup(int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.SmileyActions.Where(b => b.isActive == true).OrderBy(o => o.ID).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrganisationId == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static SmileyAction GetSmileyAction(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.SmileyActions.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<AppUserExtended> GetAllUsers()
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var query = (from a in Membership.GetAllUsers().Cast<MembershipUser>()
                                 from b in db.AppUsers
                                 where a.UserName == b.UserName
                                 select new AppUserExtended
                                 {
                                     UserName = a.UserName,
                                     FullName = b.FullName,
                                     LastActive = a.LastActivityDate,
                                     isOnline = a.IsOnline
                                 }).ToList();

                    return query;
                }
            }catch(Exception ex)
            {
                return null;
            }
        }

        public static bool AddOrganization(Organisation org)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.Organisations.Add(org);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool UpdateOrganisation(Organisation org)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    // db.Categories.Attach(cat);
                    db.Entry(org).State = System.Data.EntityState.Modified;
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
        public static IEnumerable<Organisation> GetOrganisationList()
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Organisations.Include("State").OrderBy(o => o.OrganisationName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<Organisation> GetOrganisationListByName(string name)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Organisations.Include("State").Where(b=>b.OrganisationName.Contains(name)).OrderBy(o => o.OrganisationName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<Organisation> GetOrganisationListLookup()
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Organisations.Where(b => b.isActive == true).OrderBy(o => o.OrganisationName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Organisation GetOrganisation(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Organisations.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<State> GetStateList()
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.States.OrderBy(o => o.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<LabelSetting> GetLabelList(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.LabelSettings.Include("Organisation").OrderBy(o => o.OrgId).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrgId == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateLabelSetting(LabelSetting cat)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    // db.Categories.Attach(cat);
                    db.Entry(cat).State = System.Data.EntityState.Modified;
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
        public static LabelSetting GetLabelSetting(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.LabelSettings.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}