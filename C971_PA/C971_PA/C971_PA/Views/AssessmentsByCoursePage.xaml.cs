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
    public partial class AssessmentsByCoursePage : ContentPage
    {
        Course course;
        ObservableCollection<Assessment> assessments;
        
        public AssessmentsByCoursePage(Course course)
        {
            InitializeComponent();

            this.course = course;
            backCommand.Command = new Command(async () => Shell.Current.Navigation.RemovePage(this));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = course;
            this.assessments = await App.DataBase.GetAssessmentsByCourseAsync(course);
            assessmentsListView.ItemsSource = assessments;

        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Assessment assessment = (Assessment)args.SelectedItem;
            await Shell.Current.Navigation.PushAsync(new AssessmentDetailPage(assessment.AssessmentKey));
        }

        private async void OnAddClicked(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushModalAsync(new AddAssessmentsPage(course));
        }

    }
}