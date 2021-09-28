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
    public partial class InstructorsPage : ContentPage
    {
        public InstructorsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            instructorsListView.ItemsSource = await App.DataBase.GetAllInstructorsAsync();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Instructor instructor = (Instructor)args.SelectedItem;

            if (args.SelectedItem != null)
            {
                await Shell.Current.Navigation.PushAsync(new InstructorDetailPage(instructor));
            }
        }
    }
}