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

            //BindingContext = new Term();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            termsCollectionView.ItemsSource = await App.DataBase.GetAllTermsAsync();
        }

        async void OnNewClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new AddTermPage());
        }

        async void OnSelectionChanged(object sender, SelectedItemChangedEventArgs args)
        {
            await Navigation.PushModalAsync(new TermDetailPage((Term)args.SelectedItem));
        }
    }
}