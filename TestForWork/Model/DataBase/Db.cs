
using System.Configuration;
using System.Data.SqlClient;


namespace TestForWork.Model.DataBase
{
    public class DatabaseCreator
    {
        private string MasterConnectionString { get; set; }
        private string EmployeeConnectionString { get; set; }


        public  DatabaseCreator()
        {
            MasterConnectionString = ConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
            EmployeeConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;
            InitializeAsync();

        }
         public async Task InitializeAsync()
        {
            await CreateDatabaseAsync();
        }


        public async Task CreateDatabaseAsync()
        {
            string namebase = GetDatabaseName();
            Console.WriteLine(namebase);

            using (SqlConnection connection = new SqlConnection(MasterConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand();
                    command.CommandText =
                        "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = @namebase) CREATE DATABASE [" +
                        namebase + "]";
                    command.Parameters.AddWithValue("@namebase", namebase);
                    command.Connection = connection; 
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Exception: " + ex.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        await connection.CloseAsync();
                    }
                }
            }
        }
       


        private string GetDatabaseName()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(EmployeeConnectionString);
            return builder.InitialCatalog;
        }
    }
}
