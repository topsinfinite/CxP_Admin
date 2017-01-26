using CustomerFeedbackSoln.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.UI.WebControls;

namespace CustomerFeedbackSoln.BLL
{
    public class EventBLL
    {
        public static bool AddEvent(Event evt)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.Events.Add(evt);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool Update(Event evt)
        {
            try
            {
                bool rst = false;
                using (var db = new FeedBackDBEntities())
                {
                    db.Events.Attach(evt);
                    db.Entry(evt).State = System.Data.EntityState.Modified;
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
        public static IEnumerable<Event> GetEventList(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.Events.Include(o=>o.Organisation).OrderByDescending(o =>o.DateAdded).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.OrganisationID == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public static IEnumerable<EventDevice> GetEventListLookup(int devId = 0)
        //{
        //    try
        //    {
        //        using (FeedBackDBEntities db = new FeedBackDBEntities())
        //        {
        //            var result = db.Events.OrderBy(p => p.Title).AsQueryable();
        //            var result = db.EventDevices.Include(o => o.Event).Include(p => p.DeviceTbl).OrderBy(o => o.DeviceID).AsQueryable();
        //            if (devId != 0)
        //            {

        //                result = result.Where(b => b.DeviceID == devId);
        //            }
        //            return result.ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static IEnumerable<Event> GetEventListLookup(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    //var result = db.Events.Where(z=>z.OrganisationID==orgId).OrderBy(p => p.Title).AsQueryable();
                    var result = db.Events.Where(z=>z.isActive==true).OrderBy(p => p.Title).AsQueryable();
                    if (orgId != 0)
                    {
                        
                        result = result.Where(b => b.OrganisationID == orgId);
                    }
                    return result.ToList();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<EventMetric> GetEventMetricListLookup(int evtID, int orgId = 0 )
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    //var result = db.Events.Where(z=>z.OrganisationID==orgId).OrderBy(p => p.Title).AsQueryable();
                    var result = db.EventMetrics.Include(z=>z.Event.Organisation).OrderBy(p => p.Label).AsQueryable();
                    if (orgId != 0)
                    {

                        result = result.Where(b => b.Event.OrganisationID == orgId && b.EventID==evtID);
                    }
                    else if(orgId==0 || orgId==1)
                    {
                        result = result.Where(b => b.EventID == evtID);
                    }
                    return result.ToList();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<EventMetric> GetEventMetricListLookup(string key, int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    //var result = db.Events.Where(z=>z.OrganisationID==orgId).OrderBy(p => p.Title).AsQueryable();
                    var result = db.EventMetrics.Include(z => z.Event.Organisation).OrderBy(p => p.Label).AsQueryable();
                    if (orgId != 0)
                    {

                        result = result.Where(b => b.Event.OrganisationID == orgId && b.ID==int.Parse(key));
                    }
                    return result.ToList();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<EventDevice> GetEventDeviceList(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.EventDevices.Include(o => o.Event).Include(p=>p.DeviceTbl.Branch.Area.Region.Organisation).OrderByDescending(o => o.DeviceID).AsQueryable();
                    
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Event.OrganisationID == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<DeviceTbl> GetEventDeviceListLookup(int orgID=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    //return db.DeviceTbls.Include(o => o.Branch.Area.Region.Organisation).Where(b => b.Branch.Area.Region.OrganisationID ==orgID).OrderBy(o => o.DeviceID).ToList();
                    var result = db.DeviceTbls.Include(o => o.Branch.Area.Region.Organisation).OrderBy(o => o.DeviceID).AsQueryable(); ;
                    //return db.DeviceTbls.Include(o => o.Branch.Area.Region.Organisation).Where(b => b.Branch.Area.Region.Organisation.OrganisationName == "Iedge Consulting ltd").OrderBy(o => o.DeviceID).ToList();
                        //("Branch.Area.Region.Organisation").Where(b => b.isActive).OrderBy(o => o.DeviceID).ToList();
                    //return db.DeviceTbls.Where(b => b.isActive == true).OrderBy(o => o.DeviceID).ToList();
                    if (orgID != 0)
                    {

                        result = result.Where(b => b.Branch.Area.Region.OrganisationID == orgID);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Event GetEvent(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.Events.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EventDevice GetEventDevice(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.EventDevices.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UpdateEventDevice(EventDevice dev)
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
        public static void BindEventDeviceList(DropDownList ddlList, int orgId = 0)
        {
            ddlList.DataValueField = "ID";
            ddlList.DataTextField = "DeviceID";
            ddlList.DataSource = EventBLL.GetEventDeviceListLookup(orgId);
            ddlList.DataBind();
        }
        public static void BindEventList(DropDownList ddlList, int orgId = 0)
        {
            ddlList.DataValueField = "ID";
            ddlList.DataTextField = "Title";
            ddlList.DataSource = EventBLL.GetEventListLookup(orgId);
            ddlList.DataBind();
        }
        public static void BindEventMetricList(DropDownList ddlList, int evtID, int orgId = 0)
        {
            ddlList.DataValueField = "ID";
            ddlList.DataTextField = "Label";
            ddlList.DataSource = EventBLL.GetEventMetricListLookup(evtID,orgId);
            ddlList.DataBind();
        }
        public static void BindEventMetricList(DropDownList ddlList, string key, int orgId = 0)
        {
            ddlList.DataValueField = "ID";
            ddlList.DataTextField = "Label";
            ddlList.DataSource = EventBLL.GetEventMetricListLookup(key,orgId);
            ddlList.DataBind();
        }
        public static bool AddDevice(EventDevice dev)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.EventDevices.Add(dev);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool ExistingDevice(int devID)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    
                    //var query = db.EventDevices.FirstOrDefault(z => z.DeviceID == devID && z.isActive == true);

                    var query = db.EventDevices.FirstOrDefault(z => z.DeviceID == devID  );
                   
                    if (query != null)
                    {
                       
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool DeviceExistForEvent(int evtID, int devID)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {

                   
                    var eventNameExist = db.EventDevices.FirstOrDefault(z => z.DeviceID == devID && z.EventID == evtID);
                    if (eventNameExist != null)
                    {

                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool EventExist(string eventName)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {


                    var eventNameExist = db.Events.FirstOrDefault(z => z.Title == eventName);
                    if (eventNameExist != null)
                    {

                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<EventMetric> GetEventMetricList(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.EventMetrics.Include(o=>o.Event).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Event.OrganisationID == orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<MetricElement> GetEventElementList( int evt, int metID, int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.MetricElements.Include(o => o.Event).Include(z=>z.EventMetric).AsQueryable();
                    if (orgId != 0 && evt == 0)
                    {
                        result = result.Where(b => b.Event.OrganisationID == orgId);
                    }
                    else if (orgId != 0 && evt != 0)
                        result = result.Where(b => b.Event.OrganisationID == orgId && b.EventID == evt);
                    else if (evt != 0 && metID != 0)
                        result = result.Where(b => b.EventID == evt && b.EventMetricID == metID);
                    else if (evt != 0 && metID == 0)
                        result = result.Where(b => b.EventID == evt);
                    if (orgId != 0 && evt != 0 && metID != 0)
                        result = result.Where(b => b.Event.OrganisationID == orgId && b.EventID == evt && b.EventMetricID == metID);

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static IEnumerable<EventMetric> GetSelectedEventMetric(int metID, int orgId=0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.EventMetrics.AsQueryable();
                    //if (orgId != 0 )
                    //{
                        result = result.Where(b => b.ID == metID);
                    //}
                    
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static EventMetric GetEventMetric(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.EventMetrics.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool AddEventMetric(EventMetric cat)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.EventMetrics.Add(cat);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateEventMetric(EventMetric cat)
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
        public static MetricElement GetEventElement(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.MetricElements.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool UpdateEventElement(MetricElement cat)
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
        public static bool AddEventElement(MetricElement cat)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.MetricElements.Add(cat);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<SmileyTypeEnum> SmileyTypeList()
        {
            List<SmileyTypeEnum> lst = new List<SmileyTypeEnum>()
           {
               new SmileyTypeEnum{DataField="Awesome",DataValue="1"},
               new SmileyTypeEnum{DataField="Just Okay", DataValue="2"},
               new SmileyTypeEnum{DataField="Terrible",DataValue="3"}
           };
            return lst;
        }
        public static string GetEventSmileyType(object o)
        {
            try
            {
                string type = "";
                if (o.ToString() == "1") { type = "Awesome"; }
                if (o.ToString() == "2") { type = "Just Okay"; }
                if (o.ToString() == "3") { type = "Terrible"; }
                return type;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static void BindSmileyTypeList(DropDownList ddlList)
        {
            ddlList.DataValueField = "DataValue";
            ddlList.DataTextField = "DataField";
            ddlList.DataSource = EventBLL.SmileyTypeList();
            ddlList.DataBind();
        }

        public static IEnumerable<HomeSetting> GetEventSmileyActionList(int orgId = 0)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    var result = db.HomeSettings.Include(z=>z.Event.Organisation).OrderByDescending(o => o.EventID).AsQueryable();
                    if (orgId != 0)
                    {
                        result = result.Where(b => b.Event.OrganisationID== orgId);
                    }
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static HomeSetting GetEventSmileyAction(int Id)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    return db.HomeSettings.Find(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool UpdateEventSmileyAction(HomeSetting cat)
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

        public static bool AddEventSmileyAction(HomeSetting cat)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {
                    db.HomeSettings.Add(cat);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SmileyExistForEvent(int smlID, int OrgId, int evtId)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {


                    var eventNameExist = db.HomeSettings.FirstOrDefault(z => z.SmileyType == smlID && z.OrgID == OrgId && z.EventID==evtId);
                    if (eventNameExist != null)
                    {

                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static bool LimitingEventMetric(int evtID)
        {
            try
            {
                using (FeedBackDBEntities db = new FeedBackDBEntities())
                {


                    var eventNameExist = db.EventMetrics.Where(z => z.EventID==evtID).Count();
                    if (eventNameExist ==4)
                    {

                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}