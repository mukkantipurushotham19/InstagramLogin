using System.Data;
using System.Data.SqlClient;

namespace Instagram.Models
{
    public class LoginCredentialDBDataContext
    {
        private readonly string _connectionString;

        public LoginCredentialDBDataContext(string connectionString) {
            _connectionString=connectionString;
        }

        //Fetching data from database
        public List<LoginCredentials> CheckCrential()
        {
            List<LoginCredentials> list= new List<LoginCredentials>();


            SqlConnection con = new SqlConnection(_connectionString);
            string query = "CheckCrentials";
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                LoginCredentials obj=new LoginCredentials();
                obj.UserName = reader["UserName"].ToString();
                obj.Password = reader["Password"].ToString();

                list.Add(obj);
            }
           
            con.Close();

            return list;
        }

        //Inserting the data into the database
        public bool CreateAccount(LoginCredentials obj)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            string query = "InsertData";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", obj.UserName);
            cmd.Parameters.AddWithValue("@Password", obj.Password);
            cmd.Parameters.AddWithValue("@MobileNumber", obj.MobileNumber);
            cmd.Parameters.AddWithValue("@FullName", obj.FullName);

            con.Open();
            int k= cmd.ExecuteNonQuery();
            con.Close();
            if (k != 0)
            {
                return true;
            }
            else return false;
        }


    }
}
