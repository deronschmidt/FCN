using System;

namespace FCN.Models
{
    public class MemberRoleData
    {
        public int ID { get; set; }
        public string RoleType { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
