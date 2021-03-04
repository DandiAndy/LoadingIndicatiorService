using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Animations;
using Xamarin.Forms;
using System;

namespace LIS.Views
{
    public class IndicatorOverlay : PopupPage
    {
        public Guid OverlayId = Guid.NewGuid();
        public IndicatorOverlay()
        {
            Animation = new FadeAnimation();
            BackgroundInputTransparent = false;
            CloseWhenBackgroundIsClicked = false;
            BackgroundColor = Color.FromRgba(0,0,0, 0.2);
            Content = new StackLayout
            {
                BackgroundColor = BackgroundColor,
                Children =
                {
                    new ActivityIndicator
                    {
                        WidthRequest = 100,
                        HeightRequest = 100,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Color = Color.White,
                        IsRunning = true
                    }
                }
            };
        }
    }
}
