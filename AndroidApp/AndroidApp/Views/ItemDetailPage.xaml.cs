using AndroidApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AndroidApp.Views
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