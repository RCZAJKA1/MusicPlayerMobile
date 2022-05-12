namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;

    using Xamarin.Forms;

    public class SongsViewModel : BaseViewModel
    {
        /// <summary>
        ///     The selected song.
        /// </summary>
        private Song _selectedSong;

        /// <summary>
        ///     All songs.
        /// </summary>
        private ObservableCollection<Song> _allSongs;

        public SongsViewModel()
        {
            this.Title = "Songs";
            //this.OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

            this.Songs = new ObservableCollection<Song>();
            //this.LoadSongsCommand = new Command(async () => await this.ExecuteLoadSongsCommand());

            this.SongTapped = new Command<Song>(this.OnSongSelected);
        }
        ///// <summary>
        /////     Gets the songs.
        ///// </summary>
        //public ObservableCollection<Song> Songs { get; }

        /// <summary>
        ///     Gets all songs.
        /// </summary>
        public ObservableCollection<Song> Songs
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
        ///     Loads the songs.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed loading the songs.</returns>
        private async Task LoadAllSongs()
        {
            this.IsBusy = true;

            try
            {
                this._allSongs.Clear();
                ISongService songsRepo = DependencyService.Get<ISongService>();
                IEnumerable<Song> songs = await songsRepo.GetAllSongsAsync();
                foreach (Song song in songs)
                {
                    this.Songs.Add(song);
                }
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
            //await this.LoadAllSongs();
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