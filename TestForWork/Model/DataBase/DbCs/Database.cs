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

        /// <summary>
        /// Инициализация на создание базы данных и таблиц.
        /// </summary>
        public async Task InitializeAsync()
        {
            await CreateDatabaseAsync();
            await RunProcedureAsync();
        }

        /// <summary>
        /// Создание базы данных.
        /// </summary>
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

        /// <summary>
        /// Нахождение пути к файлу с процедурой.
        /// </summary>
        /// <param name="fileName">Имя файла с процедурой.</param>
        /// <returns>Полный путь к файлу с процедурой.</returns>
        private string GetProcedurePath(string fileName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine(basePath, "..", "..", "..", "Model", "DataBase", "Procedures");
            return Path.GetFullPath(Path.Combine(relativePath, fileName));
        }

        /// <summary>
        /// Отправка на запуск процедур.
        /// </summary>
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
                    "Statuses.sql",
                    "StatEmplyeesByData.sql"
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

        /// <summary>
        /// Метод запуска запросов для создания процедур из файла через путь.
        /// </summary>
        /// <param name="path">Путь к файлу с процедурой.</param>
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

        /// <summary>
        /// Получение названия базы данных.
        /// </summary>
        /// <returns>Название базы данных.</returns>
        private string GetDatabaseName()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(EmployeeConnectionString);
            return builder.InitialCatalog;
        }

        /// <summary>
        /// Вытаскивание строк из базы данных списка сотрудников.
        /// </summary>
        /// <param name="procedure">Имя хранимой процедуры.</param>
        /// <returns>Список сотрудников.</returns>
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

        /// <summary>
        /// Чтение из базы данных статусов.
        /// </summary>
        /// <param name="procedure">Имя хранимой процедуры.</param>
        /// <returns>Список статусов.</returns>
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

        /// <summary>
        /// Метод получения списка сотрудников.
        /// </summary>
        /// <returns>Список сотрудников.</returns>
        public async Task<List<Employee>> ListEmployees()
        {
            string procedure = "GetEmployeeData";
            List<Employee> ListOfEmployee = new List<Employee>(await ReadFromBdListEmplyee(procedure));
            
            return ListOfEmployee;
        }

        /// <summary>
        /// Метод получения количества сотрудников по дням.
        /// </summary>
        /// <param name="procedure">Имя хранимой процедуры.</param>
        /// <returns>Словарь с датами и количеством сотрудников.</returns>
       
        

        /// <summary>
        /// Метод получения статусов.
        /// </summary>
        /// <returns>Список статусов.</returns>
        public async Task<List<string>> ListStatus()
        {
            string procedure = "Statuses";
            List<string> test =await ReadFromBdStatus(procedure);
            return test;
        }
        private async Task<Dictionary<DateTime,int>> GetFromBdStatusesDay(string procedure,string status, DateTime startDate, DateTime endDate)
        {
            Dictionary<DateTime, int> statuses = new Dictionary<DateTime, int>();
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
                                string statusdb = reader.GetString(0);
                                if (status == statusdb)
                                {
                                    DateTime data1 = reader.GetDateTime(1);
                                    DateTime? data2 = null;
                                    if (!reader.IsDBNull(2))
                                    {
                                        data2 = reader.GetDateTime(2);
                                    }
                                    if ((data1 >= startDate && data1 <= endDate) || (data2.HasValue && data2.Value >= startDate && data2.Value <= endDate))
                                    {
                                        // Определяем дату, которая будет использоваться для агрегации (data1 или data2, если data2 не null)
                                        DateTime dateToUse = data2 ?? data1.Date;

                                        // Увеличиваем счетчик для данной даты
                                        if (statuses.ContainsKey(dateToUse))
                                        {
                                            statuses[dateToUse]++;
                                        }
                                        else
                                        {
                                            statuses[dateToUse] = 1;
                                        }
                                    }
                                }
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

        /// <summary>
        /// Метод получения количества сотрудников по дням.
        /// </summary>
        /// <returns>Словарь с датами и количеством сотрудников.</returns>
        public async Task<Dictionary<DateTime, int>> EmployeesByDay(string status, DateTime startDate, DateTime endDate)
        {
            string procedure = "StatEmplyeesByData";
            Dictionary<DateTime, int> employeesByDay = await GetFromBdStatusesDay(procedure,status,startDate,endDate);
            return employeesByDay;
        }
    }
}
