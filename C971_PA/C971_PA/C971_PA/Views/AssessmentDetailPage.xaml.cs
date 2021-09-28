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

        Assessment assessment;

        public AssessmentDetailPage(Assessment assessment)
        {
            InitializeComponent();

            this.assessment = assessment;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = assessment;
        }

        private async void OnEditClicked(object sender, EventArgs args)
        {
            await Shell.Current.Navigation.PushModalAsync(new AssessmentEditPage(assessment));
        }
    }
}