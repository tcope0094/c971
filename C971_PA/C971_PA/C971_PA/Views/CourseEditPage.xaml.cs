﻿using System;
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

            SetSwitches();
            loaded = true;
        }

        private void SetSwitches()
        {
            courseStartSwitch.IsToggled = false;
            courseDueSwitch.IsToggled = false;

            if (Settings.CourseStartNotifications)
            {
                courseStartSwitch.IsToggled = true;
            }
            if (Settings.CourseDueDateNotifications)
            {
                courseDueSwitch.IsToggled = true;
            }
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

        private void OnFieldChanged(object sender, EventArgs args)
        {
            if (loaded)
            {
                saveButton.IsEnabled = true;
            }
        }
        private async void OnCourseDueSwitchToggled(object sender, EventArgs args)
        {
            if (courseDueSwitch.IsToggled)
            {
                Settings.CourseDueDateNotifications = true;
            }
            else
            {
                Settings.CourseDueDateNotifications = false;
            }
        }
        private async void OnCourseStartSwitchToggled(object sender, EventArgs args)
        {
            if (courseStartSwitch.IsToggled)
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