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
        private readonly SongsViewModel _songsViewModel;

        /// <summary>
        ///     Creates a new instance of the <see cref="SongsPage"/> class.
        /// </summary>
        public SongsPage()
        {
            this.InitializeComponent();

            this.BindingContext = this._songsViewModel = new SongsViewModel();
            this._songsViewModel = this.BindingContext as SongsViewModel;
        }

        /// <summary>
        ///     Prepares the view model while the page is appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this._songsViewModel.OnAppearingAsync();
        }
    }
}