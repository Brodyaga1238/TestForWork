using System.Configuration;

using System.Data.SqlClient;

namespace TestForWork.Model.DataBase.DbCs
{
    public class Database: IDb
    {
        private string MasterConnectionString { get; set; }
        private string EmployeeConnectionString { get; set; }
        
        public Database()
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
                    "CreateTableStatusProcedure.sql",
                    "ListEmployess.sql",
                    "StatEmplyees.sql"
                };
                string path;
                foreach (var procedureFileName  in listprocedure)
                {
                    path = GetProcedurePath(procedureFileName);
                    await CreateProc(path);
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
        //Метод запуска запросов для создания процедур из файла через путь
        private async Task CreateProc(string path)
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
        //вытаскивание строк из бд с добавлением с спиок

        private async Task<List<Employee>> ReadFromBdListEmplyee(string procedure)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection connection = new SqlConnection(EmployeeConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(procedure, connection);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows) 
                        {
                            while (await reader.ReadAsync()) 
                            {
                                
                                string fname = reader.GetString(0);
                                string sname = reader.GetString(1);
                                string lname = reader.GetString(2);
                                string status = reader.GetString(3);
                                string dep = reader.GetString(4);
                                string post = reader.GetString(5);
                                DateTime de = reader.GetDateTime(6);
                                DateTime? du = null;
                                if (!reader.IsDBNull(7))
                                {
                                    du = reader.GetDateTime(7);
                                }
                                Employee employee = new Employee(fname, sname, lname, de, du, status, dep, post);
                                employees.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return employees;
        }
        private async Task<List<string>> ReadFromBdStatus(string procedure)
        {
            List<string> statuses = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(EmployeeConnectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(procedure, connection);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows) 
                        {
                            while (await reader.ReadAsync())
                            {
                                string status = reader.GetString(0);
                                statuses.Add(status);
                            }
                        }
                    }
                }

                return statuses;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //метод получения списка сотрудников
        public async Task<List<Employee>> ListEmployees()
        {
            string procedure = "GetEmployeeData";
            List<Employee> ListOfEmployee = new List<Employee>(await ReadFromBdListEmplyee(procedure));
            
            return ListOfEmployee;
        }

        public async Task<List<string>> ListStatus()
        {
            string procedure = "GetStatData";
            List<string> test =await ReadFromBdStatus(procedure);
            return test;
        }
        
    }
}
