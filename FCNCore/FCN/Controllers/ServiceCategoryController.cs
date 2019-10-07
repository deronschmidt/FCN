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
    public class ServiceCategoryController : ControllerBase
    {
        // GET: api/ServiceCategory
        [HttpGet]
        public IEnumerable<ServiceCategoryData> GetServiceCategory()
        {
            List<ServiceCategoryData> serviceCategories = new List<ServiceCategoryData>();
            string sqlSelect = "exec sel_service_category";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    string updateFirstName = string.Empty;
                    string updateLastName = string.Empty;
                    ServiceCategoryData newMemberRole = new ServiceCategoryData
                    {
                        ID = (int)reader["ID"],
                        CategoryName = reader["category_name"].ToString(),
                        Description = reader["description"].ToString(),
                        Active = reader["active"].ToString() == "1" ? true : false,
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };
                    serviceCategories.Add(newMemberRole);
                }
            }
            return serviceCategories;
        }

        // GET: api/ServiceCategory/5
        [HttpGet("{id}")]
        public ActionResult<ServiceCategoryData> GetServiceCategory(int id)
        {
            ServiceCategoryData data = GetServiceCategory().FirstOrDefault((p) => p.ID == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST: api/ServiceCategory
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<ServiceCategoryData> PostServiceCategory([FromBody] ServiceCategoryData data)
        {
            int id = 0;

            int isActive = (data.Active.ToString() == "True" ? 1 : 0);

            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlInsert = "exec FCN..ins_service_category @category_name, @description, @active";

            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, fcnDBConnection))
                {
                    cmdInsert.Parameters.AddWithValue("@category_name", data.CategoryName);
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

            data = GetServiceCategory().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetServiceCategory", new { id }, data);
        }

        // PUT: api/ServiceCategory/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<ServiceCategoryData> PutServiceCategory(int id, [FromBody] ServiceCategoryData data)
        {
            int isActive = (data.Active.ToString() == "True" ? 1 : 0);
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlUpdate = "exec FCN..upd_service_category @ID, @category_name, @description, @active";
            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, fcnDBConnection))
                {
                    cmdUpdate.Parameters.AddWithValue("@ID", id);
                    cmdUpdate.Parameters.AddWithValue("@category_name", data.CategoryName);
                    cmdUpdate.Parameters.AddWithValue("@description", data.Description);
                    cmdUpdate.Parameters.AddWithValue("@active", isActive);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            data = GetServiceCategory().FirstOrDefault((p) => p.ID == id);
            return Ok(data);
        }

        // DELETE: api/ServiceCategory/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteServiceCategory(int id)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlDelete = "exec FCN..del_service_category @ID";
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
