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
    public class CommunityController : ControllerBase
    {
        // GET: api/Community
        [HttpGet]
        public IEnumerable<CommunityData> GetCommunity()
        {
            List<CommunityData> communities = new List<CommunityData>();
            string sqlSelect = "exec sel_community";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    CommunityData newCommunity = new CommunityData
                    {
                        ID = (int)reader["ID"],
                        CommunityName = reader["community_name"].ToString(),
                        Affiliation = reader["affiliation"].ToString(),
                        Address1 = reader["address1"].ToString(),
                        Address2 = reader["address2"].ToString(),
                        City = reader["city"].ToString(),
                        State = reader["state"].ToString(),
                        ZipCode = reader["zip_code"].ToString(),
                        Phone = reader["phone"].ToString(),
                        AlternatePhone = reader["alt_phone"].ToString(),
                        Email = reader["email"].ToString(),
                        Website = reader["website"].ToString(),
                        Active = reader["active"].ToString() == "1" ? true : false,
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };

                    string fcnConnectionString = Utilities.GetDBConnectionString();
                    string sqlSelectContacts = "exec sel_community_contact_by_community @communityID";
                    using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
                    {
                        fcnDBConnection.Open();
                        using (SqlCommand cmdContacts = new SqlCommand(sqlSelectContacts, fcnDBConnection))
                        {
                            cmdContacts.Parameters.AddWithValue("@communityID", newCommunity.ID);
                            using (SqlDataReader readerContacts = cmdContacts.ExecuteReader())
                            {
                                while (readerContacts.Read())
                                {
                                    CommunityContactData newContact = new CommunityContactData
                                    {
                                        ID = (int)readerContacts["ID"],
                                        CommunityID = (int)readerContacts["communityID"],
                                        CommunityName = readerContacts["community_name"].ToString(),
                                        ContactID = (int)readerContacts["contactID"],
                                        ContactName = readerContacts["first_name"].ToString() + " " + readerContacts["last_name"].ToString(),
                                        CreatedDate = (DateTime)reader["created_date"],
                                        UpdatedDate = (DateTime)reader["updated_date"]

                                    };

                                    if (newCommunity.Contacts == null)
                                    {
                                        newCommunity.Contacts = new List<CommunityContactData>();
                                    }
                                    newCommunity.Contacts.Add(newContact);
                                }
                            }
                        }
                    }

                    communities.Add(newCommunity);
                }
            }
            return communities;
        }

        // GET: api/Community/5
        [HttpGet("{id}")]
        public ActionResult<CommunityData> GetCommunity(int id)
        {
            CommunityData data = GetCommunity().FirstOrDefault((p) => p.ID == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST: api/Community
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<CommunityData> PostCommunity([FromBody] CommunityData data)
        {
            int id = 0;

            int isActive = (data.Active.ToString() == "True" ? 1 : 0);

            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlInsert = "exec FCN..ins_community @community_name, @affiliation, @address1, @address2, @city, @state, @zip_code, @phone, @alt_phone, @email, @website, @active";

            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, fcnDBConnection))
                {
                    cmdInsert.Parameters.AddWithValue("@community_name", data.CommunityName);
                    cmdInsert.Parameters.AddWithValue("@affiliation", data.Affiliation);
                    cmdInsert.Parameters.AddWithValue("@address1", data.Address1);
                    cmdInsert.Parameters.AddWithValue("@address2", data.Address2);
                    cmdInsert.Parameters.AddWithValue("@city", data.City);
                    cmdInsert.Parameters.AddWithValue("@state", data.State);
                    cmdInsert.Parameters.AddWithValue("@zip_code", data.ZipCode);
                    cmdInsert.Parameters.AddWithValue("@phone", data.Phone);
                    cmdInsert.Parameters.AddWithValue("@alt_phone", data.AlternatePhone);
                    cmdInsert.Parameters.AddWithValue("@email", data.Email);
                    cmdInsert.Parameters.AddWithValue("@website", data.Website);
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

            data = GetCommunity().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetCommunity", new { id }, data);
        }

        // PUT: api/Community/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<CommunityData> PutCommunity(int id, [FromBody] CommunityData data)
        {
            int isActive = (data.Active.ToString() == "True" ? 1 : 0);
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlUpdate = "exec FCN..upd_community @ID, @community_name, @affiliation, @address1, @address2, @city, @state, @zip_code, @phone, @alt_phone, @email, @website, @active";
            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, fcnDBConnection))
                {
                    cmdUpdate.Parameters.AddWithValue("@ID", id);
                    cmdUpdate.Parameters.AddWithValue("@community_name", data.CommunityName);
                    cmdUpdate.Parameters.AddWithValue("@affiliation", data.Affiliation);
                    cmdUpdate.Parameters.AddWithValue("@address1", data.Address1);
                    cmdUpdate.Parameters.AddWithValue("@address2", data.Address2);
                    cmdUpdate.Parameters.AddWithValue("@city", data.City);
                    cmdUpdate.Parameters.AddWithValue("@state", data.State);
                    cmdUpdate.Parameters.AddWithValue("@zip_code", data.ZipCode);
                    cmdUpdate.Parameters.AddWithValue("@phone", data.Phone);
                    cmdUpdate.Parameters.AddWithValue("@alt_phone", data.AlternatePhone);
                    cmdUpdate.Parameters.AddWithValue("@email", data.Email);
                    cmdUpdate.Parameters.AddWithValue("@website", data.Website);
                    cmdUpdate.Parameters.AddWithValue("@active", isActive);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            data = GetCommunity().FirstOrDefault((p) => p.ID == id);
            return Ok(data);
        }

        // DELETE: api/Community/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCommunity(int id)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlDelete = "exec FCN..del_community @ID";
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
