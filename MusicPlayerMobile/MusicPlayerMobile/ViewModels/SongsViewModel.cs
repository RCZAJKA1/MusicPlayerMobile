namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.Media;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;

    using Xamarin.Forms;

    /// <summary>
    ///     The songs view model.
    /// </summary>
    public class SongsViewModel : BaseViewModel
    {
        /// <summary>
        ///     The song service.
        /// </summary>
        private readonly ISongService _songService;

        /// <summary>
        ///     The selected song.
        /// </summary>
        private Song _selectedSong;

        /// <summary>
        ///     All songs.
        /// </summary>
        private List<Song> _allSongs;

        /// <summary>
        ///     The media player.
        /// </summary>
        private readonly MediaPlayer _mediaPlayer;

        /// <summary>
        ///     The song history pointer.
        /// </summary>
        private int _songHistoryPtr;

        /// <summary>
        ///     Creates a new instance of the <see cref="SongsViewModel"/> class.
        /// </summary>
        public SongsViewModel()
        {
            this.Title = "Songs";
            this._songService = DependencyService.Get<ISongService>() ?? throw new InvalidOperationException("Unable to get dependency ISongService.");

            //List<Song> songList = new List<Song>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Song song = new Song()
            //    {
            //        Id = i,
            //        Name = $"song{i}",
            //        FilePath = $"folder/song{i}.mp3"
            //    };

            //    songList.Add(song);
            //}
            //this._selectedSong = songList[0];
            //this.NowPlayingLabelText = this._selectedSong.Name;

            this.Songs = new List<Song>();
            this.SongHistory = new List<int>();
            this.SongTapped = new Command<Song>(this.OnSongSelected);
            this._mediaPlayer = new MediaPlayer();
            this._songHistoryPtr = -1;
        }

        /// <summary>
        ///     Gets and sets all songs.
        /// </summary>
        public List<Song> Songs
        {
            get => this._allSongs;
            set => this.SetProperty(ref this._allSongs, value);
        }

        /// <summary>
        ///     Gets the song tapped command.
        /// </summary>
        public Command<Song> SongTapped { get; }

        /// <summary>
        ///     Gets and sets the song history.
        /// </summary>
        private List<int> SongHistory { get; set; }

        /// <inheritdoc/>
        public string NowPlayingLabelText { get; set; }

        /// <summary>
        ///     Loads all songs from the device.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed loading the songs.</returns>
        private async Task LoadAllSongs(CancellationToken cancellationToken = default)
        {
            this.IsBusy = true;

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                if (this._allSongs.Any())
                {
                    return;
                }

                IEnumerable<Song> songs = await this._songService.GetAllSongsAsync(cancellationToken).ConfigureAwait(false);
                this.Songs.AddRange(songs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        /// <inheritdoc/>
        public async Task OnAppearingAsync(CancellationToken cancellationToken = default)
        {
            this.IsBusy = true;
            this._selectedSong = null;

            cancellationToken.ThrowIfCancellationRequested();

            await this.LoadAllSongs(cancellationToken);
        }

        /// <summary>
        ///     Handles when the song selected event is fired.
        /// </summary>
        /// <param name="song">The song.</param>
        private void OnSongSelected(Song song)
        {
            if (song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            if (!this.SongHistory.Contains(song.Id))
            {
                this.SongHistory.Add(song.Id);
                this._songHistoryPtr++;
            }

            this.PlaySong(song);
        }

        /// <inheritdoc/>
        public void PlayButtonClicked()
        {
            if (this._mediaPlayer.IsPlaying)
            {
                this._mediaPlayer.Pause();
                return;
            }

            if (this._selectedSong != null)
            {
                this._mediaPlayer.Start();
                return;
            }
        }

        /// <inheritdoc/>
        public void PlayPreviousSong()
        {
            if (this._songHistoryPtr < 1)
            {
                // TODO: notify user no more previous songs
                return;
            }

            this._songHistoryPtr--;
            int prevSongId = this.SongHistory[this._songHistoryPtr];
            Song prevSong = this.Songs.FirstOrDefault(s => s.Id == prevSongId);
            if (prevSong == null)
            {
                return;
            }

            this.PlaySong(prevSong);
        }

        /// <inheritdoc/>
        public void PlayNextSong()
        {
            if (this._selectedSong.Id == this.SongHistory.Last())
            {
                this.PlayRandomSong();
                return;
            }

            // next song in history - only increment history pointer
            this._songHistoryPtr++;
            int nextSongId = this.SongHistory[this._songHistoryPtr];
            Song nextSong = this.Songs.FirstOrDefault(s => s.Id == nextSongId);
            if (nextSong == null)
            {
                return;
            }

            this.PlaySong(nextSong);
        }

        /// <summary>
        ///     Plays the specified song.
        /// </summary>
        /// <param name="song">The song to be played.</param>
        private void PlaySong(Song song)
        {
            this._selectedSong = song;

            this._mediaPlayer.Reset();
            this._mediaPlayer.SetDataSource(this._selectedSong.FilePath);
            this._mediaPlayer.Prepare();
            this._mediaPlayer.Start();
        }

        /// <summary>
        ///     Plays a random song from the song list.
        /// </summary>
        private void PlayRandomSong()
        {
            Random random = new Random();
            int randomSongId = random.Next(0, this.Songs.Count - 1);
            Song nextSong = this.Songs[randomSongId];

            this.SongHistory.Add(nextSong.Id);
            this._songHistoryPtr++;

            this.PlaySong(nextSong);
        }
    }
}