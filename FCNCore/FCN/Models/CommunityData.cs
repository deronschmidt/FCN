using System;
using System.Collections.Generic;

namespace FCN.Models
{
    public class CommunityData
    {
        public int ID { get; set; }
        public string CommunityName { get; set; }
        public string Affiliation { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool Active { get; set; }
        public List<CommunityContactData> Contacts {get;set;}
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

}
