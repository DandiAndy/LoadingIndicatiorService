using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LIS.Services;
using LIS.Views;

namespace LIS
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<LoadingIndicatorService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
