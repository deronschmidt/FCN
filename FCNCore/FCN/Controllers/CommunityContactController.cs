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

            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlInsert = "exec FCN..ins_community_contact @communityID, @contactID";

            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, fcnDBConnection))
                {
                    cmdInsert.Parameters.AddWithValue("@communityID", data.CommunityID);
                    cmdInsert.Parameters.AddWithValue("@contactID", data.ContactID);

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

            data = GetCommunityContact().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetCommunityContact", new { id }, data);
        }

        // PUT: api/CommunityContact/5      
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<CommunityContactData> PutCommunityContact(int id, [FromBody] CommunityContactData data)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlUpdate = "exec FCN..upd_community_contact @ID, @communityID, @contactID";
            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, fcnDBConnection))
                {
                    cmdUpdate.Parameters.AddWithValue("@ID", id);
                    cmdUpdate.Parameters.AddWithValue("@communityID", data.CommunityID);
                    cmdUpdate.Parameters.AddWithValue("@contactID", data.ContactID);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            data = GetCommunityContact().FirstOrDefault((p) => p.ID == id);
            return Ok(data);
        }

        // DELETE: api/CommunityContact/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCommunityContact(int id)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlDelete = "exec FCN..del_community_contact @ID";
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