using System.ComponentModel;
using Xamarin.Forms;
using LIS.ViewModels;

namespace LIS.Views
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