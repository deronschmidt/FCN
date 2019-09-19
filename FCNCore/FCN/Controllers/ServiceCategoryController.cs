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

            string sqlInsert = string.Format("exec FCN..ins_service_category '{0}','{1}',{2}",
                            data.CategoryName.Replace("'", "''"), data.Description.Replace("'", "''"),
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

            data = GetServiceCategory().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetServiceCategory", new { id }, data);
        }

        // PUT: api/ServiceCategory/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<ServiceCategoryData> PutServiceCategory(int id, [FromBody] ServiceCategoryData data)
        {
            string sqlUpdate = string.Format("exec FCN..upd_service_category {0},'{1}','{2}',{3}",
                     id, data.CategoryName.Replace("'", "''"), data.Description.Replace("'", "''"),
                     data.Active.ToString() == "True" ? 1 : 0);
            bool cmdStatus = Utilities.ExecNonQuery(sqlUpdate);
            if (cmdStatus)
            {
                data = GetServiceCategory().FirstOrDefault((p) => p.ID == id);
                return Ok(data);
            }
            else
                return BadRequest();
        }

        // DELETE: api/ServiceCategory/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteServiceCategory(int id)
        {
            string sqlDelete = string.Format("exec FCN..del_service_category {0}", id);
            bool cmdStatus = Utilities.ExecNonQuery(sqlDelete);
            if (cmdStatus)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
