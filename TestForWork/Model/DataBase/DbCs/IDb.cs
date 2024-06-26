namespace TestForWork.Model.DataBase.DbCs
{
    public interface IDb
    {
        Task<List<Employee>>  ListEmployees();
        Task<List<string>> ListStatus();
        Task<Dictionary<DateTime, int>> EmployeesByDay(string status, DateTime startDate, DateTime endDateT);
    }
}
