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
    public partial class AddTermPage : ContentPage
    {
        public AddTermPage()
        {
            InitializeComponent();
            BindingContext = new Term
            {
                Start = DateTime.Today,
                End = DateTime.Today.AddMonths(6)
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            addButton.IsEnabled = false;
        }

        private async void OnAddButtonClicked(object sender, EventArgs args)
        {
            try
            {
                Term term = (Term)BindingContext;
                await App.DataBase.AddNewTermAsync(term);
                await Navigation.PopModalAsync();
            }
            catch (SQLite.SQLiteException e)
            {
                if ((e.Message).Contains("UNIQUE"))
                {
                    await DisplayAlert("Error", "Term name already exists", "OK");
                }
                else
                {
                    await DisplayAlert("Error", e.Message, "OK");
                }
            }
        }

        private async void FieldUpdated(object sender, EventArgs args)
        {
            if (newTermName != null)
            {
                addButton.IsEnabled = true;
            }
            else
            {
                addButton.IsEnabled = false;
            }
        }
    }
}