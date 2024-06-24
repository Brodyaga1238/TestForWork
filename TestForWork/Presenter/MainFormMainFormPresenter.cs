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
             
            if (_view.StartDate > _view.EndDate)
            {
                DateTime startDate = _view.EndDate;
                DateTime endDate = _view.StartDate;
            }
            else
            {
                DateTime startDate = _view.StartDate;
                DateTime endDate = _view.EndDate;
            }   
            MessageBox.Show($"Список сотрудников нажат", "Информация");
        }
    }
}

