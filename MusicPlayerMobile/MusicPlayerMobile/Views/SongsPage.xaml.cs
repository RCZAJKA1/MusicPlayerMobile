namespace MusicPlayerMobile.Views
{
    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    ///     The code behind for the page <see cref="SongsPage"/>.
    /// </summary>
    public partial class SongsPage : ContentPage
    {
        /// <summary>
        ///     The songs view model.
        /// </summary>
        private readonly SongsViewModel songsViewModel;

        /// <summary>
        ///     Creates a new instance of the <see cref="SongsPage"/> class.
        /// </summary>
        public SongsPage()
        {
            this.InitializeComponent();

            this.BindingContext = this.songsViewModel = new SongsViewModel();
            this.songsViewModel = this.BindingContext as SongsViewModel;
        }

        /// <summary>
        ///     Prepares the view model while the page is appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this.songsViewModel.OnAppearing();
        }
    }
}