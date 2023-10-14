using System.Data;
using System.Data.SqlClient;

namespace BackendApiBarriles.DAL.Accounts
{
    public class AccountsDAL
    {
        private IConfiguration _configuration;

        public AccountsDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("BDDQA");
        }
        public string[] insertNewUser(string user, string passwordEncrypted, string email, int numCellphone)
        {
            string[] res = {};
            try
            {                
                if (userDuplicated(user))
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("sp_Accounts_InsertarUsuario", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Username", user);
                            command.Parameters.AddWithValue("@Password", passwordEncrypted);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@NumCellphone", numCellphone);
                            command.Parameters.AddWithValue("@Vigente", 1);
                            command.Parameters.AddWithValue("@Permisos", 1);
                            command.Parameters.AddWithValue("@GlosaPermisos", "User");

                            command.ExecuteNonQuery();

                            res = new string[]{
                            "cod_insert: 00",
                            "glosa_insert: success!"
                        };
                        }
                        connection.Close();
                    }
                }
                else
                {
                    res = new string[]{
                    "cod_insert: 01",
                    "glosa_insert: Usuario duplicado"
                };
                }                
            }
            catch (Exception ex)
            {
                res = new string[]{
                    "cod_insert: 500",
                    "glosa_insert: "+ex.StackTrace
                };
            }
            return res;
        }
        public bool userDuplicated(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_Accounts_ValidaUsername", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;                        
                        command.Parameters.AddWithValue("@Username", username);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        
                        if (dataTable.Rows.Count > 0)
                        {
                            connection.Close();
                            return false;                            
                        }
                        else
                        {
                            connection.Close();
                            return true;                            
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool validateUserPassword(string username, string passwordEncripted)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_Accounts_validateuserpass", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@user", username);
                        command.Parameters.AddWithValue("@passwordEncripted", passwordEncripted);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        connection.Close();
                        return dataTable.Rows.Count > 0;                        
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
