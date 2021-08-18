using C971_PA.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace C971_PA.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}