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
    public partial class HomePage : ContentPage
    {
        List<Course> coursesDue = new List<Course>();
        List<Assessment> assessmentsDue = new List<Assessment>();
        List<Course> currentCourses = new List<Course>();
        Term term;
        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            coursesDue = await App.DataBase.GetCoursesDueAsync(7);
            assessmentsDue = await App.DataBase.GetAssessmentsDueAsync(7);
            
            coursesDueListView.ItemsSource = coursesDue;
            coursesDueListView.BindingContext = coursesDue;

            assessmentsDueListView.ItemsSource = assessmentsDue;
            assessmentsDueListView.BindingContext = assessmentsDue;
        }

        private async void OnAssessmentSelected(object sender, EventArgs args)
        {

        }
        private async void OnCourseSelected(object sender, EventArgs args)
        {

        }
    }
}