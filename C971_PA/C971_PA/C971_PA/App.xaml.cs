using C971_PA.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Text.RegularExpressions;
using System.Net.Mail;

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

        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(\(?[0-9]{3}\)?|[0-9]{3}-)[0-9]{3}-?[0-9]{4}$";

            if (!Regex.Match(phoneNumber, pattern).Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
