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
    public partial class CourseEditPage : ContentPage
    {
        Instructor instructor;
        List<string> allInstructors;
        List<string> allStatuses;
        Course course;
        Instructor newInstructor;
        Term term;
        bool loaded;
        public CourseEditPage(Course course, Instructor instructor)
        {
            InitializeComponent();
            this.course = course;
            this.instructor = instructor;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = this.course;
            this.allInstructors = await GetAllInstructorNames();

            if (course.TermID != null)
            {
                int termId = course.TermID ?? default(int);
                this.term = await App.DataBase.GetTermAsync(termId);
            }

            this.allStatuses = Course.PossibleStatuses;

            statusPicker.BindingContext = course.Status;
            instructorPicker.BindingContext = allInstructors;

            statusPicker.ItemsSource = allStatuses;
            instructorPicker.ItemsSource = allInstructors;

            statusPicker.SelectedIndex = allStatuses.IndexOf(course.Status);
            instructorPicker.SelectedIndex = allInstructors.IndexOf(instructor.Name);

            startDatePicker.MinimumDate = term.Start;
            startDatePicker.MaximumDate = term.End;
            endDatePicker.MinimumDate = term.Start;
            endDatePicker.MaximumDate = term.End;
            dueDatePicker.MinimumDate = term.Start;
            dueDatePicker.MaximumDate = term.End;

            if (term != null)
            {
                termName.Text = term.Name;
            }
            else
            {
                termName.Text = "Unassigned";
            }
                     
            loaded = true;
        }

        private async Task<List<string>> GetAllInstructorNames()
        {
            var task = await App.DataBase.GetAllInstructorsAsync();
            List<string> returnList = new List<string>();
            foreach (var item in task)
            {
                returnList.Add(item.Name);
            }
            return returnList;
        }

        private async void OnInstructorSelected(object sender, EventArgs args)
        {
            if (loaded)
            {
                if (instructor.Name != (string)instructorPicker.SelectedItem)
                {
                    newInstructor = await App.DataBase.GetInstructorByNameAsync((string)instructorPicker.SelectedItem);
                    saveButton.IsEnabled = true;
                }
            }
        }


        private async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            if (ValidDates(course, term))
            {
                try
                {
                    if (newInstructor != null)
                    {
                        course.InstructorID = newInstructor.InstructorKey;
                    }
                    course.Status = (string)statusPicker.SelectedItem;

                    var result = App.DataBase.UpdateCourseAsync(course);

                    await Shell.Current.Navigation.PopModalAsync();
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
        }

        private void OnFieldChanged(object sender, EventArgs args)
        {
            if (loaded)
            {
                saveButton.IsEnabled = true;
            }
        }

        private bool ValidDates(Course course, Term term)
        {
            bool valid = false;
            bool validCourse = false;
            bool validTermCourse = false;

            if (course.Start <= course.End && course.End <= course.DueDate)
            {
                validCourse = true;
            }

            if (course.Start >= term.Start && course.Start <= term.End && course.End >= term.Start && course.End <= term.End)
            {
                validTermCourse = true;
            }

            if (validCourse && validTermCourse)
            {
                valid = true;
            }

            return valid;
        }
    }
}