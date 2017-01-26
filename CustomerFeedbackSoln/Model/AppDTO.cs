using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerFeedbackSoln.Model
{
    public class AppDTO
    {
    }
    public class LabelSettingDTO
    {
        public int ID { get; set; }
      
        public string HomeLabel { get; set; }
        public string CategoryLabelAwesome { get; set; }
        public string CategoryLabelBad { get; set; }
        public string ServiceLabelAwesome { get; set; }
        public string ServiceLabelBad { get; set; }
        public string ServiceLabelIndifferent { get; set; }
        public string StaffLabelAwesome { get; set; }
        public string StaffLabelBad { get; set; }
    }
    public class OrgSettingDTO
    {
        public int ID { get; set; }
        public string HomeLabel { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public Nullable<bool> isActive { get; set; }
    }
    public class CategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CategoryIcon { get; set; }
        public Nullable<Int32> CategoryType { get; set; }
        public Nullable<bool> isActive { get; set; }
    }
    public class SmileyActionDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SmileyImage { get; set; }
        public Nullable<Int32> SmileyType { get; set; }
        public Nullable<bool> isActive { get; set; }
    }

    public class ServicesDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ServiceIcon { get; set; }
        public int ServiceCatId { get; set; }
        public Nullable<bool> isActive { get; set; }
    }
    public class FeedbackDTO
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> SmileyActionID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public string Comment { get; set; }

        public string DeviceID { get; set; }
        //field to check if comment is included
        public string CommentInclude { get; set; }
    }
}