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

            if (oaDueDatePicker.Date > course.End || oaDueDatePicker.Date < course.Start || paDueDatePicker.Date > course.End || paDueDatePicker.Date < course.Start)
            {
                await DisplayAlert("Error", "Assessment due date must be between course start date and end date", "OK");
            }

            else
            {
                Assessment pa = new Assessment();
                Assessment oa = new Assessment();

                pa.CourseID = course.CourseKey;
                pa.DueDate = paDueDatePicker.Date;
                pa.Name = newPaName.Text;
                pa.Type = "PA";

                oa.CourseID = course.CourseKey;
                oa.DueDate = oaDueDatePicker.Date;
                oa.Name = newOaName.Text;
                oa.Type = "OA";

                int paResult = await App.DataBase.AddNewAssessmentAsync(pa);
                int oaResult = await App.DataBase.AddNewAssessmentAsync(oa);

                Shell.Current.Navigation.PopToRootAsync();

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