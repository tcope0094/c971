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
    public partial class TermEditPage : ContentPage
    {
        public TermEditPage(Term term)
        {
            InitializeComponent();
        }
        public void OnSelectedItemChanged(object sender, SelectedItemChangedEventArgs args)
        {
            //int a = termCoursesListView.SelectedItems.Count;
            //if (termCoursesListView.SelectedItems.Count > 0)
            //{
            //    removeButton.IsEnabled = true;
            //}
            //else
            //{
            //    removeButton.IsEnabled = false;
            //}
        }
    }
}