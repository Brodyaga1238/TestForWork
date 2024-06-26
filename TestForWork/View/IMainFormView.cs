using TestForWork.Model.DataBase.DbCs;

namespace TestForWork.View
{
    public interface IMainFormView
    {
        event EventHandler ListEmployeesClicked;
        event EventHandler StatEmployeesClick;
        event EventHandler ApplyButtonClicked;
        string SelectedStatus { get; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        void DisplayEmployees(List<Employee> employees);
        void AddStatuses(List<string> statuses);
        void DisplayStatusCount(Dictionary<DateTime, int> statcount);
    }
}

