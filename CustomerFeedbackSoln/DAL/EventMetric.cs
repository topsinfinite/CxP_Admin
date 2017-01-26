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
    
    public partial class EventMetric
    {
        public EventMetric()
        {
            this.EventFeedbacks = new HashSet<EventFeedback>();
            this.MetricElements = new HashSet<MetricElement>();
        }
    
        public int ID { get; set; }
        public Nullable<int> EventID { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string Title { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual ICollection<EventFeedback> EventFeedbacks { get; set; }
        public virtual ICollection<MetricElement> MetricElements { get; set; }
    }
}
