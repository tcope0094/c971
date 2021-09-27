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

            this.coursesNotInTerm = await App.DataBase.GetCoursesNotInATermAsync();
            addCoursePicker.ItemsSource = coursesNotInTerm;

            this.coursesToAdd = new ObservableCollection<Course>();
            var test = this.BindingContext;

            nameEntry.Text = term.Name;
        }
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..");
            //Navigation.PopModalAsync();
            return base.OnBackButtonPressed();
        }
        public async void OnCoursePickerSelected(object sender, EventArgs args)
        {
            coursesInTerm.Add((Course)addCoursePicker.SelectedItem);
            coursesToAdd.Add((Course)addCoursePicker.SelectedItem);
            coursesNotInTerm.Remove((Course)addCoursePicker.SelectedItem);
            addCoursePicker.SelectedItem = null;
            saveButton.IsEnabled = true;
        }

        public async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            App.DataBase.UpdateTermAsync((Term)this.BindingContext);
            if (coursesToAdd.Count > 0)
            {
                foreach (var item in coursesToAdd)
                {
                    if (item == null)
                    {
                        continue;
                    }
                    var temp = coursesToAdd;
                    item.TermID = this.term.TermKey;
                    int result = await App.DataBase.UpdateCourseAsync(item);
                }
            }
            Shell.Current.Navigation.PopModalAsync();
        }

        public async void OnRemoveCoursesButtonClicked(object sender, EventArgs args)
        {
            int result = await App.DataBase.RemoveCoursesFromTermAsync((Course)termCoursesCollectionView.SelectedItem);
            this.coursesInTerm.Remove((Course)termCoursesCollectionView.SelectedItem);
            removeButton.IsEnabled = false;
        }

        public async void OnTermCoursesSelectionChanged(object sender, EventArgs args)
        {
            if (termCoursesCollectionView.SelectedItem != null)
            {
                removeButton.IsEnabled = true;
            }
            else
            {
                removeButton.IsEnabled = false;
            }
        }


    }
}