using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using C971_PA.Models;
using System.Windows.Input;

namespace C971_PA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        int courseKey;
        string nameOfTerm;
        Instructor instructor;
        Course course;
        Term term;
        public CourseDetailPage(int courseKey)
        {
            InitializeComponent();
            this.courseKey = courseKey;

            backCommand.Command = new Command(async () => Shell.Current.Navigation.RemovePage(this));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = await App.DataBase.GetCourseAsync(courseKey);
            this.course = (Course)this.BindingContext;
            this.instructor = await App.DataBase.GetInstructorByCourseAsync((Course)this.BindingContext);
            instructorGrid.BindingContext = instructor;
            if (string.IsNullOrWhiteSpace(course.Notes))
            {
                shareNotesButton.IsEnabled = false;
            }
            else
            {
                shareNotesButton.IsEnabled = true;
            }
            if (course.TermID != null)
            {
                int termId = course.TermID ?? default(int);
                this.term = await App.DataBase.GetTermAsync(termId);
            }
            if (term != null)
            {
                termName.Text = term.Name;
            }
            else
            {
                termName.Text = "Unassigned";
            }
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

        private async void OnShareNotesButtonClicked(object sender, EventArgs args)
        {
            await ShareNotes(course.Name, course.Notes);
        }

        private async Task ShareNotes(string courseName, string notes)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Notes:" + Environment.NewLine + notes,
                Title = $"{courseName} Notes",
                Subject = $"{courseName} Notes"
            }); 
        }
    }
}