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
    internal sealed class SongsViewModel : BaseViewModel
    {
        /// <summary>
        ///     The song service.
        /// </summary>
        private readonly ISongService _songService;

        /// <summary>
        ///     Creates a new instance of the <see cref="SongsViewModel"/> class.
        /// </summary>
        public SongsViewModel()
        {
            this.Title = "Songs";
            this._songService = DependencyService.Get<ISongService>() ?? throw new InvalidOperationException("Unable to get dependency ISongService.");

            #region Testing

            List<Song> songList = new List<Song>();
            for (int i = 0; i < 10; i++)
            {
                Song song = new Song()
                {
                    Id = i,
                    Name = $"song{i}",
                    FilePath = $"folder/song{i}.mp3"
                };

                songList.Add(song);
            }
            this._selectedSong = songList[0];
            this.NowPlayingLabelText = this._selectedSong.Name;

            #endregion

            this.Songs = songList;
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

        /// <summary>
        ///     Prepares the view model while the page is appearing.
        /// </summary>
        public async Task OnAppearingAsync(CancellationToken cancellationToken = default)
        {
            this.IsBusy = true;
            this._selectedSong = null;

            cancellationToken.ThrowIfCancellationRequested();

            await this.LoadAllSongs(cancellationToken);
        }
    }
}