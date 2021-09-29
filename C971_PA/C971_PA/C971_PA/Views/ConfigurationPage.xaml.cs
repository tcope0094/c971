using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_PA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationPage : ContentPage
    {
        public ConfigurationPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Settings.CourseDueDateNotifications)
            {
                courseDueSwitch.IsToggled = true;
            }
            else
            {
                courseDueSwitch.IsToggled = false;
            }

            if (Settings.AssessmentDueDateNotifications)
            {
                assessmentSwitch.IsToggled = true;
            }
            else
            {
                assessmentSwitch.IsToggled = false;
            }
        }

        public void OnClearDatabaseButtonClicked(object sender, EventArgs args)
        {
            App.DataBase.ClearAllTables();
            Settings.FirstRun = true;
        }

        private async void OnCourseDueSwitchToggled(object sender, EventArgs args)
        {
            if (courseDueSwitch.IsToggled == true)
            {
                Settings.CourseDueDateNotifications = true;
            }
            else
            {
                Settings.CourseDueDateNotifications = false;
            }
        }
        private async void OnAssessmentSwitchToggled(object sender, EventArgs args)
        {
            if (assessmentSwitch.IsToggled == true)
            {
                Settings.AssessmentDueDateNotifications = true;
            }
            else
            {
                Settings.AssessmentDueDateNotifications = false;
            }
        }

        private async void OnCourseStartSwitchToggled(object sender, EventArgs args)
        {
            if (courseStartSwitch.IsToggled == true)
            {
                Settings.CourseStartNotifications = true;
            }
            else
            {
                Settings.CourseStartNotifications = false;
            }
        }
    }
}