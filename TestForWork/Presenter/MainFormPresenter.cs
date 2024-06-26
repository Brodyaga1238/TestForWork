
using TestForWork.Model.DataBase.DbCs;
using TestForWork.View;

namespace TestForWork.Presenter
{
    public class MainFormPresenter : IMainFormPresenter
    {
        private readonly IMainFormView _view;
        private readonly IDb _db;
        public MainFormPresenter(IMainFormView view, IDb db)
        {
            _view = view;
            _view.ListEmployeesClicked += ListEmployeesClicked;
            _view.StatEmployeesClick += StatEmployeesClick;
            _view.ApplyButtonClicked += ApllyButtonClicked;
            _db = db;

        }

       
        /// <summary>Логика обработки нажатия кнопки "Список сотрудников".
        /// </summary>
        public async void ListEmployeesClicked(object sender, EventArgs e)
        {
            List<Employee>  listEmployees = await _db.ListEmployees();
            _view.DisplayEmployees(listEmployees);
           
        }
        
        /// <summary>Логика обработки нажатия кнопки "Статистика сотрудников".
        /// </summary>
        public async void StatEmployeesClick(object sender, EventArgs e)
        {
            List<string> statuses = await _db.ListStatus();
            _view.AddStatuses(statuses);
        }
        
      
        /// <summary>Логика обработки кнопки "Применить".
        /// </summary>
        public async void ApllyButtonClicked(object sender, EventArgs e)
        {
            string status = _view.SelectedStatus;
            DateTime start = _view.StartDate;
            DateTime end = _view.EndDate;
            Dictionary<DateTime, int> test = await _db.EmployeesByDay(status, start, end);
            _view.DisplayStatusCount(test);
        }
    }
}

