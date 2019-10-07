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
            
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlInsert = "exec FCN..ins_fcn_activity @description, @comments, @activity_date, @communityID, @fcn_memberID, @service_categoryID, @service_subcategoryID, " +
                                                          "@people_served, @unpaid_time, @paid_time, @mileage, @other_expenses";

            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, fcnDBConnection))
                {
                    cmdInsert.Parameters.AddWithValue("@description", data.Description);
                    cmdInsert.Parameters.AddWithValue("@comments", data.Comments);
                    cmdInsert.Parameters.AddWithValue("@activity_date", data.ActivityDate);
                    cmdInsert.Parameters.AddWithValue("@communityID", data.CommunityID);
                    cmdInsert.Parameters.AddWithValue("@fcn_memberID", data.FCNMemberID);
                    cmdInsert.Parameters.AddWithValue("@service_categoryID", data.ServiceCategoryID);
                    cmdInsert.Parameters.AddWithValue("@service_subcategoryID", data.ServiceSubcategoryID);
                    cmdInsert.Parameters.AddWithValue("@people_served", data.PeopleServed);
                    cmdInsert.Parameters.AddWithValue("@unpaid_time", data.UnpaidTime);
                    cmdInsert.Parameters.AddWithValue("@paid_time", data.PaidTime);
                    cmdInsert.Parameters.AddWithValue("@mileage", data.Mileage);
                    cmdInsert.Parameters.AddWithValue("@other_expenses", data.OtherExpenses);

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

            data = GetFCNActivity().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetFCNActivity", new { id }, data);
        }

        // PUT: api/FCNActivity/5
        [HttpPut("{id}")]
        public ActionResult<FCNActivityData> PutFCNActivity(int id, [FromBody] FCNActivityData data)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlUpdate = "exec FCN..upd_fcn_activity @ID, @description, @comments, @activity_date, @communityID, @fcn_memberID, @service_categoryID, @service_subcategoryID, " +
                                                          "@people_served, @unpaid_time, @paid_time, @mileage, @other_expenses";
            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, fcnDBConnection))
                {
                    cmdUpdate.Parameters.AddWithValue("@ID", id);
                    cmdUpdate.Parameters.AddWithValue("@description", data.Description);
                    cmdUpdate.Parameters.AddWithValue("@comments", data.Comments);
                    cmdUpdate.Parameters.AddWithValue("@activity_date", data.ActivityDate);
                    cmdUpdate.Parameters.AddWithValue("@communityID", data.CommunityID);
                    cmdUpdate.Parameters.AddWithValue("@fcn_memberID", data.FCNMemberID);
                    cmdUpdate.Parameters.AddWithValue("@service_categoryID", data.ServiceCategoryID);
                    cmdUpdate.Parameters.AddWithValue("@service_subcategoryID", data.ServiceSubcategoryID);
                    cmdUpdate.Parameters.AddWithValue("@people_served", data.PeopleServed);
                    cmdUpdate.Parameters.AddWithValue("@unpaid_time", data.UnpaidTime);
                    cmdUpdate.Parameters.AddWithValue("@paid_time", data.PaidTime);
                    cmdUpdate.Parameters.AddWithValue("@mileage", data.Mileage);
                    cmdUpdate.Parameters.AddWithValue("@other_expenses", data.OtherExpenses);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            data = GetFCNActivity().FirstOrDefault((p) => p.ID == id);
            return Ok(data);
        }

        // DELETE: api/FCNActivity/5
        [HttpDelete("{id}")]
        public ActionResult DeleteFCNActivity(int id)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlDelete = "exec FCN..del_fcn_activity @ID";
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
