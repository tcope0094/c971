using C971_PA.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Plugin.LocalNotifications;
using C971_PA.Models;
using System.Threading.Tasks;

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
            TriggerNotifications();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            TriggerNotifications();
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

        public static async Task TriggerNotifications()
        {
            if (Settings.AssessmentDueDateNotifications)
            {
                List<Assessment> assessments = await App.DataBase.GetAssessmentsDueAsync(7);
                string message = "";
                if (assessments.Count > 0)
                {
                    foreach (var item in assessments)
                    {
                        message += $"{Environment.NewLine}{item.Name}: " + $"{item.DueDate}";
                    }
                    CrossLocalNotifications.Current.Show("Assessments Due Soon", message, 0);
                }
            }
            if (Settings.CourseDueDateNotifications)
            {
                List<Course> courses = await App.DataBase.GetCoursesDueAsync(7);
                string message = "";
                if (courses.Count > 0)
                {
                    foreach (var item in courses)
                    {
                        message += $"{Environment.NewLine}{item.Name}: " + $"{item.DueDate}";
                    }
                    CrossLocalNotifications.Current.Show("Courses Due Soon", message, 1);
                }
            }
            if (Settings.CourseStartNotifications)
            {
                List<Course> courses = await App.DataBase.GetUpcomingCoursesAsync(7);
                string message = "";
                if (courses.Count > 0)
                {
                    foreach (var item in courses)
                    {
                        message += $"{Environment.NewLine}{item.Name}: " + $"{item.Start}";
                    }
                    CrossLocalNotifications.Current.Show("Courses Starting Soon", message, 2);
                }
            }
        }
    }
}
