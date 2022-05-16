namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;

    using Xamarin.Forms;

    public class CreatePlaylistViewModel : BaseViewModel
    {

        private readonly ISongService _songService;

        /// <summary>
        ///     The new playlist.
        /// </summary>
        private Playlist _newPlaylist;

        /// <summary>
        ///     The playlist name.
        /// </summary>
        private string _playlistName;

        /// <summary>
        ///     The selected songs.
        /// </summary>
        private readonly List<Song> _selectedSongs;

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
            this.AllSelectableSongs = new List<Song>();
            this._selectedSongs = new List<Song>();
            this._songService = DependencyService.Get<ISongService>() ?? throw new InvalidOperationException("Unable to get dependency ISongService");
        }

        /// <summary>
        ///     Gets the song selected command.
        /// </summary>
        public Command SongSelectedCommand { get; }

        /// <summary>
        ///     Gets and sets all songs.
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
        ///     Gets and sets the new playlist name.
        /// </summary>
        public string PlaylistName
        {
            get => this._playlistName;
            set => this.SetProperty(ref this._playlistName, value);
        }

        private void OnSongSelectedCommand(Song song)
        {
            this._selectedSongs.Add(song);
        }

        public async Task OnAppearingAsync(CancellationToken cancellationToken = default)
        {
            this.IsBusy = true;

            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<Song> allSongs = new List<Song>
            {
                new Song{ Name = "song 1" },
                new Song{ Name = "song 2" },
                new Song{ Name = "song 3" }
            };

            //IEnumerable<Song> allSongs = await this._songService.GetAllSongsAsync(cancellationToken).ConfigureAwait(false);
            List<Song> songsList = allSongs.ToList();
            this.AllSelectableSongs = songsList;
        }
    }
}