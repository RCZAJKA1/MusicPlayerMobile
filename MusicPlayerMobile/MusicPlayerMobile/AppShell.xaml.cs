namespace MusicPlayerMobile
{
    using System;

    using Xamarin.Forms;

    public partial class AppShell : Shell
    {
        public AppShell()
        {
            this.InitializeComponent();

            // Register routes for new pages
            //Routing.RegisterRoute(nameof(NewPage), typeof(NewPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
        }
    }
}
