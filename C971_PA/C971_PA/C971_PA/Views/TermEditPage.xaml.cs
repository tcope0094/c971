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
    public partial class TermEditPage : ContentPage
    {
        Term term;
        ObservableCollection<Course> coursesInTerm;
        ObservableCollection<Course> coursesNotInTerm;
        ObservableCollection<Course> coursesToAdd;
        public bool IsRefreshing { get; set; }
        public TermEditPage(Term term)
        {
            InitializeComponent();
            this.term = term;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = term;

            this.coursesInTerm = await App.DataBase.GetCoursesInTermAsync(this.term);
            termCoursesCollectionView.ItemsSource = coursesInTerm;

            this.coursesNotInTerm = await App.DataBase.GetCoursesNotInTermAsync(this.term);
            addCoursePicker.ItemsSource = coursesNotInTerm;

            this.coursesToAdd = new ObservableCollection<Course>();
            coursesToAddCollectionView.ItemsSource = this.coursesToAdd;
        }
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..");
            //Navigation.PopModalAsync();
            return base.OnBackButtonPressed();
        }
        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
                        
        }

        public async void OnAddCourseButtonClicked(object sender, EventArgs args)
        {

        }

        public async void OnCoursePickerSelected(object sender, EventArgs args)
        {
            coursesToAdd.Add((Course)addCoursePicker.SelectedItem);
            coursesNotInTerm.Remove((Course)addCoursePicker.SelectedItem);
            addCoursePicker.SelectedItem = null;            
        }

        public async void OnSaveButtonClicked(object sender, EventArgs args)
        {

        }
    }
}