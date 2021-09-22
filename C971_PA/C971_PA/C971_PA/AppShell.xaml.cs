using C971_PA.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace C971_PA
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("terms", typeof(TermsPage));
            Routing.RegisterRoute("term_edit", typeof(TermEditPage));
            Routing.RegisterRoute("courses", typeof(CoursesPage));
            Routing.RegisterRoute("course_detail", typeof(CourseDetailPage));
            Routing.RegisterRoute("course_edit", typeof(CourseEditPage));
            Routing.RegisterRoute("term_detail", typeof(TermDetailPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
