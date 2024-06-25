using TestForWork.Model.DataBase.DbCs;
using TestForWork.View;

namespace TestForWork
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            DatabaseCreator test = new DatabaseCreator();
            test.ListEmployees();
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}