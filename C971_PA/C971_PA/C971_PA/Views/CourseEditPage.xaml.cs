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

            this.allStatuses = Course.PossibleStatuses;

            statusPicker.BindingContext = course.Status;
            instructorPicker.BindingContext = allInstructors;

            statusPicker.ItemsSource = allStatuses;
            instructorPicker.ItemsSource = allInstructors;

            statusPicker.SelectedIndex = allStatuses.IndexOf(course.Status);
            instructorPicker.SelectedIndex = allInstructors.IndexOf(instructor.Name);
            loaded = true;
        }

        private async Task<List<string>> GetAllInstructorNames()
        {
            var task = App.DataBase.GetAllInstructorsAsync();
            task.Wait();
            var result = task.Result;
            List<string> returnList = new List<string>();
            foreach (var item in result)
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
            if (newInstructor != null)
            {
                course.InstructorID = newInstructor.InstructorKey;
            }
            course.Status = (string)statusPicker.SelectedItem;

            var result = App.DataBase.UpdateCourseAsync(course);

            await Shell.Current.Navigation.PopModalAsync();
        }

        private void OnFieldChanged(object sender, EventArgs args)
        {
            if (loaded)
            {
                saveButton.IsEnabled = true;
            }
        }
    }
}