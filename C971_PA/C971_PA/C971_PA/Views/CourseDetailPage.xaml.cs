using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971_PA.Models;

namespace C971_PA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(CourseKey), "courseKey")]
    public partial class CourseDetailPage : ContentPage
    {
        public int CourseKey { get; set; }


        Instructor instructor;
        Course course;
        public CourseDetailPage(int courseKey)
        {
            InitializeComponent();
            //this.courseKey = courseKey;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = await App.DataBase.GetCourseAsync(CourseKey);
            this.course = (Course)this.BindingContext;
            this.instructor = await App.DataBase.GetInstructorByCourseAsync((Course)this.BindingContext);
            instructorGrid.BindingContext = instructor;
        }

        public async void OnEditClicked(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushModalAsync(new CourseEditPage((Course)this.BindingContext, instructor));
        }

        private async void OnAssessmentsClicked(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushAsync(new AssessmentsByCoursePage(course));
        }

        private async void OnDeleteClicked(object sender, EventArgs args)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this course?", "Yes", "No");

            if (confirm)
            {
                int result = await App.DataBase.DeleteCourseAsync(course);
                
                // had to use the RemovePage method, the PopAsync method threw an ambiguous route error when you navigated to this page from 
                // the TermDetail page, but not from the course list page

                Shell.Current.Navigation.RemovePage(this);
            }
        }
    }
}