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
        public CourseEditPage(Course course)
        {
            InitializeComponent();
            this.course = course;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = this.course;
            this.instructor = await App.DataBase.GetInstructorByCourseAsync(this.course);
            this.allInstructors = await App.DataBase.GetAllInstructorsAsync();
            instructorPicker.BindingContext = allInstructors;


            instructorPicker.ItemsSource = allInstructors;
            var test = allInstructors.IndexOf(instructor);
            instructorPicker.SelectedIndex = allInstructors.IndexOf(instructor);
        }
    }
}