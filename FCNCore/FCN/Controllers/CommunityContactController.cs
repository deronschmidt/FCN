using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FCN.Models;
using FCNHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FCN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityContactController : ControllerBase
    {
        // GET: api/CommunityContact
        [HttpGet]
        public IEnumerable<CommunityContactData> GetCommunityContact()
        {
            List<CommunityContactData> communityContacts = new List<CommunityContactData>();
            string sqlSelect = "exec sel_community_contact";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    CommunityContactData newCommunityContact = new CommunityContactData
                    {
                        ID = (int)reader["ID"],
                        CommunityID = (int)reader["communityID"],
                        CommunityName = reader["community_name"].ToString(),
                        ContactID = (int)reader["contactID"],
                        ContactName = reader["first_name"].ToString() + " " + reader["last_name"].ToString(),
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };

                    communityContacts.Add(newCommunityContact);
                }
            }
            return communityContacts;
        }

        // GET: api/CommunityContact/5
        [HttpGet("{id}")]
        public ActionResult<CommunityContactData> GetCommunityContact(int id)
        {
            CommunityContactData data = GetCommunityContact().FirstOrDefault((p) => p.ID == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST: api/CommunityContact
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<CommunityContactData> PostCommunityContact([FromBody] CommunityContactData data)
        {
            int id = 0;

            string sqlInsert = string.Format("exec FCN..ins_community_contact {0},{1}",
                                                data.CommunityID, data.ContactID);

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

            data = GetCommunityContact().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetCommunityContact", new { id }, data);
        }

        // PUT: api/CommunityContact/5      
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<CommunityContactData> PutCommunityContact(int id, [FromBody] CommunityContactData data)
        {
            string sqlUpdate = string.Format("exec FCN..upd_community_contact {0},{1},{2}",
                                id, data.CommunityID, data.ContactID);
            bool cmdStatus = Utilities.ExecNonQuery(sqlUpdate);

            if (cmdStatus)
            {
                data = GetCommunityContact().FirstOrDefault((p) => p.ID == id);
                return Ok(data);
            }
            else
                return BadRequest();

        }

        // DELETE: api/CommunityContact/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCommunityContact(int id)
        {
            string sqlDelete = string.Format("exec FCN..del_community_contact {0}", id);
            bool cmdStatus = Utilities.ExecNonQuery(sqlDelete);
            if (cmdStatus)
                return NoContent();
            else
                return BadRequest();
        }
    }
}