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
        Course course;
        public CourseEditPage(Course course)
        {
            InitializeComponent();
            this.course = course;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = course;
        }
    }
}