using System;
using System.Data;
using System.Data.SqlClient;

namespace FCNHelpers
{
    public class Utilities
    {
        public static string ToDBNull(object value)
        {
            if (null != value)
            {
                string objectType = value.GetType().ToString();
                if (objectType == "System.String")
                    return "'" + value.ToString().Replace("'", "''") + "'";
                else if (objectType == "System.DateTime")
                    return "'" + value + "'";
                else
                    return value.ToString();
            }
            else
                return "NULL";
        }

        public static string Env()
        {
            string env = Environment.GetEnvironmentVariable("XXX");

            env = !string.IsNullOrEmpty(env) ? env.ToUpper().Trim() : "DEVELOPMENT";

            return env;
        }

        public static bool ExecNonQuery(string inSQL)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    // Create the connectionString  
                    // Trusted_Connection is used to denote the connection uses Windows Authentication  
                    conn.ConnectionString = "Server=SCHMIDT-DEN;Database=FCN;" +
                        "Trusted_Connection=true;Connection timeout=30;User Id=****;" +
                        "Password=****; ";
                    conn.Open();

                    string sqlNonQuery = inSQL;
                    using (SqlCommand cmdNonQuery = new SqlCommand(sqlNonQuery, conn))
                    {
                        cmdNonQuery.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch(Exception)
            {
                //throw ex;
                return false;
            }
        }

        public static SqlDataReader ExecQuery(string inSQL)
        {
            try
            {
                string connectionString = "Server=SCHMIDT-DEN;Database=FCN;" +
                        "Trusted_Connection=true;Connection timeout=30;User Id=****;" +
                        "Password=****; ";
                SqlConnection conn = new SqlConnection(connectionString);
                using (SqlCommand cmd = new SqlCommand(inSQL, conn))
                {
                    conn.Open();
                    // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                    // IDataReader is closed.  
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    return reader;
                }
            }
            catch(Exception)
            {
                SqlDataReader sqlDataReader = null;
                return sqlDataReader;
            }
        }
    }
}
