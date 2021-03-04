using LIS.Interface;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup;
using System.Threading.Tasks;
using System.Reactive;
using System.Linq;
using Rg.Plugins.Popup.Extensions;

namespace LIS.Views
{
    public partial class AboutPage : ContentPage
    {
        private ILoadingIndicatorService _loadingIndicatorService;
        private IndicatorOverlay _indicatorOverlay;
        public AboutPage()
        {
            InitializeComponent();
            _loadingIndicatorService = DependencyService.Get<ILoadingIndicatorService>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _loadingIndicatorService.Subscribe(
                async _ => await ApplyOverlay(),
                async () => await RemoveOverlay());

            using (_loadingIndicatorService.IsLoading())
            {
                // Testing
                await Task.Delay(3000);
            }
        }

        private async Task ApplyOverlay()
        {
            _indicatorOverlay = new IndicatorOverlay();
            await Navigation.PushPopupAsync(_indicatorOverlay);
        }

        private async Task RemoveOverlay()
        {
            await Navigation.PopPopupAsync();
        }
    }
}