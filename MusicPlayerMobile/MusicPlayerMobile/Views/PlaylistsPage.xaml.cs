namespace MusicPlayerMobile.Views
{
    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    ///     The code behind for the <see cref="PlaylistsPage"/> view.
    /// </summary>
    public partial class PlaylistsPage : ContentPage
    {
        /// <summary>
        ///     The playlist view model.
        /// </summary>
        private readonly PlaylistsViewModel _playlistsViewModel;

        /// <summary>
        ///     Creates a new instance of the <see cref="PlaylistsPage"/> class.
        /// </summary>
        public PlaylistsPage()
        {
            this.InitializeComponent();

            this.BindingContext = this._playlistsViewModel = new PlaylistsViewModel();
            this._playlistsViewModel = this.BindingContext as PlaylistsViewModel;
        }

        /// <summary>
        ///     Prepares the playlists view model while the page is appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this._playlistsViewModel.OnAppearingAsync();
        }
    }
}