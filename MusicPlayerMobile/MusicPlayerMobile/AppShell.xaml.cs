namespace MusicPlayerMobile
{
    using System;

    using MusicPlayerMobile.Views;

    using Xamarin.Forms;

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            this.InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Current.GoToAsync("//LoginPage");
        }
    }
}
