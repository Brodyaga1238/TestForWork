namespace TestForWork.Model.DataBase.DbCs
{
    public interface IDb
    {
        Task<List<Employee>>  ListEmployees();
    }
}
