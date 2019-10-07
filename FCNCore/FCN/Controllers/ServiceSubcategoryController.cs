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
    public class ServiceSubcategoryController : ControllerBase
    {
        // GET: api/ServiceSubcategory
        [HttpGet]
        public IEnumerable<ServiceSubcategoryData> GetServiceSubcategory()
        {
            List<ServiceSubcategoryData> serviceSubcategories = new List<ServiceSubcategoryData>();
            string sqlSelect = "exec sel_service_subcategory";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    string updateFirstName = string.Empty;
                    string updateLastName = string.Empty;
                    ServiceSubcategoryData newServiceSubcategory = new ServiceSubcategoryData
                    {
                        ID = (int)reader["ID"],
                        SubCategoryName = reader["subcategory_name"].ToString(),
                        CategoryID = (int)reader["service_categoryID"],
                        CategoryName = reader["category_name"].ToString(),
                        Description = reader["description"].ToString(),
                        Active = reader["active"].ToString() == "1" ? true : false,
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };
                    serviceSubcategories.Add(newServiceSubcategory);
                }
            }
            return serviceSubcategories;
        }

        // GET: api/ServiceSubcategory/5
        [HttpGet("{id}")]
        public ActionResult<ServiceSubcategoryData> GetServiceSubcategory(int id)
        {
            ServiceSubcategoryData data = GetServiceSubcategory().FirstOrDefault((p) => p.ID == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // POST: api/ServiceSubcategory
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<ServiceSubcategoryData> PostServiceSubcategory([FromBody] ServiceSubcategoryData data)
        {
            int id = 0;
            int isActive = (data.Active.ToString() == "True" ? 1 : 0);

            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlInsert = "exec FCN..ins_service_subcategory @subcategory_name, @service_categoryID, @description, @active";

            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, fcnDBConnection))
                {
                    cmdInsert.Parameters.AddWithValue("@subcategory_name", data.SubCategoryName);
                    cmdInsert.Parameters.AddWithValue("@service_categoryID", data.CategoryID);
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

            data = GetServiceSubcategory().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetServiceSubcategory", new { id }, data);
        }

        // PUT: api/ServiceSubcategory/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<ServiceSubcategoryData> PutServiceSubcategory(int id, [FromBody] ServiceSubcategoryData data)
        {
            int isActive = (data.Active.ToString() == "True" ? 1 : 0);
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlUpdate = "exec FCN..upd_service_subcategory @ID, @subcategory_name, @service_categoryID, @description, @active";
            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, fcnDBConnection))
                {
                    cmdUpdate.Parameters.AddWithValue("@ID", id);
                    cmdUpdate.Parameters.AddWithValue("@subcategory_name", data.SubCategoryName);
                    cmdUpdate.Parameters.AddWithValue("@service_categoryID", data.CategoryID);
                    cmdUpdate.Parameters.AddWithValue("@description", data.Description);
                    cmdUpdate.Parameters.AddWithValue("@active", isActive);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            data = GetServiceSubcategory().FirstOrDefault((p) => p.ID == id);                
            return Ok(data);
        }

        // DELETE: api/ServiceSubcategory/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteServiceSubcategory(int id)
        {
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            string sqlDelete = "exec FCN..del_service_subcategory @ID";
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
