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
    
    public partial class LabelSetting
    {
        public int ID { get; set; }
        public Nullable<int> OrgId { get; set; }
        public string HomeLabel { get; set; }
        public string CategoryLabelAwesome { get; set; }
        public string CategoryLabelBad { get; set; }
        public string ServiceLabelAwesome { get; set; }
        public string ServiceLabelBad { get; set; }
        public string ServiceLabelIndifferent { get; set; }
        public string StaffLabelAwesome { get; set; }
        public string StaffLabelBad { get; set; }
    
        public virtual Organisation Organisation { get; set; }
    }
}
