using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FCN.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using FCNHelpers;

namespace FCN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRoleController : ControllerBase
    {
        // GET: api/MemberRole
        [HttpGet]
        public IEnumerable<MemberRoleData> GetMemberRole()
        {
            List<MemberRoleData> memberRoles = new List<MemberRoleData>();
            string sqlSelect = "exec sel_member_role";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    string updateFirstName = string.Empty;
                    string updateLastName = string.Empty;
                    MemberRoleData newMemberRole = new MemberRoleData
                    {
                        ID = (int)reader["ID"],
                        RoleType = reader["role_type"].ToString(),
                        Description = reader["description"].ToString(),
                        Active = reader["active"].ToString() == "1" ? true : false,
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };
                    memberRoles.Add(newMemberRole);
                }
            }
            return memberRoles;
        }

        // GET: api/MemberRole/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<MemberRoleData> GetMemberRole(int id)
        {
            MemberRoleData data = GetMemberRole().FirstOrDefault((p) => p.ID == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST: api/MemberRole
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<MemberRoleData> PostMemberRole([FromBody] MemberRoleData data)
        {
            int id = 0;

            string sqlInsert = string.Format("exec FCN..ins_member_role '{0}','{1}',{2}",
                            data.RoleType.Replace("'", "''"), data.Description.Replace("'", "''"),
                            data.Active.ToString() == "True" ? 1 : 0);

            using (SqlDataReader reader = Utilities.ExecQuery(sqlInsert))
            {
                while (reader.Read())
                {
                    if (reader["ID"] != DBNull.Value)
                    {
                        id = (int)reader["ID"];
                    }

                    if (id == 0)
                    {
                        return NotFound();
                    }
                }
            }

            data = GetMemberRole().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetMemberRole", new { id }, data);
        }

        // PUT: api/MemberRole/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<MemberRoleData> PutMemberRole(int id, [FromBody] MemberRoleData data)
        {
            string sqlUpdate = string.Format("exec FCN..upd_member_role {0},'{1}','{2}',{3}",
                            id, data.RoleType.Replace("'", "''"), data.Description.Replace("'", "''"),
                            data.Active.ToString() == "True" ? 1 : 0);
            bool cmdStatus = Utilities.ExecNonQuery(sqlUpdate);

            if (cmdStatus)
            {
                data = GetMemberRole().FirstOrDefault((p) => p.ID == id);
                return Ok(data);
            }
            else
                return BadRequest();
        }

        // DELETE: api/MemberRole/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteMemberRole(int id)
        {
            string sqlDelete = string.Format("exec FCN..del_member_role {0}", id);
            bool cmdStatus = Utilities.ExecNonQuery(sqlDelete);
            if (cmdStatus)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
