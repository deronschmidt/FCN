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
        [Authorize(Roles = "Admin")]
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
            int isActive = (data.Active.ToString() == "True" ? 1 : 0);

            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlInsert = "exec FCN..ins_member_role @role_type, @description, @active";

            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, fcnDBConnection))
                {
                    cmdInsert.Parameters.AddWithValue("@role_type", data.RoleType);
                    cmdInsert.Parameters.AddWithValue("@description", data.Description);
                    cmdInsert.Parameters.AddWithValue("@active", isActive);

                    using (SqlDataReader reader = cmdInsert.ExecuteReader())
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
            int isActive = (data.Active.ToString() == "True" ? 1 : 0);
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlUpdate = "exec FCN..upd_member_role @ID, @role_type, @description, @active";
            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, fcnDBConnection))
                {
                    cmdUpdate.Parameters.AddWithValue("@ID", id);
                    cmdUpdate.Parameters.AddWithValue("@role_type", data.RoleType);
                    cmdUpdate.Parameters.AddWithValue("@description", data.Description);
                    cmdUpdate.Parameters.AddWithValue("@active", isActive);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            data = GetMemberRole().FirstOrDefault((p) => p.ID == id);
            return Ok(data);
        }

        // DELETE: api/MemberRole/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteMemberRole(int id)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlDelete = "exec FCN..del_member_role @ID";
            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdDelete = new SqlCommand(sqlDelete, fcnDBConnection))
                {
                    cmdDelete.Parameters.AddWithValue("@ID", id);
                    cmdDelete.ExecuteNonQuery();
                }
            }

            return NoContent();
        }
    }
}
