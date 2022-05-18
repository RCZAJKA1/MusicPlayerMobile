namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;
    using MusicPlayerMobile.Views;

    using Newtonsoft.Json;

    using Xamarin.Forms;

    public class CreatePlaylistViewModel : BaseViewModel
    {
        /// <summary>
        ///     The song service.
        /// </summary>
        private readonly ISongService _songService;

        /// <summary>
        ///     The file service.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        ///     The navigation service.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        ///     The android toast service.
        /// </summary>
        private readonly IAndroidToastService _androidToastService;

        /// <summary>
        ///     The new playlist.
        /// </summary>
        private Playlist _newPlaylist;

        /// <summary>
        ///     The new playlist name.
        /// </summary>
        private string _newPlaylistName;

        /// <summary>
        ///     The selected songs.
        /// </summary>
        private List<Song> _selectedSongs;

        /// <summary>
        ///     The selectable songs.
        /// </summary>
        private List<Song> _selectableSongs;

        /// <summary>
        ///     Creates a new instance of the <see cref="CreatePlaylistViewModel"/> class.
        /// </summary>
        public CreatePlaylistViewModel()
        {
            this.Title = "Create Playlist";

            this.SongSelectedCommand = new Command<Song>(this.OnSongSelectedCommand);
            this.BackButtonClickedCommand = new Command(async () => await this.OnBackClickedCommand());

            this.AllSelectableSongs = new List<Song>();
            this._selectedSongs = new List<Song>();
            this._songService = DependencyService.Get<ISongService>() ?? throw new InvalidOperationException("Unable to get dependency ISongService");
            this._fileService = DependencyService.Get<IFileService>() ?? throw new InvalidOperationException("Unable to get dependency IFileService");
            this._navigationService = DependencyService.Get<INavigationService>() ?? throw new InvalidOperationException("Unable to get dependency INavigationService");
            this._androidToastService = DependencyService.Get<IAndroidToastService>() ?? throw new InvalidOperationException("Unable to get dependency IAndroidToastService");
        }

        /// <summary>
        ///     Gets the song selected command.
        /// </summary>
        public Command SongSelectedCommand { get; }

        /// <summary>
        ///     Gets the back button clicked command.
        /// </summary>
        public Command BackButtonClickedCommand { get; }

        /// <summary>
        ///     Gets and sets all selectable songs. Needed for xaml binding.
        /// </summary>
        public List<Song> AllSelectableSongs
        {
            get => this._selectableSongs;
            set => this.SetProperty(ref this._selectableSongs, value);
        }

        /// <summary>
        ///     Gets and sets the new playlist.
        /// </summary>
        public Playlist NewPlaylist
        {
            get => this._newPlaylist;
            set => this.SetProperty(ref this._newPlaylist, value);
        }

        /// <summary>
        ///     Gets and sets the new playlist name. Needed for xaml binding.
        /// </summary>
        public string PlaylistName
        {
            get => this._newPlaylistName;
            set => this.SetProperty(ref this._newPlaylistName, value);
        }

        /// <summary>
        ///     Gets and sets the selected songs.
        /// </summary>
        internal List<Song> SelectedSongs
        {
            get => this._selectedSongs;
            set => this.SetProperty(ref this._selectedSongs, value);
        }

        /// <summary>
        ///     Caches the selected song to be added to the new playlist.
        /// </summary>
        /// <param name="song">The selected song.</param>
        private void OnSongSelectedCommand(Song song)
        {
            if (!this._selectedSongs.Contains(song))
            {
                this._selectedSongs.Add(song);
                return;
            }

            this._selectedSongs.Remove(song);
        }

        /// <summary>
        ///     Handles when the page back button is clicked.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed handling the back button clicked event.</returns>
        private async Task OnBackClickedCommand()
        {
            await Shell.Current.GoToAsync($"//{nameof(PlaylistsPage)}");
        }

        /// <summary>
        ///     Clears the selected songs.
        /// </summary>
        internal void ClearSelectedSongs()
        {
            this._selectedSongs.Clear();
        }

        /// <summary>
        ///     Loads all selectable songs to add to a new playlist.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed loading the selectable songs.</returns>
        public async Task LoadSelectableSongsAsync(CancellationToken cancellationToken = default)
        {
            this.IsBusy = true;

            cancellationToken.ThrowIfCancellationRequested();

            #region Testing

            //IEnumerable<Song> allSongs = new List<Song>
            //{
            //    new Song{ Name = "song 1" },
            //    new Song{ Name = "song 2" },
            //    new Song{ Name = "song 3" },
            //    new Song{ Name = "song 4" },
            //    new Song{ Name = "song 5" },
            //    new Song{ Name = "song 6" },
            //    new Song{ Name = "song 7" },
            //    new Song{ Name = "song 8" },
            //    new Song{ Name = "song 9" },
            //    new Song{ Name = "song 10" },
            //    new Song{ Name = "song 11" },
            //    new Song{ Name = "song 12" }
            //};

            #endregion

            IEnumerable<Song> allSongs = await this._songService.GetAllSongsAsync(cancellationToken).ConfigureAwait(false);
            List<Song> songsList = allSongs.ToList();
            this.AllSelectableSongs = songsList;
        }

        /// <summary>
        ///     Creates a new playlist.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed creating the new playlist.</returns>
        public async Task CreateNewPlaylistAsync(CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(this._newPlaylistName))
            {
                this._androidToastService.DisplayToastMessage("Playlist name required");
                return;
            }

            Regex regex = new Regex("[a-zA-Z0-9]+");
            if (!regex.IsMatch(this._newPlaylistName))
            {
                this._androidToastService.DisplayToastMessage("Alphanumeric characters only");
                return;
            }

            cancellationToken.ThrowIfCancellationRequested();

            this._newPlaylist = new Playlist
            {
                Name = this._newPlaylistName,
                Songs = this._selectedSongs
            };

            string playlistJson = JsonConvert.SerializeObject(this._newPlaylist);
            string fileName = this._newPlaylistName + ".json";

            //await this._fileService.SavePlaylistAsync(fileName, playlistJson).ConfigureAwait(false);

            await this._navigationService.NavigateToPageAsync(nameof(PlaylistsPage)).ConfigureAwait(false);
        }
    }
}