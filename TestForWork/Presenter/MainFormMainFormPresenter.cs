using Microsoft.VisualBasic;
using TestForWork.View;

namespace TestForWork.Presenter
{
    public class MainFormPresenter:IMainFormPresenter
    {
        private readonly IMainFormView _view;
        
        public MainFormPresenter(IMainFormView view)
        {
            _view = view;
            _view.DateRangeChanged += DateRangeChanged;
            _view.ListEmployeesClicked += ListEmployeesClicked;
            _view.StatEmployeesClick += StatEmployeesClick;
        }
        

        public void ListEmployeesClicked(object sender, EventArgs e)
        {
            // Логика обработки нажатия кнопки "Список сотрудников"
        }

        public void StatEmployeesClick(object sender, EventArgs e)
        {
            // Логика обработки нажатия кнопки "Статистика сотрудников"
        }

        public void DateRangeChanged(object sender, EventArgs e)
        {
            DateTime startdate;
            DateTime enddate;
            if (_view.StartDate > _view.EndDate)
            {
                 startdate = _view.EndDate;
                 enddate = _view.StartDate;
            }
            else
            {
                startdate = _view.StartDate;
                enddate = _view.EndDate;
            }
            
        }
    }
}

