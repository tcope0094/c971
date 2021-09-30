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
    public partial class AddCoursePage : ContentPage
    {
        public AddCoursePage()
        {
            InitializeComponent();
            this.BindingContext = new Course
            {
                Start = DateTime.Today,
                End = DateTime.Today.AddMonths(1),
                DueDate = DateTime.Today.AddMonths(6)
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            statusPicker.ItemsSource = Course.PossibleStatuses;
            statusPicker.SelectedIndex = 1;

            instructorPicker.ItemsSource = await App.DataBase.GetAllInstructorNamesAsync();

            addButton.IsEnabled = false;
        }

        private async void OnAddButtonClicked(object sender, EventArgs args)
        {
            try
            {
                Course course = (Course)BindingContext;
                Instructor instructor = await App.DataBase.GetInstructorByNameAsync((string)instructorPicker.SelectedItem);

                course.Status = (string)statusPicker.SelectedItem;
                course.InstructorID = instructor.InstructorKey;
                await App.DataBase.AddNewCourseAsync(course);
                await Shell.Current.Navigation.PushModalAsync(new AddAssessmentsPage(course));
            }
            catch (SQLite.SQLiteException e)
            {
                if ((e.Message).Contains("UNIQUE"))
                {
                    await DisplayAlert("Error", "Course name already exists", "OK");
                }
                else
                {
                    await DisplayAlert("Error", e.Message, "OK");
                }
            }
        }

        private async void ValidateFields(object sender, EventArgs args)
        {
            if (newCourseName != null && newCourseDescription != null && instructorPicker.SelectedIndex != -1 && startDatePicker.Date <= endDatePicker.Date && startDatePicker.Date <= dueDatePicker.Date)
            {
                addButton.IsEnabled = true;
            }
            else
            {
                addButton.IsEnabled = false;
            }
        }

        private async void OnNewAssessmentClicked(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushModalAsync(new AddAssessmentsPage(((Course)BindingContext)));
        }
    }
}