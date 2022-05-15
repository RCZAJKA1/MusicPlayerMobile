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
    using MusicPlayerMobile.Views;

    using Xamarin.Forms;

    /// <summary>
    ///     The playlists view model.
    /// </summary>
    internal sealed class PlaylistsViewModel : BaseViewModel
    {
        /// <summary>
        ///     The playlists.
        /// </summary>
        private List<Playlist> _allPlaylists;

        /// <summary>
        ///     The song service.
        /// </summary>
        private readonly ISongService _songService;

        /// <summary>
        ///     Creates a new instance of the <see cref="PlaylistsViewModel"/> class.
        /// </summary>
        public PlaylistsViewModel()
        {
            this.Title = "Playlists";

            this.AddPlaylistCommand = new Command(async () => await this.OnAddPlaylistClickedAsync());
            this._songService = DependencyService.Get<ISongService>() ?? throw new InvalidOperationException("Unable to get dependency ISongService.");

            #region Testing

            List<Playlist> playlists = new List<Playlist>();
            for (int i = 0; i < 10; i++)
            {
                Playlist song = new Playlist()
                {
                    Id = i,
                    Name = $"playlist{i}",
                    Songs = new List<Song>
                    {
                        new Song
                        {
                            Id = 1,
                            Name = $"playlist{i}_song",
                            FilePath = "testFilePath"
                        }
                    }
                };

                playlists.Add(song);
            }

            #endregion

            this.Playlists = playlists;
        }

        /// <summary>
        ///     Gets the add playlist command.
        /// </summary>
        public Command AddPlaylistCommand { get; }

        /// <summary>
        ///     Gets and sets all playlists.
        /// </summary>
        public List<Playlist> Playlists
        {
            get => this._allPlaylists;
            set => this.SetProperty(ref this._allPlaylists, value);
        }

        /// <summary>
        ///     Adds a playlist when the add button is clicked.
        /// </summary>
        /// <returns></returns>
        private async Task OnAddPlaylistClickedAsync()
        {
            // open new page to select songs into playlist
            await Shell.Current.GoToAsync($"//{nameof(CreatePlaylistPage)}");

            // create playlist

            // save playlist to device - application local storage
        }

        /// <summary>
        ///     Loads all playlists from the device.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed loading the playlists.</returns>
        private async Task LoadAllPlaylistsAsync(CancellationToken cancellationToken = default)
        {
            this.IsBusy = true;

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                if (this._allPlaylists.Any())
                {
                    return;
                }

                IEnumerable<Playlist> playlists = await this._songService.GetAllPlaylistsAsync(cancellationToken).ConfigureAwait(false);
                this.Playlists.AddRange(playlists);
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
        public async Task OnAppearingAsync()
        {
            await this.LoadAllPlaylistsAsync();
        }
    }
}