using System;

namespace FCN.Models
{
    public class FCNMemberData
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string RoleType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public int CommunityID { get; set; }
        public string CommunityName { get; set; }
        public bool Licensed { get; set; }
        public bool Active { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public bool Administrator { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
