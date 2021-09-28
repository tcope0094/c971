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
    public partial class AssessmentDetailPage : ContentPage
    {
        int assessmentKey;
        Assessment assessment;

        public AssessmentDetailPage(int assessmentKey)
        {
            InitializeComponent();

            this.assessmentKey = assessmentKey;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            this.assessment = await App.DataBase.GetAssessmentAsync(assessmentKey);
            this.BindingContext = this.assessment;
        }

        private async void OnEditClicked(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushModalAsync(new AssessmentEditPage(assessment));
        }

        private async void OnDeleteClicked(object sender, EventArgs args)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this assessment?", "Yes", "No");

            if (confirm)
            {
                int result = await App.DataBase.DeleteAssessmentAsync(assessment);
                await Shell.Current.Navigation.PopAsync();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Shell.Current.Navigation.RemovePage(this);
            return base.OnBackButtonPressed();
        }
    }
}