
using TestForWork.Model.DataBase;
using TestForWork.View;

namespace TestForWork.Presenter
{
    public class MainFormPresenter : IMainFormPresenter
    {
        private readonly IMainFormView _view;

        public MainFormPresenter(IMainFormView view)
        {
            _view = view;
            _view.DateRangeChanged += DateRangeChanged;
            _view.ListEmployeesClicked += ListEmployeesClicked;
            _view.StatEmployeesClick += StatEmployeesClick;
            _view.ApplyButtonClicked += ApllyButtonClicked;
        }

        // Логика обработки нажатия кнопки "Список сотрудников"
        public void ListEmployeesClicked(object sender, EventArgs e)
        {

        }

        // Логика обработки нажатия кнопки "Статистика сотрудников"
        public void StatEmployeesClick(object sender, EventArgs e)
        {

        }

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

        public void ApllyButtonClicked(object sender, EventArgs e)
        {
            DatabaseCreator test = new DatabaseCreator();
        }
    }
}

