using System.ComponentModel;
using TestForWork.Model.DataBase.DbCs;
using TestForWork.Presenter;

namespace TestForWork.View
{
    public partial class MainForm : Form, IMainFormView
    {
        private DateTimePicker _datafirst;
        private DateTimePicker _datasecond;
        private string _selectedStatus;

        public string SelectedStatus => _selectedStatus;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public event EventHandler ListEmployeesClicked;
        public event EventHandler StatEmployeesClick;
        public event EventHandler ApplyButtonClicked;
        
        private ComboBox _statusComboBox;
        private Panel _conditionsPanel;
        private Panel _statPanel;
        
        private MainFormPresenter _presenter;

        /// <summary>
        /// Инициализация формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            IDb db = new Database();
            _presenter = new MainFormPresenter(this, db);
        }

        /// <summary>
        /// Инициализация всех элементов п.и. начального окна.
        /// </summary>
        private void InitializeComponent()
        {
            FormCreator();
            ButtonsAdd();
        }
        
        /// <summary>
        /// Создание формы.
        /// </summary>
        private void FormCreator()
        {
            SuspendLayout();
            ClientSize = new Size(1920, 1080);
            Name = "MainForm";
            ResumeLayout(false);
        }
        
        /// <summary>
        /// Добавление кнопок.
        /// </summary>
        private void ButtonsAdd()
        {
            SuspendLayout();
            ButtonsList_Employees();
            ButtonStat_Emplyees();
            ResumeLayout(false);
        }
       
