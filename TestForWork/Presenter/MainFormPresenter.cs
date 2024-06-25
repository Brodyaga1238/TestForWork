
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
            _view.DateRangeChanged += DateRangeChanged;
            _view.ListEmployeesClicked += ListEmployeesClicked;
            _view.StatEmployeesClick += StatEmployeesClick;
            _view.ApplyButtonClicked += ApllyButtonClicked;
            _db = db;

        }

        // Логика обработки нажатия кнопки "Список сотрудников"
        public async void ListEmployeesClicked(object sender, EventArgs e)
        {
            List<Employee> Test= await _db.ListEmployees();
            
        }

        // Логика обработки нажатия кнопки "Статистика сотрудников"
        public void StatEmployeesClick(object sender, EventArgs e)
        {

        }
        // Логика обработки изменения промежутка дат
        public void DateRangeChanged(object sender, EventArgs e)
        {

            DateTime c;
            if (_view.StartDate > _view.EndDate)
            {
                c = _view.StartDate;
                _view.StartDate = _view.EndDate;
                _view.EndDate = c;
            }
            
        }
        // Логика обработки нажатия кнопки Применить
        public void ApllyButtonClicked(object sender, EventArgs e)
        {
        }
    }
}

