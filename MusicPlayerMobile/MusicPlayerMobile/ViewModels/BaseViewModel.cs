namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Android.Media;
    using Android.Widget;

    using MusicPlayerMobile.Models;

    using Xamarin.Forms;

    /// <summary>
    ///     The base view model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The is busy.
        /// </summary>
        private bool _isBusy = false;

        /// <summary>
        ///     The title.
        /// </summary>
        private string _title = string.Empty;

        /// <summary>
        ///     The selected song.
        /// </summary>
        private Song _selectedSong;

        /// <summary>
        ///     The media player.
        /// </summary>
        private MediaPlayer _mediaPlayer;

        /// <summary>
        ///     All songs.
        /// </summary>
        private List<Song> _allSongs;

        /// <summary>
        ///     The playlist songs.
        /// </summary>
        private List<Song> _playlistSongs;

        /// <summary>
        ///     The song history pointer.
        /// </summary>
        private int _songHistoryPtr;

        /// <summary>
        ///     The now playing label text.
        /// </summary>
        private string _nowPlayingLabelText;

        /// <summary>
        ///     The song history.
        /// </summary>
        private List<int> _songHistory;

        /// <summary>
        ///     Creates a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        public BaseViewModel()
        {
            this.SongHistory = new List<int>();
            this.SongTappedCommand = new Command<Song>(this.OnSongSelected);
            this.PrevButtonClickedCommand = new Command(this.OnPrevButtonClicked);
            this.PlayButtonClickedCommand = new Command(this.OnPlayButtonClicked);
            this.NextButtonClickedCommand = new Command(this.OnNextButtonClicked);
        }

        /// <summary>
        ///     Gets and sets the is busy.
        /// </summary>
        protected bool IsBusy
        {
            get => this._isBusy;
            set => this.SetProperty(ref this._isBusy, value);
        }

        /// <summary>
        ///     Gets and sets the title.
        /// </summary>
        public string Title
        {
            get => this._title;
            set => this.SetProperty(ref this._title, value);
        }

        /// <summary>
        ///     Gets and sets the now playing label text.
        /// </summary>
        public string NowPlayingLabelText
        {
            get => this._nowPlayingLabelText;
            set => this.SetProperty(ref this._nowPlayingLabelText, value);
        }

        /// <summary>
        ///     Gets and sets the selected song.
        /// </summary>
        protected Song SelectedSong
        {
            get => this._selectedSong;
            set => this.SetProperty(ref this._selectedSong, value);
        }

        /// <summary>
        ///     Gets and sets the media player.
        /// </summary>
        protected MediaPlayer MediaPlayer
        {
            get => this._mediaPlayer;
            set => this.SetProperty(ref this._mediaPlayer, value);
        }

        /// <summary>
        ///     Gets and sets the song history pointer.
        /// </summary>
        protected int SongHistoryPtr
        {
            get => this._songHistoryPtr;
            set => this.SetProperty(ref this._songHistoryPtr, value);
        }

        /// <summary>
        ///     Gets and sets the song history.
        /// </summary>
        protected List<int> SongHistory
        {
            get => this._songHistory;
            set => this.SetProperty(ref this._songHistory, value);
        }

        /// <summary>
        ///     Gets and sets the playlist songs.
        /// </summary>
        protected List<Song> PlaylistSongs
        {
            get => this._playlistSongs;
            set => this.SetProperty(ref this._playlistSongs, value);
        }

        /// <summary>
        ///     Gets and sets all songs.
        /// </summary>
        public List<Song> AllSongs
        {
            get => this._allSongs;
            set => this.SetProperty(ref this._allSongs, value);
        }

        /// <summary>
        ///     Gets the song tapped command.
        /// </summary>
        public Command<Song> SongTappedCommand { get; set; }

        /// <summary>
        ///     Gets the previous button clicked command.
        /// </summary>
        public Command PrevButtonClickedCommand { get; set; }

        /// <summary>
        ///     Gets the play button clicked command.
        /// </summary>
        public Command PlayButtonClickedCommand { get; set; }

        /// <summary>
        ///     Gets the next button clicked command.
        /// </summary>
        public Command NextButtonClickedCommand { get; set; }

        /// <summary>
        ///     Sets the referenced property using the specified value.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="backingStore">The backing store.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="onChanged">The on changed action.</param>
        /// <returns><c>true</c> if the property was successfully changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
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

        /// <summary>
        ///     Plays the previous song, if it exists, when the previous button is clicked.
        /// </summary>
        private void OnPrevButtonClicked()
        {
            if (this._songHistoryPtr < 1)
            {
                Toast noPrevSongMsg = Toast.MakeText(Android.App.Application.Context, "No previous songs", ToastLength.Short);
                noPrevSongMsg.Show();
                return;
            }

            this._songHistoryPtr--;
            int prevSongId = this.SongHistory[this._songHistoryPtr];
            Song prevSong = this._allSongs.FirstOrDefault(s => s.Id == prevSongId);
            if (prevSong == null)
            {
                return;
            }

            this.PlaySong(prevSong);
        }

        /// <summary>
        ///     Plays and pauses the media player.
        /// </summary>
        private void OnPlayButtonClicked()
        {
            if (this._mediaPlayer.IsPlaying)
            {
                this._mediaPlayer.Pause();
                return;
            }

            this._mediaPlayer.Start();
        }

        /// <summary>
        ///     Plays the next song from history, if it exists, otherwise a random song, when the next button is clicked.
        /// </summary>
        private void OnNextButtonClicked()
        {
            if (this.SelectedSong == null || this.SelectedSong.Id == this.SongHistory.Last())
            {
                this.PlayRandomSong();
                return;
            }

            // next song in history - only increment history pointer
            this._songHistoryPtr++;
            int nextSongId = this.SongHistory[this._songHistoryPtr];
            Song nextSong = this._allSongs.FirstOrDefault(s => s.Id == nextSongId);
            if (nextSong == null)
            {
                return;
            }

            this.PlaySong(nextSong);
        }

        /// <summary>
        ///     Plays a random song from the song list.
        /// </summary>
        private void PlayRandomSong()
        {
            Random random = new Random();
            int randomSongId = random.Next(0, this._allSongs.Count - 1);
            Song nextSong = this._allSongs[randomSongId];

            this.SongHistory.Add(nextSong.Id);
            this._songHistoryPtr++;

            this.PlaySong(nextSong);
        }

        /// <summary>
        ///     Plays the specified song.
        /// </summary>
        /// <param name="song">The song to be played.</param>
        private void PlaySong(Song song)
        {
            this.SelectedSong = song ?? throw new ArgumentNullException(nameof(song));

            this._mediaPlayer.Reset();
            this._mediaPlayer.SetDataSource(this.SelectedSong.FilePath);
            this._mediaPlayer.Prepare();
            this._mediaPlayer.Start();

            Device.BeginInvokeOnMainThread(() =>
            {
                this.NowPlayingLabelText = song.Name;
            });
        }

        #region INotifyPropertyChanged

        /// <summary>
        ///     The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Executes upon the property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