        /// <summary>
        /// Создание кнопки "Список сотрудников".
        /// </summary>
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
            Controls.Add(listEmployees);
        }

        /// <summary>
        /// Создание кнопки "Статистика сотрудников".
        /// </summary>
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

        /// <summary>
        /// Создание панели с условиями.
        /// </summary>
        private void CreateStatPanel()
        {
            _conditionsPanel = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(600, 800),
                Location = new Point(200, 10),
            };

            AddConditionsLabel();
            AddStatusLabel();
            AddDateStartLabel();
            AddDateEndLabel();
            AddDateTimePickers();
            AddApplyButton();

            Controls.Add(_conditionsPanel);
        }

        /// <summary>
        /// Добавление метки условий.
        /// </summary>
        private void AddConditionsLabel()
        {
            Label conditionslabel = new Label
            {
                Text = "Выберите условия",
                Location = new Point(130, 10),
                Size = new Size(130, 20)
            };
            _conditionsPanel.Controls.Add(conditionslabel);
        }

        /// <summary>
        /// Добавление метки статуса.
        /// </summary>
        private void AddStatusLabel()
        {
            Label statuslabel = new Label
            {
                Text = "Выберите статус сотрудника:",
                Location = new Point(10, 30),
                Size = new Size(170, 20)
            };
            _conditionsPanel.Controls.Add(statuslabel);
        }

        /// <summary>
        /// Добавление метки даты начала.
        /// </summary>
        private void AddDateStartLabel()
        {
            Label datastartlabel = new Label
            {
                Text = "Выберите дату начала:",
                Location = new Point(10, 90),
                Size = new Size(170, 20)
            };
            _conditionsPanel.Controls.Add(datastartlabel);
        }

        /// <summary>
        /// Добавление метки даты окончания.
        /// </summary>
        private void AddDateEndLabel()
        {
            Label dataendlabel = new Label
            {
                Text = "Выберите дату окончания:",
                Location = new Point(10, 120),
                Size = new Size(170, 20)
            };
            _conditionsPanel.Controls.Add(dataendlabel);
        }

        /// <summary>
        /// Добавление элементов выбора даты.
        /// </summary>
        private void AddDateTimePickers()
        {
            _datafirst = new DateTimePicker
            {
                BackColor = Color.Gainsboro,
                Location = new Point(180, 90),
                Size = new Size(200, 20),
                Format = DateTimePickerFormat.Short,
                MaxDate = DateTime.Today,
                Value = DateTime.Today
            };
            
            _conditionsPanel.Controls.Add(_datafirst);

            _datasecond = new DateTimePicker
            {
                BackColor = Color.Gainsboro,
                Location = new Point(180, 120),
                Size = new Size(200, 20),
                Format = DateTimePickerFormat.Short,
                MaxDate = DateTime.Today
            };
            _conditionsPanel.Controls.Add(_datasecond);
        }

        /// <summary>
        /// Создание кнопки "Принять".
        /// </summary>
        private void AddApplyButton()
        {
            Button applyButton = new Button
            {
                Text = "Применить",
                Location = new Point(430, 30),
                Size = new Size(100, 30),
                BackColor = Color.Gainsboro
            };
            applyButton.Click += ApplyButton_Click;
            _conditionsPanel.Controls.Add(applyButton);
        }

        /// <summary>
        /// Создание панели с выводом списка сотрудников.
        /// </summary>
        /// <param name="employees">Список сотрудников.</param>
        private void CreateEmplyeesPanell(List<Employee> employees)
        {
            _statPanel = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(770, 950),
                Location = new Point(200, 10),
            };
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false
            };
            dataGridView.DataSource = new BindingList<Employee>(employees);
            
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FirstName",
                HeaderText = "Name",
                DataPropertyName = "Name",
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastNameInitial",
                HeaderText = "SecondName",
                DataPropertyName = "SecondName",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MiddleNameInitial",
                HeaderText = "LastName",
                DataPropertyName = "LastName",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Department",
                HeaderText = "Dep",
                DataPropertyName = "Dep",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateEmploy",
                HeaderText = "DateEmploy",
                DataPropertyName = "DateEmploy",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DateUneploy",
                HeaderText = "DateUnEmploy",
                DataPropertyName = "DateUnEmploy",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Position",
                HeaderText = "Post",
                DataPropertyName = "Post",
                Width = 75,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            _statPanel.Controls.Add(dataGridView);
            Controls.Add(_statPanel);
        }

        /// <summary>
        /// Активация панели сотрудников.
        /// </summary>
        private void ShowConditionsPanel()
        {
            if (_conditionsPanel == null)
            {
                CreateStatPanel();
            }
        }

        /// <summary>
        /// Активация панели списка сотрудников.
        /// </summary>
        /// <param name="employees">Список сотрудников.</param>
        private void ShowEmployeesPanell(List<Employee> employees)
        {
            if (_statPanel == null)
            {
                CreateEmplyeesPanell(employees);
            }
        }

        /// <summary>
        /// Событие нажатия на кнопку "Применить".
        /// </summary>
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            _selectedStatus = _statusComboBox.SelectedItem?.ToString();
            if (_selectedStatus == null)
            {
                MessageBox.Show("Требуется выбрать статус", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            StartDate = _datafirst.Value;
            EndDate = _datasecond.Value;
            ApplyButtonClicked?.Invoke(sender, e);
        }
        
        /// <summary>
        /// Событие нажатия на кнопку "Список сотрудников".
        /// </summary>
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
        
        /// <summary>
        /// Событие нажатия на кнопку "Статистика сотрудников".
        /// </summary>
        private void stat_employees_Click(object sender, EventArgs e)
        {
            ShowConditionsPanel();
            if (_statPanel != null)
            {
                Controls.Remove(_statPanel);
                _statPanel.Dispose();
                _statPanel = null;
            }
            StatEmployeesClick?.Invoke(sender, e);
        }

        /// <summary>
        /// Событие изменения периода дат.
        /// </summary>

        /// <summary>
        /// Метод отображения списка сотрудников.
        /// </summary>
        public void DisplayEmployees(List<Employee> employees)
        {
            ShowEmployeesPanell(employees);
        }

        /// <summary>
        /// Метод добавляющий статусы в представление.
        /// </summary>
        /// /// <param name="statuses">Список статусов.</param>
        public void AddStatuses(List<string> statuses)
        {
            _statusComboBox = new ComboBox
            {
                Location = new Point(180, 30),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            foreach (var status in statuses)
            {
                _statusComboBox.Items.Add(status);
            }
            _conditionsPanel.Controls.Add(_statusComboBox);
        }
        /// <summary>
        /// Метод получающих команду от презентора на передачу данных.
        /// </summary>
        /// /// <param name="statcount">Словарь статус/количества.</param>
        public void DisplayStatusCount(Dictionary<DateTime, int> statcount)
        {
            ShowStatusCount(statcount);
        }
        /// <summary>
        /// Метод создающий таблицу на панель, Выводи статистику по списку сотрудников: количество сотрудников выбранного статуса, принятых или уволенных на работу за заданный период с разбиением по дням .
        /// </summary>
        /// /// <param name="statcount">Словарь день/количество.</param>
        private void ShowStatusCount(Dictionary<DateTime, int> statcount)
        {
            DataGridView existingDataGridView = _conditionsPanel.Controls.OfType<DataGridView>().FirstOrDefault();

            if (existingDataGridView != null)
            {
              
                existingDataGridView.Rows.Clear();

                foreach (var entry in statcount)
                {
                    existingDataGridView.Rows.Add(entry.Key.ToShortDateString(), entry.Value);
                }
            }
            else
            {
                DataGridView dataGridView = new DataGridView
                {
                    Location = new Point(150, 200),
                    Size = new Size(300, 300),
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                };

                dataGridView.Columns.Add("DateColumn", "Дата");
                dataGridView.Columns.Add("CountColumn", "Количество");

                foreach (var entry in statcount)
                {
                    dataGridView.Rows.Add(entry.Key.ToShortDateString(), entry.Value);
                }

                _conditionsPanel.Controls.Add(dataGridView);
            }
        }


    }
}
