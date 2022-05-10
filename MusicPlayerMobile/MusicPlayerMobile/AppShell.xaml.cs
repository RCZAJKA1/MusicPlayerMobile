namespace MusicPlayerMobile
{
    using System;
    using System.Collections.Generic;

    using MusicPlayerMobile.ViewModels;
    using MusicPlayerMobile.Views;

    using Xamarin.Forms;

    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
