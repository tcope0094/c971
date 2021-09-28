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
    public partial class InstructorEditPage : ContentPage
    {
        Instructor instructor;
        public InstructorEditPage(Instructor instructor)
        {
            InitializeComponent();
            this.instructor = instructor;
            this.BindingContext = instructor;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            saveButton.IsEnabled = false;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs args)
        {   
            int result = await App.DataBase.UpdateInstructorAsync(instructor);

            await Shell.Current.Navigation.PopModalAsync();
        }

        private void FieldUpdated(object sender, EventArgs args)
        {
            saveButton.IsEnabled = true;
        }
    }
}