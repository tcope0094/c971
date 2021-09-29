using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace C971_PA
{
    public static class Settings
    {
        public static bool FirstRun
        {
            get { return Preferences.Get(nameof(FirstRun), true); }
            set { Preferences.Set(nameof(FirstRun), value); }
        }

        public static bool CourseDueDateNotifications
        {
            get { return Preferences.Get(nameof(CourseDueDateNotifications), true); }
            set { Preferences.Set(nameof(CourseDueDateNotifications), value); }
        }

        public static bool AssessmentDueDateNotifications
        {
            get { return Preferences.Get(nameof(AssessmentDueDateNotifications), true); }
            set { Preferences.Set(nameof(AssessmentDueDateNotifications), value); }
        }

        public static bool CourseStartNotifications
        {
            get { return Preferences.Get(nameof(CourseStartNotifications), true); }
            set { Preferences.Set(nameof(CourseStartNotifications), value); }
        }
    }
}
