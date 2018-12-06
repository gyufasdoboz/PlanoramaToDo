using System;
using System.IO;
using ToDo.DB;
using ToDo.Model;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToDo
{
    /// <summary>
    /// This gets called first,
    /// We handle the app life-cycle here
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Makes sure that the User Data gets saved
        /// </summary>
        static IWebService webService;

        /// <summary>
        /// The WebService is currently hooked to an sqlite local db,
        /// But this can be changed easily to another service,
        /// which implement the WebService interface
        /// </summary>
        public static IWebService WebService
        {
            get
            {
                if (webService != null)
                    return webService;

                return webService = new ToDoDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new ToDoPage();
            ToDoHandler.LoadToDoListAsync();
        }

        protected override void OnStart()
        {
            ToDoHandler.LoadToDoListAsync();
        }

        protected override void OnSleep()
        {
            ToDoHandler.SaveToDoList();
        }

        protected override void OnResume()
        {
            ToDoHandler.LoadToDoListAsync();
        }
    }
}
