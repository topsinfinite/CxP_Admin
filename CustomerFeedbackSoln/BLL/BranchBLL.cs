using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomerFeedbackSoln.DAL;
namespace CustomerFeedbackSoln.BLL
{
    public class BranchBLL
    {
        public static bool AddBranch(Branch brch)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.Branches.Add(brch);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool Update(Branch brch)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    db.Branches.Attach(brch);
                    db.Entry(brch).State = System.Data.EntityState.Modified;
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
        public static IEnumerable<Branch> GetBranchList(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.Branches.Include("Area").Include("Area.Region").Include("Area.Region.Organisation").OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Area.Region.OrganisationID == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<Branch> GetBranchListLookup(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result= db.Branches.Include("Area").Include("Area.Region").Include("Area.Region.Organisation").Where(b => b.isActive == true).OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Area.Region.OrganisationID == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public static Branch GetBranch(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Branches.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public static bool AddArea(Area area)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {
                   db.Areas.Add(area);
                   db.SaveChanges();
                   return true;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static bool UpdateArea(Area area)
       {
           try
           {
               bool rst = false;
               using (var db = new FeedBackDBEntities())
               {
                   db.Areas.Attach(area);
                   db.Entry(area).State = System.Data.EntityState.Modified;
                  // area.Region = null;
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
       public static IEnumerable<Area> GetAreaList(int orgId = 0)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {

                   var result = db.Areas.Include("Region").Include("Region.Organisation").OrderBy(o => o.Name).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Region.OrganisationID.Value == orgId);
                    }

                    return result.ToList();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static IEnumerable<Area> GetAreaListLookup(int orgId = 0)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {
                   var result= db.Areas.Include("Region").Include("Region.Organisation").Where(b => b.isActive == true).OrderBy(o => o.Name).AsQueryable();
                   if (orgId != 0)
                   {
                       result = result.Where(b => b.Region.OrganisationID.Value == orgId);
                   }
                   return result.ToList();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static Area GetArea(int Id)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {
                   return db.Areas.Include("Region").Where(a=>a.ID==Id).FirstOrDefault();
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public static bool AddRegion(Region reg)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {
                   db.Regions.Add(reg);
                   db.SaveChanges();
                   return true;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static bool UpdateRegion(Region area)
       {
           try
           {
               bool rst = false;
               using (var db = new FeedBackDBEntities())
               {
                   db.Regions.Attach(area);
                   db.Entry(area).State = System.Data.EntityState.Modified;
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
       public static IEnumerable<Region> GetRegionList(int orgId=0)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {
                   if (orgId == 0)
                   {
                       return db.Regions.Include("Organisation").OrderBy(o => o.Name).ToList();
                   }
                   else
                   {
                       return db.Regions.Include("Organisation").Where(j=>j.OrganisationID==orgId).OrderBy(o => o.Name).ToList();
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static IEnumerable<Region> GetRegionListLookup(int orgId = 0)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {
                   if (orgId == 0)
                   {
                       return db.Regions.Include("Organisation").Where(b => b.isActive == true).OrderBy(o => o.Name).ToList();
                   }
                   else
                   {
                       return db.Regions.Include("Organisation").Where(b => b.isActive == true && b.OrganisationID==orgId).OrderBy(o => o.Name).ToList();
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public static Region GetRegion(int Id)
       {
           try
           {
               using (FeedBackDBEntities db = new FeedBackDBEntities())
               {
                   return db.Regions.Find(Id);
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}