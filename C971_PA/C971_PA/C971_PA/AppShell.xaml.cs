using C971_PA.ViewModels;
using C971_PA.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace C971_PA
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(TermsPage), typeof(TermsPage));
            Routing.RegisterRoute(nameof(TermDetailPage), typeof(TermDetailPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
