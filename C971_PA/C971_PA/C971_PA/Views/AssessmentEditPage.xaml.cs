﻿using System;
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
    public partial class AssessmentEditPage : ContentPage
    {
        Assessment assessment;
        List<string> types;
        public AssessmentEditPage(Assessment assessment)
        {
            InitializeComponent();

            this.assessment = assessment;
            this.types = Assessment.PossibleTypes;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.BindingContext = assessment;

            assessmentTypePicker.BindingContext = assessment.Type;
            assessmentTypePicker.ItemsSource = types;
            assessmentTypePicker.SelectedIndex = types.IndexOf(assessment.Type);
            saveButton.IsEnabled = false;
        }

        private void OnFieldChanged(object sender, EventArgs args)
        {
            saveButton.IsEnabled = true;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            assessment.Type = assessmentTypePicker.SelectedItem.ToString();
            int result = await App.DataBase.UpdateAssessmentAsync(assessment);

            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}