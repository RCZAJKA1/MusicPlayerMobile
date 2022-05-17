namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
        private List<Playlist> _playlists;

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
            for (int i = 0; i < 20; i++)
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
            get => this._playlists;
            set => this.SetProperty(ref this._playlists, value);
        }

        /// <summary>
        ///     Navigates to the create playlist page when the add button is clicked.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed navigating to the <see cref="CreatePlaylistPage"/>.</returns>
        private async Task OnAddPlaylistClickedAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Shell.Current.GoToAsync($"//{nameof(CreatePlaylistPage)}");
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