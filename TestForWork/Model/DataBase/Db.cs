
using System.Configuration;
using System.Data.SqlClient;

namespace TestForWork.Model.DataBase
{
    public class DatabaseCreator
    {
        private string MasterConnectionString { get; set; }
        private string EmployeeConnectionString { get; set; }
        
        public DatabaseCreator()
        {
            MasterConnectionString = ConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
            EmployeeConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;
            InitializeAsync();

        }
        //Запуск инициализации на создание бд и таблиц
         public async Task InitializeAsync()
        {
            await CreateDatabaseAsync();
            await RunProcedureAsync();
        }

        //создание бд 
        public async Task CreateDatabaseAsync()
        {
            string namebase = GetDatabaseName();

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
        //Методнахождения пути и отправки на запуск процедур
        private async Task RunProcedureAsync()
        {
            try
            {
                List<string> listprocedure = new List<string>()
                {
                    "CreateTableDepsTableProcedure.sql",
                    "CreateTablePersonsProcedure.sql",
                    "CreateTablePostProcedure.sql",
                    "CreateTableStatusProcedure.sql"
                };
                
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = Path.Combine(basePath, "..", "..", "..", "Model", "DataBase", "Procedures");
                string GetFullPath(string fileName)
                {
                    return Path.GetFullPath(Path.Combine(relativePath, fileName));
                }
                    
                foreach (var c in listprocedure)
                { 
                    string scriptPath = GetFullPath(c); 
                    await AddTables(scriptPath);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Exception: " + ex.Message);
            }
        }
        //Метод запуска процедур из файла через путь
        private async Task AddTables(string path)
        {
            try
            {
                string script = await File.ReadAllTextAsync(path);
                using (SqlConnection connection = new SqlConnection(MasterConnectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(script, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Exception: " + ex.Message);
            }
            
        }
        // получение названия бд
        private string GetDatabaseName()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(EmployeeConnectionString);
            return builder.InitialCatalog;
        }
    }
}
