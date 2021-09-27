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
            coursesInTerm.Add((Course)addCoursePicker.SelectedItem);
            coursesNotInTerm.Remove((Course)addCoursePicker.SelectedItem);
            addCoursePicker.SelectedItem = null;
            ValidateFields();
        }

        public async void OnSaveButtonClicked(object sender, EventArgs args)
        {

        }

        private void OnNameEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            ValidateFields();
        }
        private void OnDatePickerChanged(object sender, DateChangedEventArgs e)
        {
            ValidateFields();
        }
        private void ValidateFields()
        {
            if (nameEntry.Text == this.term.Name && startDatePicker.Date == this.term.Start && endDatePicker.Date == this.term.End && coursesToAdd.Count == 0)
            {
                saveButton.IsEnabled = false;
            }
            else
            {
                saveButton.IsEnabled = true;
            }
        }        
    }
}