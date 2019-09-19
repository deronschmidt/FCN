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
                    
                    string sqlSelectContacts = string.Format("exec sel_community_contact_by_community {0}", newCommunity.ID);
                    using (SqlDataReader readerContacts = Utilities.ExecQuery(sqlSelectContacts))
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

            string sqlInsert = string.Format("exec FCN..ins_community '{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8},{9},{10},{11}",
                    data.CommunityName.Replace("'", "''"), data.Affiliation.Replace("'", "''"), data.Address1.Replace("'", "''"),
                    Utilities.ToDBNull(data.Address2), data.City.Replace("'", "''"), data.State, data.ZipCode, data.Phone,
                    Utilities.ToDBNull(data.AlternatePhone), Utilities.ToDBNull(data.Email), Utilities.ToDBNull(data.Website), 
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

            data = GetCommunity().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetCommunity", new { id }, data);
        }

        // PUT: api/Community/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<CommunityData> PutCommunity(int id, [FromBody] CommunityData data)
        {
            string sqlUpdate = string.Format("exec FCN..upd_community {0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',{9},{10},{11},{12}",
                                id, data.CommunityName.Replace("'", "''"), data.Affiliation.Replace("'", "''"), data.Address1.Replace("'", "''"),
                                Utilities.ToDBNull(data.Address2), data.City.Replace("'", "''"), data.State, data.ZipCode, data.Phone,
                                Utilities.ToDBNull(data.AlternatePhone), Utilities.ToDBNull(data.Email), Utilities.ToDBNull(data.Website),
                                data.Active.ToString() == "True" ? 1 : 0);
            bool cmdStatus = Utilities.ExecNonQuery(sqlUpdate);
            if (cmdStatus)
            {
                data = GetCommunity().FirstOrDefault((p) => p.ID == id);
                return Ok(data);
            }
            else
                return BadRequest();

        }

        // DELETE: api/Community/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCommunity(int id)
        {
            string sqlDelete = string.Format("exec FCN..del_community {0}", id);
            bool cmdStatus = Utilities.ExecNonQuery(sqlDelete);
            if (cmdStatus)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
