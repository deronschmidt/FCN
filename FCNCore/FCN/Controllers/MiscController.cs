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
            //Get connection string - to be replaced with different credentials later
            string fcnConnectionString = Utilities.GetDBConnectionString();

            List<ServiceSubcategoryData> serviceSubcategories = new List<ServiceSubcategoryData>();
            string sqlSelect = "exec FCN..sel_active_service_subcategories @categoryID";

            using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
            {
                fcnDBConnection.Open();
                using (SqlCommand cmdInsert = new SqlCommand(sqlSelect, fcnDBConnection))
                {
                    cmdInsert.Parameters.AddWithValue("@subcategory_name", id);
                    using (SqlDataReader reader = cmdInsert.ExecuteReader())
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
                }
            }
            return serviceSubcategories;
        }

        // GET: api/GetActiveMemberRoles
        [Route("api/GetActiveMemberRoles")]
        [HttpGet]
        public IEnumerable<MemberRoleData> GetActiveMemberRoles()
        {
            List<MemberRoleData> memberRoles = new List<MemberRoleData>();
            string sqlSelect = "exec FCN..sel_active_member_roles";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    MemberRoleData newMemberRole = new MemberRoleData
                    {
                        ID = (int)reader["ID"],
                        RoleType = reader["role_type"].ToString(),
                        Description = reader["description"].ToString(),
                        Active = reader["active"].ToString() == "1" ? true : false,
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"]
                    };
                    memberRoles.Add(newMemberRole);
                }
            }
            return memberRoles;
        }

        // GET: api/GetActiveCommunities
        [Route("api/GetActiveCommunities")]
        [HttpGet]
        public IEnumerable<CommunityData> GetActiveCommunities()
        {
            List<CommunityData> communities = new List<CommunityData>();
            string sqlSelect = "exec sel_active_communities";
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

                    //Get connection string - to be replaced with different credentials later
                    string fcnConnectionString = Utilities.GetDBConnectionString();
                    string sqlSelectContacts = "exec sel_community_contact_by_community @communityID";
                    using (SqlConnection fcnDBConnection = new SqlConnection(fcnConnectionString))
                    {
                        fcnDBConnection.Open();
                        using (SqlCommand cmdInsert = new SqlCommand(sqlSelectContacts, fcnDBConnection))
                        {
                            cmdInsert.Parameters.AddWithValue("@communityID", newCommunity.ID);
                            using (SqlDataReader readerContacts = cmdInsert.ExecuteReader())
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
    }
}