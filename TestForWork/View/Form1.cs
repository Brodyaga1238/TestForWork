namespace TestForWork.View
{
    public partial class MainForm : Form, IMainFormView
    {
        public event EventHandler ListEmployeesClicked;

        public event EventHandler StatEmployeesClick;
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
            
            Button listEmployees = new Button();
            listEmployees.BackColor = Color.Gainsboro;
            listEmployees.Text = "Список сотрудников"; // Установим текст кнопки
            listEmployees.Size = new Size(150, 30); // Установим размеры кнопки
            listEmployees.Location = new Point(10, 10);
            listEmployees.Click += list_employees_Click; // Привязываем обработчик клика
            
            Controls.Add(listEmployees); // Добавляем кнопку на форму
        }
        //Создание кнопки для списка сотрудников по запросу
        private void ButtonStat_Emplyees()
        {
            
            Button statEmployees = new Button();
            statEmployees.BackColor = Color.Gainsboro;
            statEmployees.Text = "Статистика сотрудников"; // Установим текст кнопки
            statEmployees.Size = new Size(150, 30); // Установим размеры кнопки
            statEmployees.Click += stat_employees_Click; // Привязываем обработчик клика
            statEmployees.Location = new Point(10, 50);
            Controls.Add(statEmployees); // Добавляем кнопку на форму
        }
        //Событие списка
        private void list_employees_Click(object sender, EventArgs e)
        {
            ListEmployeesClicked?.Invoke(sender, e);
        }
        //Событие статистики
        private void stat_employees_Click(object sender, EventArgs e)
        {
            StatEmployeesClick?.Invoke(sender,e);
        }
        
    }
}
