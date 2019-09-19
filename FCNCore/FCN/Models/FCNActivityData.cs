using System;

namespace FCN.Models
{
    public class FCNActivityData
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime ActivityDate { get; set; }
        public int CommunityID { get; set; }
        public string CommunityName { get; set; }
        public int FCNMemberID { get; set; }
        public string FCNMemberName { get; set; }
        public int ServiceCategoryID { get; set; }
        public string ServiceCategory { get; set; }
        public int ServiceSubcategoryID { get; set; }
        public string ServiceSubcategory { get; set; }
        public int PeopleServed { get; set; }
        public int UnpaidTime { get; set; }
        public int PaidTime { get; set; }
        public int Mileage { get; set; }
        public decimal OtherExpenses { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
