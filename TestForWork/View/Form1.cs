using System.ComponentModel;
using TestForWork.Model.DataBase.DbCs;
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
        
        public event EventHandler ApplyButtonClicked;
        
        private Panel _conditionsPanel;
        private Panel _statPanel;
        
        private MainFormPresenter _presenter;
        //Инициализация
        public MainForm()
        {
            InitializeComponent();
            IDb db = new Database();
            _presenter = new MainFormPresenter(this, db);
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
        private void CreateStatPanel()
        {
            _conditionsPanel = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(400, 300),
                Location = new Point(200, 10),
               
            };
            Console.WriteLine(3);
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

        private void CreateEmplyeesPanell(List<Employee> employees)
        {
            _statPanel = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(750, 950),
                Location = new Point(200, 10),
            };
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible=false
            };
            dataGridView.DataSource = new BindingList<Employee>(employees);
            
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FirstName",
                HeaderText = "Имя",
                DataPropertyName = "Name", 
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastNameInitial",
                HeaderText = "Фамилия",
                DataPropertyName = "SecondName",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MiddleNameInitial",
                HeaderText = "Отчество",
                DataPropertyName = "LastName",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Статус",
                DataPropertyName = "Status",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Department",
                HeaderText = "Отдел",
                DataPropertyName = "Dep",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateEmploy",
                HeaderText = "Приём",
                DataPropertyName = "DateEmploy",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateUneploy",
                HeaderText = "Увольнение",
                DataPropertyName = "DateUnEmploy",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Position",
                HeaderText = "Должность",
                DataPropertyName = "Post",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            _statPanel.Controls.Add(dataGridView);
            Controls.Add(_statPanel);
        }
        //Активация панели
        private void ShowConditionsPanel()
        {
            if (_conditionsPanel == null)
            {
                CreateStatPanel();
            }
         
        }
        //Активация панели Статистика
        private void ShowEmployeesPanell(List<Employee> employees)
        {
            if (_statPanel == null)
            {
                CreateEmplyeesPanell(employees);
            }
        }
        //Нажатие отправки запроса 
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            ApplyButtonClicked?.Invoke(sender, e);
        }
        
        //Событие списка сотрудников
        private void list_employees_Click(object sender, EventArgs e)
        {
            if (_conditionsPanel != null)
            {
                Controls.Remove(_conditionsPanel);
                _conditionsPanel.Dispose(); 
                _conditionsPanel = null;  
            }
          
            ListEmployeesClicked?.Invoke(sender, e);
        }
        //Событие статистики
        private void stat_employees_Click(object sender, EventArgs e)
        {
            
            ShowConditionsPanel();
            if (_statPanel != null)
            {
                Controls.Remove(_statPanel);
                _statPanel.Dispose(); 
                _statPanel = null;  
            }
            StatEmployeesClick?.Invoke(sender,e);
        }
        //Событие изменеия периода
        private void DataRangeChanged(object sender, EventArgs e)
        {
            StartDate = _datafirst.Value;
            EndDate =_datasecond.Value ;
            DateRangeChanged?.Invoke(sender, e);    
        }
        //Показывание списка сотрудников
        public void DisplayEmployees(List<Employee> employees)
        {
            ShowEmployeesPanell(employees);
        }
        //Список статусов
        public void AddStatuses(List<string> statuses)
        {
            ComboBox statusComboBox = new ComboBox
            {
                Location = new Point(180, 30),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            foreach (var status in statuses)
            {
                statusComboBox.Items.Add(status);
            }
            _conditionsPanel.Controls.Add(statusComboBox);
        }
    }
}
