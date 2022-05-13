﻿namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

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
        ///     Creates a new instance of the <see cref="SongsViewModel"/> class.
        /// </summary>
        public SongsViewModel()
        {
            this.Title = "Songs";
            this.Songs = new List<Song>();
            this.SongTapped = new Command<Song>(this.OnSongSelected);
            this.songService = DependencyService.Get<ISongService>();
        }

        /// <summary>
        ///     Gets all songs.
        /// </summary>
        public List<Song> Songs
        {
            get => this._allSongs;
            set => this.SetProperty(ref this._allSongs, value);
        }

        /// <summary>
        ///     Gets and sets the selected song.
        /// </summary>
        public Song SelectedSong
        {
            get => this._selectedSong;
            set
            {
                this.SetProperty(ref this._selectedSong, value);
                this.OnSongSelected(value);
            }
        }

        /// <summary>
        ///     Gets the load songs command.
        /// </summary>
        public Command LoadSongsCommand { get; }

        /// <summary>
        ///     Gets the song tapped command.
        /// </summary>
        public Command<Song> SongTapped { get; }

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

                this._allSongs.Clear();
                IEnumerable<Song> songs = await this.songService.GetAllSongsAsync();
                songs.OrderBy(x => x.Name);
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
                return;
            }

            //// This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}