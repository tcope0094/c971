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
            termCoursesListView.ItemsSource = await App.DataBase.GetCoursesInTermAsync((Term)this.BindingContext); 
        }

        public void OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs args)
        {
            int a = termCoursesListView.SelectedItems.Count;
            if (termCoursesListView.SelectedItems.Count > 0)
            {
                removeButton.IsEnabled = true;
            }
            else
            {
                removeButton.IsEnabled = false;
            }
        }

        public async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            Term term = (Term)BindingContext;
            await App.DataBase.UpdateTermAsync(term);
            await Navigation.PopModalAsync();
        }

        public async void OnRemoveButtonClicked(object sender, EventArgs args)
        {
            foreach (var item in termCoursesListView.SelectedItems)
            {
                await App.DataBase.RemoveCoursesFromTermAsync((Course)item);
            }
            termCoursesListView.ItemsSource = await App.DataBase.GetCoursesInTermAsync((Term)this.BindingContext);
        }

        public async void OnEditClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new TermEditPage((Term)this.BindingContext));
        }


    }
}