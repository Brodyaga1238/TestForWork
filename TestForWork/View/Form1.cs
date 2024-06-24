using Microsoft.VisualBasic;
using TestForWork.Presenter;

namespace TestForWork.View
{
    public partial class MainForm : Form, IMainFormView
    {
        private DateTimePicker _datafirst;
        private DateTimePicker _datasecond;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        
        public event EventHandler ListEmployeesClicked;

        public event EventHandler StatEmployeesClick;
        public event EventHandler DateRangeChanged;
        
        private Panel _conditionsPanel;
        
        private MainFormPresenter _presenter;
        //Инициализация
        public MainForm()
        {
            InitializeComponent();
            _presenter = new MainFormPresenter(this);
        }
        //Запуск создания всех элементов п.и.
        private void InitializeComponent()
        {
           
            FormCreator();
            ButtonsAdd();
          
        }
        //Создание формы
        private void FormCreator()
        {
            SuspendLayout();
          
            ClientSize = new Size(1920, 1080);
            Name = "MainForm";
          
            ResumeLayout(false);
        }
        
        //Добавление кнопок
        private void ButtonsAdd()
        {
            SuspendLayout();
            ButtonsList_Employees();
            ButtonStat_Emplyees();
            ResumeLayout(false);

        }
        //Создание кнопки для списка сотрудников
        private void ButtonsList_Employees()
        {

            Button listEmployees = new Button()
            {
                BackColor = Color.Gainsboro,
                Text = "Список сотрудников",
                Size = new Size(150, 30), 
                Location = new Point(10, 10),
            };
            listEmployees.Click += list_employees_Click;
            
            Controls.Add(listEmployees); // Добавляем кнопку на форму
        }
        //Создание кнопки для списка сотрудников по запросу
        private void ButtonStat_Emplyees()
        {

            Button statEmployees = new Button()
            {
                BackColor = Color.Gainsboro,
                Text = "Статистика сотрудников",
                Size = new Size(150, 30),
                Location = new Point(10, 50),
            };
            statEmployees.Click += stat_employees_Click;
            Controls.Add(statEmployees); 
        }
        // Создание панели с условиями
        private void CreateConditionsPanel()
        {
            _conditionsPanel = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(400, 300),
                Location = new Point(200, 10),
               
            };

            //  добавления элементов на панель
            //  добавления лейбла условия на панель
            Label conditionslabel = new Label
            {
                Text = "Выберите условия",
                Location = new Point(130, 10),
                Size = new Size(130, 20)
            };
            _conditionsPanel.Controls.Add(conditionslabel);
            //  добавления лейбла статуса на панель
            Label statuslabel = new Label
            {
                Text = "Выберите статус сотрудинка:",
                Location = new Point(10, 30),
                Size = new Size(170, 20)
            };
            _conditionsPanel.Controls.Add(statuslabel);
            //  добавления рабочий статуса статуса на панель
            Label worklabel = new Label
            {
                Text = "Выберите статус работника:",
                Location = new Point(10, 60),
                Size = new Size(170, 20)
            };
            _conditionsPanel.Controls.Add(worklabel);
            //  добавления лейбла статуса на панель
            Label datastartlabel = new Label
            {
                Text = "Выберите дату :",
                Location = new Point(10, 90),
                Size = new Size(170, 20)
            };
            _conditionsPanel.Controls.Add(datastartlabel);
            //  добавления лейбла статуса на панель
            Label dataendlabel = new Label
            {
                Text = "Выберите дату :",
                Location = new Point(10, 120),
                Size = new Size(170, 20)
            };
            _conditionsPanel.Controls.Add(dataendlabel);
            //  добавления комбобокса статуса на панель
            ComboBox statusComboBox = new ComboBox
            {
                
                Location = new Point(180, 30),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _conditionsPanel.Controls.Add(statusComboBox);
            //  добавления комбобокса статуса работника на панель
            ComboBox workStatus = new ComboBox
            {
                Items = {"принят", "уволен"},
                Location = new Point(180, 60),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _conditionsPanel.Controls.Add(workStatus);
            //  добавления дататаймера начала на панель
             _datafirst = new DateTimePicker 
            {
                BackColor = Color.Gainsboro,
                Location = new Point(180, 90),
                Size = new Size(200, 20),
                Format = DateTimePickerFormat.Short,
                MaxDate = DateTime.Today
            };
            _datafirst.ValueChanged += DataRangeChanged;
            _conditionsPanel.Controls.Add(_datafirst);
            //  добавления дататаймера начала на панель
             _datasecond = new DateTimePicker 
            {
                BackColor = Color.Gainsboro,
                Location = new Point(180, 120),
                Size = new Size(200, 20),
                Format = DateTimePickerFormat.Short,
                MaxDate = DateTime.Today
            };
            _datasecond.ValueChanged += DataRangeChanged;
            _conditionsPanel.Controls.Add(_datasecond);
            //  добавления кнопки на панель
            Button applyButton = new Button
            {
                Text = "Применить",
                Location = new Point(130, 270),
                Size = new Size(100, 30),
                BackColor = Color.Gainsboro
            };
            applyButton.Click += ApplyButton_Click;
            _conditionsPanel.Controls.Add(applyButton);
            
            Controls.Add(_conditionsPanel);
            
        }
        //Активация панели
        private void ShowConditionsPanel()
        {
            if (_conditionsPanel == null)
            {
                CreateConditionsPanel();
            }
            else
            {
                Controls.Remove(_conditionsPanel);
                _conditionsPanel.Dispose(); 
                _conditionsPanel = null;  
            }
         
        }
        //Нажатие отправки запроса 
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Условия применены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        //Событие списка
        private void list_employees_Click(object sender, EventArgs e)
        {
            ListEmployeesClicked?.Invoke(sender, e);
        }
        //Событие статистики
        private void stat_employees_Click(object sender, EventArgs e)
        {
            ShowConditionsPanel();
            StatEmployeesClick?.Invoke(sender,e);
           
        }
        //Событие изменеия периода
        private void DataRangeChanged(object sender, EventArgs e)
        {
            StartDate = _datafirst.Value;
            EndDate =_datasecond.Value ;
            DateRangeChanged?.Invoke(sender, e);    
        }
    }
}
