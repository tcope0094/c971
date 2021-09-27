using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using C971_PA.Models;

namespace C971_PA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetailPage : ContentPage
    {
        int termKey;

        public TermDetailPage(int termKey)
        {
            InitializeComponent();
            this.termKey = termKey;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = await App.DataBase.GetTermAsync(termKey);
            var coursesInTerm = await App.DataBase.GetCoursesInTermAsync((Term)this.BindingContext);
            termCoursesListView.ItemsSource = coursesInTerm; 
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Course course = (Course)args.SelectedItem;
            if (course != null)
            {
                await Shell.Current.Navigation.PushAsync(new CourseDetailPage(course.CourseKey));
            }
        }

        public async void OnEditClicked(object sender, EventArgs args)
        {
            Shell.Current.Navigation.PushModalAsync(new TermEditPage((Term)this.BindingContext));
        }
    }
}