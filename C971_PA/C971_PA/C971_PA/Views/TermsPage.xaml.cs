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
    public partial class TermsPage : ContentPage
    {
        public TermsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            termsListView.ItemsSource = await App.DataBase.GetAllTermsAsync();
        }

        async void OnNewClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddTermPage());
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Term term = (Term)args.SelectedItem;
            if (args.SelectedItem != null)
            {
                termsListView.SelectedItem = null;
                await Navigation.PushAsync(new TermDetailPage(term.TermKey));
            }
        }
    }
}