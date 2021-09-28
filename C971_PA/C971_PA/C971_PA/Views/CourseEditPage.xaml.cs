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
        List<Instructor> allInstructors;
        Course course;
        public CourseEditPage(Course course, Instructor instructor)
        {
            InitializeComponent();
            this.course = course;
            this.instructor = instructor;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = this.course;
            this.allInstructors = GetAllInstructorAndWait();
            instructorPicker.BindingContext = allInstructors;


            instructorPicker.ItemsSource = allInstructors;
            var test = allInstructors.IndexOf(instructor);
            instructorPicker.SelectedIndex = allInstructors.IndexOf(instructor);
        }

        private static Instructor GetInstructorAndWait(Course course)
        {
            var task = App.DataBase.GetInstructorByCourseAsync(course);
            task.Wait();
            var result = task.Result;
            return result;
        }

        private static List<Instructor> GetAllInstructorAndWait()
        {
            var task = App.DataBase.GetAllInstructorsAsync();
            task.Wait();
            var result = task.Result;
            return result;
        }
    }
}