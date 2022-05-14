namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Android.Media;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;
    using MusicPlayerMobile.Views;

    using Xamarin.Forms;

    /// <summary>
    ///     The songs view model.
    /// </summary>
    public class SongsViewModel : BaseViewModel, ISongsPage
    {
        /// <summary>
        ///     The song service.
        /// </summary>
        private readonly ISongService songService;

        /// <summary>
        ///     The selected song.
        /// </summary>
        private Song _selectedSong;

        /// <summary>
        ///     All songs.
        /// </summary>
        private List<Song> _allSongs;

        /// <summary>
        ///     The played songs.
        /// </summary>
        private List<int> _songHistory;

        /// <summary>
        ///     The media player.
        /// </summary>
        private readonly MediaPlayer _mediaPlayer;

        /// <summary>
        ///     The song history pointer.
        /// </summary>
        private int songHistoryPtr;

        /// <summary>
        ///     Creates a new instance of the <see cref="SongsViewModel"/> class.
        /// </summary>
        public SongsViewModel()
        {
            this.Title = "Songs";

            //List<Song> songs = new List<Song>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Song s = new Song
            //    {
            //        Id = i,
            //        Name = $"song{i}",
            //        FilePath = $"filepath_song{i}"
            //    };
            //    songs.Add(s);
            //}

            this.Songs = new List<Song>();
            ;
            this.SongHistory = new List<int>();
            this.SongTapped = new Command<Song>(this.OnSongSelected);
            this.songService = DependencyService.Get<ISongService>() ?? throw new InvalidOperationException("Unable to get dependency for ISongService.");
            this._mediaPlayer = new MediaPlayer();
            this.songHistoryPtr = -1;
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

        /// <inheritdoc />
        public Song SelectedSong
        {
            get => this._selectedSong;
            set => this.SetProperty(ref this._selectedSong, value);
        }

        /// <inheritdoc />
        public List<int> SongHistory
        {
            get => this._songHistory;
            set => this.SetProperty(ref this._songHistory, value);
        }

        /// <summary>
        ///     Loads all songs from the device.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed loading the songs.</returns>
        private async Task LoadAllSongs()
        {
            this.IsBusy = true;

            try
            {
                if (this._allSongs.Any())
                {
                    return;
                }

                IEnumerable<Song> songs = await this.songService.GetAllSongsAsync().ConfigureAwait(false);
                this.Songs.AddRange(songs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        /// <summary>
        ///     Configures the view model when the form is appearing.
        /// </summary>
        internal async Task OnAppearing()
        {
            this.IsBusy = true;
            this.SelectedSong = null;
            await this.LoadAllSongs();
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

            //this.PlaySong(song);

            this.SelectedSong = song;
            if (!this.SongHistory.Contains(song.Id))
            {
                this.SongHistory.Add(song.Id);
                this.songHistoryPtr++;
            }

            this._mediaPlayer.Reset();
            this._mediaPlayer.SetDataSource(this.SelectedSong.FilePath);
            this._mediaPlayer.Prepare();
            this._mediaPlayer.Start();
        }

        /// <summary>
        ///     Plays or pauses the media player.
        /// </summary>
        internal void PlayButtonClicked()
        {
            if (this._mediaPlayer.IsPlaying)
            {
                this._mediaPlayer.Pause();
                return;
            }

            if (this.SelectedSong != null)
            {
                this._mediaPlayer.Start();
                return;
            }
        }

        /// <summary>
        ///     Plays the previous song if it exists.
        /// </summary>
        internal void PlayPreviousSong()
        {
            if (this.songHistoryPtr < 1)
            {
                // TODO: notify user no more previous songs
                return;
            }

            this.songHistoryPtr--;
            int prevSongId = this.SongHistory[this.songHistoryPtr];
            Song prevSong = this.Songs.FirstOrDefault(s => s.Id == prevSongId);
            if (prevSong == null)
            {
                return;
            }

            //this.PlaySong(prevSong);

            this.SelectedSong = prevSong;

            this._mediaPlayer.Reset();
            this._mediaPlayer.SetDataSource(this.SelectedSong.FilePath);
            this._mediaPlayer.Prepare();
            this._mediaPlayer.Start();
        }

        /// <summary>
        ///     Plays the next song in the song history, otherwise plays a random song.
        /// </summary>
        internal void PlayNextSong()
        {
            if (this.SelectedSong.Id == this.SongHistory.Last())
            {
                this.PlayRandomSong();
                return;
            }

            // next song in history - only increment history pointer
            this.songHistoryPtr++;
            int nextSongId = this.SongHistory[this.songHistoryPtr];
            Song nextSong = this.Songs.FirstOrDefault(s => s.Id == nextSongId);
            if (nextSong == null)
            {
                return;
            }

            //this.PlaySong(nextSong);

            this.SelectedSong = nextSong;

            this._mediaPlayer.Reset();
            this._mediaPlayer.SetDataSource(this.SelectedSong.FilePath);
            this._mediaPlayer.Prepare();
            this._mediaPlayer.Start();
        }

        /// <summary>
        ///     Plays the specified song.
        /// </summary>
        /// <param name="song">The song.</param>
        //private void PlaySong(Song song)
        //{
        //    this.SelectedSong = song;
        //    this.SongHistory.Add(song.Id);
        //    this.songHistoryPtr++;

        //    this._mediaPlayer.Reset();
        //    this._mediaPlayer.SetDataSource(this.SelectedSong.FilePath);
        //    this._mediaPlayer.Prepare();
        //    this._mediaPlayer.Start();
        //}

        /// <summary>
        ///     Plays a random song from the song list.
        /// </summary>
        private void PlayRandomSong()
        {
            Random random = new Random();
            int randomSongId = random.Next(0, this.Songs.Count - 1);
            Song nextSong = this.Songs[randomSongId];

            this.SelectedSong = nextSong;
            this.SongHistory.Add(nextSong.Id);
            this.songHistoryPtr++;

            this._mediaPlayer.Reset();
            this._mediaPlayer.SetDataSource(this.SelectedSong.FilePath);
            this._mediaPlayer.Prepare();
            this._mediaPlayer.Start();
        }
    }
}