using System;

namespace FCN.Models
{
    public class CommunityContactData
    {
        public int ID { get; set; }
        public int CommunityID { get; set; }
        public string CommunityName { get; set; }
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
