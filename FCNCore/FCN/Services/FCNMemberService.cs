using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FCN.Helpers;
using FCN.Models;
using System.Data.SqlClient;
using FCNHelpers;

namespace FCN.Services
{
    public interface IFCNMemberService
    {
        FCNMemberData Authenticate(string username, string password);
        IEnumerable<FCNMemberData> GetFCNMember();
        FCNMemberData GetFCNMemberByID(int id);
        FCNMemberData Create(FCNMemberData member, string password);
        bool Update(FCNMemberData member, string password = null);
        bool Delete(int id);
    }
    
    public class FCNMemberService : IFCNMemberService
    {
        private List<FCNMemberData> _fcnMembers = new List<FCNMemberData>();

        private void LoadAllUsers()
        {
            _fcnMembers.Clear();
            string sqlSelect = "exec sel_fcn_member";
            using (SqlDataReader reader = Utilities.ExecQuery(sqlSelect))
            {
                while (reader.Read())
                {
                    string updateFirstName = string.Empty;
                    string updateLastName = string.Empty;
                    FCNMemberData newFCNMember = new FCNMemberData
                    {
                        ID = (int)reader["ID"],
                        RoleID = (int)reader["roleID"],
                        RoleType = reader["role_type"].ToString(),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Address1 = reader["address1"].ToString(),
                        Address2 = reader["address2"].ToString(),
                        City = reader["city"].ToString(),
                        State = reader["state"].ToString(),
                        ZipCode = reader["zip_code"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        AlternatePhone = reader["alt_phone"].ToString(),
                        CommunityID = (int)reader["communityID"],
                        CommunityName = reader["community_name"].ToString(),
                        Licensed = reader["licensed"].ToString() == "1" ? true : false,
                        Active = reader["active"].ToString() == "1" ? true : false,
                        ActiveDate = (DateTime)reader["active_date"],
                        Administrator = reader["administrator"].ToString() == "1" ? true : false,
                        CreatedDate = (DateTime)reader["created_date"],
                        UpdatedDate = (DateTime)reader["updated_date"],
                        Password = reader["password"].ToString(),
                        //Token = reader["token"].ToString()
                    };

                    if (reader["inactive_date"] != DBNull.Value)
                    {
                        newFCNMember.InactiveDate = (DateTime)reader["inactive_date"];
                    }
                    _fcnMembers.Add(newFCNMember);
                }
            }
        }

        private readonly AppSettings _appSettings;

        public FCNMemberService(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }

        public FCNMemberData Authenticate(string username, string password)
        {
            LoadAllUsers();
            string role = "User";

            string eng = Encryption.Encrypt(password);
            var member = _fcnMembers.SingleOrDefault(x => x.Email == username && Encryption.Decrypt(x.Password) == password);

            // return null if user not found
            if (member == null)
                return null;

            if (member.Administrator)
            {
                role = "Admin";
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, member.ID.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            member.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            member.Password = null;

            return member;
        }

        public IEnumerable<FCNMemberData> GetFCNMember()
        {
            LoadAllUsers();
            return _fcnMembers.Select(x => {
                x.Password = null;
                return x;
            });
        }

        public FCNMemberData GetFCNMemberByID(int id)
        {
            LoadAllUsers();
            var member = _fcnMembers.FirstOrDefault(x => x.ID == id);

            // return user without password
            if (member != null)
                member.Password = null;

            return member;
        }

        public FCNMemberData Create(FCNMemberData data, string password)
        {
            int id = 0;

            FCNMemberData newMember = null;
            LoadAllUsers();
            // email has changed so check if the new username is already taken
            if (_fcnMembers.Any(x => x.Email == data.Email))
                throw new AppException("Email " + data.Email + " is already taken");

            string encryptedPassword = Encryption.Encrypt(password);
            string sqlInsert = string.Format("exec FCN..ins_fcn_member {0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}'" +
                                        ",{10},{11},{12},{13},'{14}',{15},{16},'{17}'",
                                    data.RoleID, data.FirstName.Replace("'", "''"), data.LastName.Replace("'", "''"), data.Address1.Replace("'", "''"),
                                    Utilities.ToDBNull(data.Address2), data.City.Replace("'", "''"), data.State, data.ZipCode, data.Email.Replace("'", "''"),
                                    data.Phone.Replace("'", "''"), Utilities.ToDBNull(data.AlternatePhone), data.CommunityID, data.Licensed.ToString() == "True" ? 1 : 0,
                                    data.Active.ToString() == "True" ? 1 : 0, data.ActiveDate, Utilities.ToDBNull(data.InactiveDate), data.Administrator.ToString() == "True" ? 1 : 0,
                                    encryptedPassword);

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
                        return newMember;
                    }
                }
            }
            newMember = GetFCNMemberByID(id);

            return newMember;
        }

        public bool Update(FCNMemberData data, string password = null)
        {
            FCNMemberData originalMember = GetFCNMemberByID(data.ID);
            if (originalMember == null)
                throw new AppException("User not found");
            bool cmdStatus = false;
            if (data.Email != originalMember.Email)
            {
                // email has changed so check if the new username is already taken
                if (_fcnMembers.Any(x => x.Email == data.Email))
                    throw new AppException("Email " + data.Email + " is already taken");
            }
            string sqlUpdate = string.Format("exec FCN..upd_fcn_member {0},{1},'{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}'" +
                                    ",'{10}',{11},{12},{13},{14},'{15}',{16},{17}",
                                    data.ID, data.RoleID, data.FirstName.Replace("'", "''"), data.LastName.Replace("'", "''"), data.Address1.Replace("'", "''"),
                                    Utilities.ToDBNull(data.Address2), data.City.Replace("'", "''"), data.State, data.ZipCode, data.Email.Replace("'", "''"),
                                    data.Phone.Replace("'", "''"), Utilities.ToDBNull(data.AlternatePhone), data.CommunityID, data.Licensed.ToString() == "True" ? 1 : 0,
                                    data.Active.ToString() == "True" ? 1 : 0, data.ActiveDate, Utilities.ToDBNull(data.InactiveDate), data.Administrator.ToString() == "True" ? 1 : 0);
            cmdStatus = Utilities.ExecNonQuery(sqlUpdate);
            return cmdStatus;
        }

        public bool Delete(int id)
        {
            bool cmdStatus = false;
            FCNMemberData member = GetFCNMemberByID(id);
            if (member != null)
            {
                string sqlDelete = string.Format("exec FCN..del_fcn_member {0}", id);
                cmdStatus = Utilities.ExecNonQuery(sqlDelete);
            }
            return cmdStatus;
        }
    }
}
