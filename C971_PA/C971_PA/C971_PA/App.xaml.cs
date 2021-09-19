using C971_PA.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace C971_PA
{
    public partial class App : Application
    {
        public static Models.C971DB DataBase { get; private set; }      

        public App(string dbPath)
        {
            InitializeComponent();

            DataBase = new Models.C971DB(dbPath);
            var temp = Settings.FirstRun;
            if (Settings.FirstRun)
            {
                DataBase.CreateTables();
                DataBase.CreateInitialData();
                Settings.FirstRun = false;
            }

            //DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
