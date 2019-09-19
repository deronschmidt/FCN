using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FCN.Models;
using FCNHelpers;
using Microsoft.AspNetCore.Mvc;

namespace FCN.Controllers
{

    [ApiController]
    public class MiscController : ControllerBase
    {
        // GET: api/GetActiveServiceCategories
        [Route("api/GetActiveServiceCategories")]
        [HttpGet]
        public IEnumerable<ServiceCategoryData> GetActiveServiceCategories()
        {
            List<ServiceCategoryData> serviceCategories = new List<ServiceCategoryData>();
            string sqlSelect = "exec FCN..sel_active_service_categories";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    string updateFirstName = string.Empty;
                    string updateLastName = string.Empty;
                    ServiceCategoryData newActiveCategory = new ServiceCategoryData
                    {
                        ID = (int)reader["ID"],
                        CategoryName = reader["category_name"].ToString(),
                        Description = reader["description"].ToString(),
                        Active = reader["active"].ToString() == "1" ? true : false,
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };
                    serviceCategories.Add(newActiveCategory);
                }
            }
            return serviceCategories;
        }

        // GET: api/GetActiveServiceSubcategories
        [Route("api/GetActiveServiceSubcategories/{id}")]
        [HttpGet]
        public IEnumerable<ServiceSubcategoryData> GetActiveServiceSubcategories(int id)
        {
            List<ServiceSubcategoryData> serviceSubcategories = new List<ServiceSubcategoryData>();
            string sqlSelect = string.Format("exec FCN..sel_active_service_subcategories {0}", id);
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    string updateFirstName = string.Empty;
                    string updateLastName = string.Empty;
                    ServiceSubcategoryData newSubCategory = new ServiceSubcategoryData
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
                    serviceSubcategories.Add(newSubCategory);
                }
            }
            return serviceSubcategories;
        }       
    }
}