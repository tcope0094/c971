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
            string message = "";
            bool validEmail = App.IsValidEmail(emailEntry.Text);
            bool validPhoneNumber = App.IsValidPhoneNumber(phoneEntry.Text);

            if (!validEmail)
            {
                message += Environment.NewLine + "Email is invalid";
            }
            if (!validPhoneNumber)
            {
                message += Environment.NewLine + "Phone Number is invalid";
            }
            if (message != "")
            {
                await DisplayAlert("Error", message, "OK");
            }
            try
            {
                int result = await App.DataBase.UpdateInstructorAsync(instructor);

                await Shell.Current.Navigation.PopModalAsync();
            }
            catch (SQLite.SQLiteException e)
            {
                if ((e.Message).Contains("UNIQUE"))
                {
                    await DisplayAlert("Error", "Instructor name already exists", "OK");
                }
                else
                {
                    await DisplayAlert("Error", e.Message, "OK");
                }
            }
        }

        private void ValidateFields(object sender, EventArgs args)
        {
            if (nameEntry.Text != null && emailEntry.Text != null && phoneEntry.Text != null)
            {
                saveButton.IsEnabled = true;
            }
            else
            {
                saveButton.IsEnabled = false;
            }
        }
    }
}