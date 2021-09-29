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
    public partial class CoursesPage : ContentPage
    {
        public CoursesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            coursesListView.ItemsSource = await App.DataBase.GetAllCoursesAsync();
        }

        async void OnNewClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddCoursePage());
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Course course = (Course)args.SelectedItem;
            if (args.SelectedItem != null)
            {
                coursesListView.SelectedItem = null;
                await Shell.Current.Navigation.PushAsync(new CourseDetailPage(course.CourseKey));
            }
        }
    }
}