namespace MusicPlayerMobile
{
    using System;

    using MusicPlayerMobile.Services;
    using MusicPlayerMobile.Views;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
