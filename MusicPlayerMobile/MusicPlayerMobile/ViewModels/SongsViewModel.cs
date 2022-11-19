namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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

            //List<Song> songList = new List<Song>();
            //for (int i = 1; i < 11; i++)
            //{
            //    Song song = new Song()
            //    {
            //        Id = i,
            //        Name = $"song{i}",
            //        FilePath = $"folder/song{i}.mp3"
            //    };

            //    songList.Add(song);
            //}
            //this.SelectedSong = songList[0];
            //this.NowPlayingLabelText = this.SelectedSong.Name;
            //this.AllSongs = songList;

            #endregion

            this.AllSongs = new List<Song>();
            this.SongHistoryPtr = -1;
        }

        /// <summary>
        ///     Loads all songs from the device.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed loading the songs.</returns>
        private async Task LoadAllSongs(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                if (this.AllSongs.Any())
                {
                    return;
                }

                IEnumerable<Song> songs = await this._songService.GetAllSongsAsync(cancellationToken).ConfigureAwait(false);
                this.AllSongs.AddRange(songs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        ///     Prepares the view model while the page is appearing.
        /// </summary>
        public async Task OnAppearingAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await this.LoadAllSongs(cancellationToken);
        }
    }
}