using TestForWork.Model.DataBase.DbCs;

namespace TestForWork.View
{
    public interface IMainFormView
    {
        event EventHandler ListEmployeesClicked;
        event EventHandler StatEmployeesClick;
        event EventHandler DateRangeChanged;
        event EventHandler ApplyButtonClicked;
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        void DisplayEmployees(List<Employee> employees);
        void AddStatuses(List<string> statuses);
    }
}

