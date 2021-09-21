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
        public CourseDetailPage(int courseKey)
        {
            InitializeComponent();
            this.courseKey = courseKey;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = await App.DataBase.GetCourseAsync(this.courseKey);
            this.instructor = await App.DataBase.GetInstructorByCourseAsync((Course)this.BindingContext);
            instructorGrid.BindingContext = instructor;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}