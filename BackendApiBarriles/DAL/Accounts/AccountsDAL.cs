using System.Data;
using System.Data.SqlClient;

namespace BackendApiBarriles.DAL.Accounts
{
    public class AccountsDAL
    {
        string connectionString = "Data Source=LAPTOP-LIPHGIDV\\SQLEXPRESS;Initial Catalog=BarrilAppDB;Integrated Security=True";         
        public string[] insertNewUser(string user, string passwordEncrypted, string email, int numCellphone)
        {
            string[] res = {};
            try
            {
                if (userDuplicated(user))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_Accounts_ValidaUsername", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Definir los parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@Username", username);

                        // Crear un adaptador de datos para ejecutar el procedimiento almacenado
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        // Llenar el DataTable con los resultados del procedimiento almacenado
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
    }
}
