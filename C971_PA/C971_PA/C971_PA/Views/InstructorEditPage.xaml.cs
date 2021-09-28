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
    public partial class InstructorEditPage : ContentPage
    {
        Instructor instructor;
        public InstructorEditPage(Instructor instructor)
        {
            InitializeComponent();
            this.instructor = instructor;
            this.BindingContext = instructor;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            saveButton.IsEnabled = false;
        }

        private void OnSaveButtonClicked(object sender, EventArgs args)
        {

        }

        private void FieldUpdated(object sender, EventArgs args)
        {
            saveButton.IsEnabled = true;
        }
    }
}