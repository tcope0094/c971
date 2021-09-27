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
    public partial class AddCourseToTerm : ContentPage
    {
        Term term;
        ObservableCollection<Course> coursesNotInTerm;
        ObservableCollection<Course> coursesToAdd;
        public AddCourseToTerm(Term term)
        {
            InitializeComponent();
            this.term = term;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = term;

            this.coursesNotInTerm = await App.DataBase.GetCoursesNotInTermAsync(this.term);
            addCoursePicker.ItemsSource = coursesNotInTerm;

            this.coursesToAdd = new ObservableCollection<Course>();
            coursesToAddCollectionView.ItemsSource = this.coursesToAdd;
        }

        public void OnCoursePickerSelected(object sender, EventArgs args)
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