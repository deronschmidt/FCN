using System;

namespace FCN.Models
{
    public class ServiceSubcategoryData
    {
        public int ID { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
