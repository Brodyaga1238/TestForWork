using System.Configuration;
using System.Data.SqlClient;

namespace TestForWork.Model.DataBase.DbCs
{
    public class DatabaseCreator: IDb
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
                    using (SqlCommand command = new SqlCommand())
                    {
                        await connection.OpenAsync();
                        command.CommandText =
                            "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = @namebase) CREATE DATABASE [" +
                            namebase + "]";
                        command.Parameters.AddWithValue("@namebase", namebase);
                        command.Connection = connection; 
                        await command.ExecuteNonQueryAsync();
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
            
        }
        // нахождение пути
        private string GetProcedurePath(string fileName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "..", "..", "..", "Model", "DataBase", "Procedures");
            return Path.GetFullPath(Path.Combine(relativePath, fileName));
        }

        
        //отправка на запуск процедур
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
                    
                foreach (var procedureFileName  in listprocedure)
                { 
                    string scriptPath = GetProcedurePath(procedureFileName ); 
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

        private async Task ReadFromBd(string path)
        {
           
        }

        public async Task ListEmployees()
        {
            string test = GetProcedurePath("ListEmployess.sql");
            await ReadFromBd(test);
        }
        
    }
}
