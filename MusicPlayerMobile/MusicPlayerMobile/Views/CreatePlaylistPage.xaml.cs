namespace MusicPlayerMobile.Views
{
    using System;

    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    ///     The code behind for the <see cref="CreatePlaylistPage"/> view.
    /// </summary>
    public partial class CreatePlaylistPage : ContentPage
    {
        /// <summary>
        ///     The create playlists view model.
        /// </summary>
        private readonly CreatePlaylistViewModel _createPlaylistsViewModel;

        public CreatePlaylistPage()
        {
            this.InitializeComponent();

            this.BindingContext = this._createPlaylistsViewModel = new CreatePlaylistViewModel();
            this._createPlaylistsViewModel = this.BindingContext as CreatePlaylistViewModel;
        }

        /// <summary>
        ///     Handles the create button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private async void CreateButton_Clicked(object sender, EventArgs e)
        {
            //// get selected songs
            ////List<object> selectedSongs = this.SelectableSongsListView.SelectedItems.ToList();
            ////List<Song> selectedSongs = new List<Song>();
            ////foreach (object selectedObject in selectedObjects)
            ////{
            ////    Song song = new Song
            ////    {
            ////        Name = selectedObject.
            ////    };
            ////}

            //// get new playlist name
            //string newPlaylistName = this.NewPlaylistNameEntry.Text;
            //if (string.IsNullOrWhiteSpace(newPlaylistName))
            //{
            //    Toast playlistNameRequiredMsg = Toast.MakeText(Android.App.Application.Context, "Playlist name required", ToastLength.Short);
            //    playlistNameRequiredMsg.Show();
            //    return;
            //}

            //// create new playlist object
            //Playlist newPlaylist = new Playlist()
            //{
            //    Name = newPlaylistName,
            //    //Songs = selectedSongs
            //};
            //this._createPlaylistsViewModel.NewPlaylist = newPlaylist;

            //// return to playlists page
            //await Shell.Current.GoToAsync($"..");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this._createPlaylistsViewModel.OnAppearingAsync().ConfigureAwait(false);
        }
    }
}