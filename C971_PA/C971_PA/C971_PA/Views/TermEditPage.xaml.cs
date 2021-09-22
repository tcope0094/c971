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
    public partial class TermEditPage : ContentPage
    {
        Term term;
        public TermEditPage(Term term)
        {
            InitializeComponent();
            this.term = term;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = term;
        }
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..");
            //Navigation.PopModalAsync();
            return base.OnBackButtonPressed();
        }
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
                        
        }
    }
}