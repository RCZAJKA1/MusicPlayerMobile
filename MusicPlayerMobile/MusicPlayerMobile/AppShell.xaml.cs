namespace MusicPlayerMobile
{
    using System;

    using MusicPlayerMobile.Views;

    using Xamarin.Forms;

    /// <summary>
    ///     The application shell.
    /// </summary>
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            this.InitializeComponent();

            // Register routes for new pages
            Routing.RegisterRoute(nameof(CreatePlaylistPage), typeof(CreatePlaylistPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
        }
    }
}
