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
    public partial class AddAssessmentsPage : ContentPage
    {
        Course course;
        public AddAssessmentsPage(Course course)
        {
            InitializeComponent();
            this.course = course;
        }

        private async void OnAddButtonClicked(object sender, EventArgs args)
        {
            Assessment pa = new Assessment();
            Assessment oa = new Assessment();

            if (oaDueDatePicker.Date > course.End || oaDueDatePicker.Date < course.Start || paDueDatePicker.Date > course.End || paDueDatePicker.Date < course.Start)
            {
                await DisplayAlert("Error", "Assessment due date must be between course start date and end date", "OK");
            }
            else
            {
                pa.CourseID = course.CourseKey;
                pa.DueDate = paDueDatePicker.Date;
                pa.Name = newPaName.Text;
                pa.Type = "PA";

                oa.CourseID = course.CourseKey;
                oa.DueDate = oaDueDatePicker.Date;
                oa.Name = newOaName.Text;
                oa.Type = "OA";
            }

            try
            {
                int paResult = await App.DataBase.AddNewAssessmentAsync(pa);

                await Shell.Current.Navigation.PopToRootAsync();

            }
            catch (SQLite.SQLiteException e)
            {
                if ((e.Message).Contains("UNIQUE"))
                {
                    await DisplayAlert("Error", "Performance Assessment name already exists", "OK");
                }
                else
                {
                    await DisplayAlert("Error", e.Message, "OK");
                }
            }

            try
            {
                int oaResult = await App.DataBase.AddNewAssessmentAsync(oa);

                await Shell.Current.Navigation.PopToRootAsync();

            }
            catch (SQLite.SQLiteException e)
            {
                if ((e.Message).Contains("UNIQUE"))
                {
                    await DisplayAlert("Error", "Objective Assessment name already exists", "OK");
                }
                else
                {
                    await DisplayAlert("Error", e.Message, "OK");
                }
            }
        }

        private async void ValidateFields(object sender, EventArgs args)
        {
            if (newOaName.Text != null && newPaName.Text != null)
            {
                addButton.IsEnabled = true;
            }
        }
    }
}