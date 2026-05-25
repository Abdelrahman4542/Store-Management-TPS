using Microsoft.Data.SqlClient;
using StoreManagementSystem.Helpers;

namespace StoreManagementSystem.Services
{
    public class AuthService
    {
        public bool TestConnection()
        {
            try
            {
                using SqlConnection connection = DbConnection.GetConnection();

            connection.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
