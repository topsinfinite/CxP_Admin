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
    
    public partial class Feedback
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> SmileyActionID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string Comment { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual Category Category { get; set; }
        public virtual Service Service { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual SmileyAction SmileyAction { get; set; }
    }
}