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

            string sqlInsert = string.Format("exec FCN..ins_service_subcategory '{0}',{1},'{2}',{3}",
                               data.SubCategoryName.Replace("'", "''"), data.CategoryID, 
                               data.Description.Replace("'", "''"), data.Active.ToString() == "True" ? 1 : 0);

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

            data = GetServiceSubcategory().FirstOrDefault((p) => p.ID == id);
            return CreatedAtAction("GetServiceSubcategory", new { id }, data);
        }

        // PUT: api/ServiceSubcategory/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public ActionResult<ServiceSubcategoryData> PutServiceSubcategory(int id, [FromBody] ServiceSubcategoryData data)
        {
            string sqlUpdate = string.Format("exec FCN..upd_service_subcategory {0},'{1}',{2},'{3}',{4}",
                    id, data.SubCategoryName.Replace("'", "''"), data.CategoryID, data.Description.Replace("'", "''"),
                    data.Active.ToString() == "True" ? 1 : 0);
            bool cmdStatus = Utilities.ExecNonQuery(sqlUpdate);
            if (cmdStatus)
            {
                data = GetServiceSubcategory().FirstOrDefault((p) => p.ID == id);
                return Ok(data);
            }
            else
                return BadRequest();
        }

        // DELETE: api/ServiceSubcategory/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteServiceSubcategory(int id)
        {
            string sqlDelete = string.Format("exec FCN..del_service_subcategory {0}", id);
            bool cmdStatus = Utilities.ExecNonQuery(sqlDelete);
            if (cmdStatus)
                return NoContent();
            else
                return BadRequest();
        }
    }
}
