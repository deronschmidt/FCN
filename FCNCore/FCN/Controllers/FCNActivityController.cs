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
    public class FCNActivityController : ControllerBase
    {
        // GET: api/FCNActivity
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<FCNActivityData> GetFCNActivity()
        {
            List<FCNActivityData> fCNActivities = new List<FCNActivityData>();
            string sqlSelect = "exec sel_fcn_activity";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    string memberFirstName = string.Empty;
                    string memberLastName = string.Empty;
                    string updateFirstName = string.Empty;
                    string updateLastName = string.Empty;
                    FCNActivityData newFCNActivity = new FCNActivityData
                    {
                        ID = (int)reader["ID"],
                        Description = reader["description"].ToString(),
                        Comments = reader["comments"].ToString(),
                        ActivityDate = (DateTime)reader["activity_date"],
                        CommunityID = (int)reader["communityID"],
                        CommunityName = reader["community_name"].ToString(),
                        FCNMemberID = (int)reader["fcn_memberID"],
                        FCNMemberName = reader["provider_first_name"].ToString() + " " + reader["provider_last_name"].ToString(),
                        ServiceCategoryID = (int)reader["service_categoryID"],
                        ServiceCategory = reader["category_name"].ToString(),
                        PeopleServed = (int)reader["people_served"],
                        UnpaidTime = (int)reader["unpaid_time"],
                        PaidTime = (int)reader["paid_time"],
                        Mileage = (int)reader["mileage"],
                        OtherExpenses = (decimal)reader["other_expenses"],
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };                    

                    if (reader["service_subcategoryID"] != DBNull.Value)
                    {
                        newFCNActivity.ServiceSubcategoryID = (int)reader["service_subcategoryID"];
                        newFCNActivity.ServiceSubcategory = reader["subcategory_name"].ToString();
                    }

                    fCNActivities.Add(newFCNActivity);
                }
            }
            return fCNActivities;
        }

        // GET: api/FCNActivity/5
        [HttpGet("{id}")]
        public ActionResult<FCNActivityData> GetFCNActivity(int id)
        {
            FCNActivityData data = GetFCNActivity().FirstOrDefault((p) => p.ID == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST: api/FCNActivity
        [HttpPost]
        public ActionResult<FCNActivityData> PostFCNActivity([FromBody] FCNActivityData data)
        {
            int id = 0;

            string sqlInsert = string.Format("exec FCN..ins_fcn_activity '{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                            data.Description.Replace("'","''"), data.Comments.Replace("'", "''"), data.ActivityDate, data.CommunityID, 
                            data.FCNMemberID, data.ServiceCategoryID, Utilities.ToDBNull(data.ServiceSubcategoryID),
                            data.PeopleServed, data.UnpaidTime, data.PaidTime, data.Mileage, data.OtherExpenses);
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

            data = GetFCNActivity().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetFCNActivity", new { id }, data);
        }

        // PUT: api/FCNActivity/5
        [HttpPut("{id}")]
        public ActionResult<FCNActivityData> PutFCNActivity(int id, [FromBody] FCNActivityData data)
        {
            string sqlUpdate = string.Format("exec FCN..upd_fcn_activity {0},'{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                                id, data.Description.Replace("'", "''"), data.Comments.Replace("'", "''"), data.ActivityDate, data.CommunityID,
                                data.FCNMemberID, data.ServiceCategoryID, Utilities.ToDBNull(data.ServiceSubcategoryID),
                                data.PeopleServed, data.UnpaidTime, data.PaidTime, data.Mileage, data.OtherExpenses);
            bool cmdStatus = Utilities.ExecNonQuery(sqlUpdate);
            if (cmdStatus)
            {
                data = GetFCNActivity().FirstOrDefault((p) => p.ID == id);
                return Ok(data);
            }
            else
                return BadRequest();
        }

        // DELETE: api/FCNActivity/5
        [HttpDelete("{id}")]
        public ActionResult DeleteFCNActivity(int id)
        {
            string sqlDelete = string.Format("exec FCN..del_fcn_activity {0}", id);
            bool cmdStatus = Utilities.ExecNonQuery(sqlDelete);
            if (cmdStatus)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
