namespace TestForWork.View
{
    public interface IMainFormView
    {
        event EventHandler ListEmployeesClicked;
        event EventHandler StatEmployeesClick;
        event EventHandler DateRangeChanged;
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}

