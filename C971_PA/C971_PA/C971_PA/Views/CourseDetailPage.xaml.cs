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
    public partial class CourseDetailPage : ContentPage
    {
        Instructor instructor;
        int courseKey;
        Course course;
        public CourseDetailPage(int courseKey)
        {
            InitializeComponent();
            this.courseKey = courseKey;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = await App.DataBase.GetCourseAsync(this.courseKey);
            this.course = (Course)this.BindingContext;
            this.instructor = await App.DataBase.GetInstructorByCourseAsync((Course)this.BindingContext);
            instructorGrid.BindingContext = instructor;
        }

        public async void OnEditClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new CourseEditPage((Course)this.BindingContext));
        }

        private async void OnAssessmentsClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AssessmentsByCoursePage(course));
        }

        private async void OnDeleteClicked(object sender, EventArgs args)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this course?", "Yes", "No");

            if (confirm)
            {
                int result = await App.DataBase.DeleteCourseAsync(course);
                await Shell.Current.Navigation.PopAsync();
            }
        }

    }
}