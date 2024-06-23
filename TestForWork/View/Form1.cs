namespace TestForWork.View
{
    public partial class MainForm : Form, IMainFormView
    {
        public event EventHandler ListEmployeesClicked;

        public event EventHandler StatEmployeesClick;
        
        private Panel conditionsPanel;
        //Инициализация
        public MainForm()
        {
            InitializeComponent();
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

        private void CreateButtons(string text,Size size, Point location)
        {
            Button button = new Button()
            {
                BackColor = Color.Gainsboro,
                Location = location,
                Text = text,
                Size = size
            };
            Controls.Add(button);
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
            conditionsPanel = new Panel
            {
                BackColor = Color.LightGray,
                Size = new Size(400, 300),
                Location = new Point(200, 10),
                Visible = false // Изначально панель невидима
            };

            // Пример добавления элементов на панель
            Label conditionslabel = new Label
            {
                Text = "Выберите условия",
                Location = new Point(130, 10),
                Size = new Size(130, 20)
            };
            conditionsPanel.Controls.Add(conditionslabel);
            Label statuslabel = new Label
            {
                Text = "Выберите статус сотрудинка:",
                Location = new Point(10, 30),
                Size = new Size(170, 20)
            };
            conditionsPanel.Controls.Add(statuslabel);
            Label worklabel = new Label
            {
                Text = "Выберите статус работника:",
                Location = new Point(10, 60),
                Size = new Size(170, 20)
            };
            conditionsPanel.Controls.Add(worklabel);
            ComboBox statusComboBox = new ComboBox
            {
                Items = {"хз", "хз", "хз"},
                Location = new Point(180, 30),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            conditionsPanel.Controls.Add(statusComboBox);
            ComboBox workStatus = new ComboBox
            {
                Items = {"принят", "уволен"},
                Location = new Point(180, 60),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            conditionsPanel.Controls.Add(workStatus);

            Button applyButton = new Button
            {
                Text = "Применить",
                Location = new Point(130, 270),
                Size = new Size(100, 30),
                BackColor = Color.Gainsboro
            };
            applyButton.Click += ApplyButton_Click;
            conditionsPanel.Controls.Add(applyButton);

            Controls.Add(conditionsPanel);
            
        }
        //Активация панели
        private void ShowConditionsPanel()
        {
            if (conditionsPanel == null)
            {
                CreateConditionsPanel();
            }

            conditionsPanel.Visible = !conditionsPanel.Visible;
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            // Логика обработки нажатия кнопки "Применить"
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
        
    }
}
