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
        private List<Playlist> _playlists;

        /// <summary>
        ///     The song service.
        /// </summary>
        private readonly ISongService _songService;

        /// <summary>
        ///     The selected playlist.
        /// </summary>
        private Playlist _selectedPlaylist;

        /// <summary>
        ///     Creates a new instance of the <see cref="PlaylistsViewModel"/> class.
        /// </summary>
        public PlaylistsViewModel()
        {
            this.Title = "Playlists";

            this._songService = DependencyService.Get<ISongService>() ?? throw new InvalidOperationException("Unable to get dependency ISongService.");

            this.AddPlaylistCommand = new Command(async () => await this.OnAddPlaylistClickedAsync());
            this.SelectPlaylistCommand = new Command<Playlist>(async (Playlist playlist) => await this.OnPlaylistClickedAsync(playlist));

            #region Testing

            //List<Playlist> playlists = new List<Playlist>();
            //for (int i = 0; i < 20; i++)
            //{
            //    Playlist song = new Playlist()
            //    {
            //        Id = i,
            //        Name = $"playlist{i}",
            //        Songs = new List<Song>
            //        {
            //            new Song
            //            {
            //                Id = 1,
            //                Name = $"playlist{i}_song",
            //                FilePath = "testFilePath"
            //            }
            //        }
            //    };

            //    playlists.Add(song);
            //}
            //this.Playlists = playlists;

            #endregion

            this.Playlists = new List<Playlist>();
        }

        /// <summary>
        ///     Gets the add playlist command. Public for xaml binding.
        /// </summary>
        public Command AddPlaylistCommand { get; }

        /// <summary>
        ///     Gets the select playlist command. Public for xaml binding.
        /// </summary>
        public Command SelectPlaylistCommand { get; }

        /// <summary>
        ///     Gets and sets all playlists. Public for xaml binding.
        /// </summary>
        public List<Playlist> Playlists
        {
            get => this._playlists;
            set => this.SetProperty(ref this._playlists, value);
        }

        /// <summary>
        ///     Gets and sets the selected playlist. Public for xaml binding.
        /// </summary>
        public Playlist SelectedPlaylist
        {
            get => this._selectedPlaylist;
            set => this.SetProperty(ref this._selectedPlaylist, value);
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
        ///     Loads the playlist songs to the songs page.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed navigating to the <see cref="SongsPage"/>.</returns>
        private async Task OnPlaylistClickedAsync(Playlist playlist, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.AllSongs = playlist.Songs;

            await Shell.Current.GoToAsync($"//{nameof(SongsPage)}");
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
                if (this._playlists.Any())
                {
                    return;
                }

                IEnumerable<Playlist> playlists = await this._songService.GetAllPlaylistsAsync(cancellationToken).ConfigureAwait(false);
                this._playlists.AddRange(playlists);
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