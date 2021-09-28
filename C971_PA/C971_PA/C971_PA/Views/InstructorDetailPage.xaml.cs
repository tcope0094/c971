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
    public partial class InstructorDetailPage : ContentPage
    {
        Instructor instructor;
        int instructorKey;
        public InstructorDetailPage(int instructorKey)
        {
            InitializeComponent();
            this.instructorKey = instructorKey;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.instructor = await App.DataBase.GetInstructorAsync(instructorKey);
            this.BindingContext = instructor;
            
        }
        private async void OnEditClicked(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushModalAsync(new InstructorEditPage(instructor));
        }

        private async void OnDeleteClicked(object sender, EventArgs args)
        {


            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this instructor?", "Yes", "No");

            if (confirm)
            {
                int result = await App.DataBase.DeleteInstructorAsync(instructor);
                await Shell.Current.Navigation.PopAsync();
            }
        }
    }
}