namespace MusicPlayerMobile.ViewModels
{
    using System.Collections.Generic;

    using MusicPlayerMobile.Models;

    using Xamarin.Forms;

    public class CreatePlaylistViewModel : BaseViewModel
    {
        /// <summary>
        ///     The new playlist.
        /// </summary>
        private Playlist _newPlaylist;

        /// <summary>
        ///     The selected songs.
        /// </summary>
        private readonly List<Song> _selectedSongs;

        /// <summary>
        ///     Creates a new instance of the <see cref="CreatePlaylistViewModel"/> class.
        /// </summary>
        public CreatePlaylistViewModel()
        {
            this.Title = "Create Playlist";

            this.SongSelectedCommand = new Command<Song>(this.OnSongSelectedCommand);
        }

        /// <summary>
        ///     Gets the song selected command.
        /// </summary>
        public Command SongSelectedCommand { get; }

        /// <summary>
        ///     Gets and sets all songs.
        /// </summary>
        public List<Song> Songs
        {
            get => this._allSongs;
            set => this.SetProperty(ref this._allSongs, value);
        }

        /// <summary>
        ///     Gets and sets the new playlist.
        /// </summary>
        public Playlist NewPlaylist
        {
            get => this._newPlaylist;
            set => this.SetProperty(ref this._newPlaylist, value);
        }

        private void OnSongSelectedCommand(Song song)
        {
            this._selectedSongs.Add(song);
        }
    }
}