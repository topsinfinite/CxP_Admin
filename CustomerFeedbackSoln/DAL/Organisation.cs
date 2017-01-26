//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomerFeedbackSoln.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organisation
    {
        public Organisation()
        {
            this.Regions = new HashSet<Region>();
            this.Categories = new HashSet<Category>();
            this.Services = new HashSet<Service>();
            this.SmileyActions = new HashSet<SmileyAction>();
            this.LabelSettings = new HashSet<LabelSetting>();
            this.Events = new HashSet<Event>();
            this.HomeSettings = new HashSet<HomeSetting>();
            this.EventDevices = new HashSet<EventDevice>();
        }
    
        public int ID { get; set; }
        public string OrgID { get; set; }
        public string OrganisationName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMobileNo { get; set; }
        public string ContactPhoneNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<int> StateID { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string HomeLabel { get; set; }
        public Nullable<decimal> AccountBalance { get; set; }
    
        public virtual AppUser AppUser { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<SmileyAction> SmileyActions { get; set; }
        public virtual ICollection<LabelSetting> LabelSettings { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<HomeSetting> HomeSettings { get; set; }
        public virtual ICollection<EventDevice> EventDevices { get; set; }
    }
}
