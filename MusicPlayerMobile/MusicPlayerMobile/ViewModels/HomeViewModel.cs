namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using MusicPlayerMobile.Models;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    ///     The home view model.
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>
        ///     The selected song.
        /// </summary>
        private Song _selectedSong;

        /// <summary>
        ///     Creates a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        public HomeViewModel()
        {
            this.Title = "Home";
            this.OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

            this.Songs = new ObservableCollection<Song>();
            this.LoadSongsCommand = new Command(async () => await this.ExecuteLoadSongsCommand());

            this.SongTapped = new Command<Song>(this.OnSongSelected);

            if (Directory.Exists(Constants.AndroidMusicFolderPath))
            {
                try
                {
                    //bool isReadonly = Environment.MediaMountedReadOnly.Equals(Environment.ExternalStorageState);

                    //string[] songFiles = Directory.GetFiles(Constants.AndroidMusicFolderPath);
                    IEnumerable<string> test = Directory.EnumerateFiles(Constants.AndroidMusicFolderPath);

                    //int id = 1;
                    //foreach (string file in songFiles)
                    //{
                    //    Song song = new Song()
                    //    {
                    //        Id = id++,
                    //        Name = file
                    //    };

                    //    this.Songs.Add(song);
                    //}
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        /// <summary>
        ///     Gets the open web command.
        /// </summary>
        public ICommand OpenWebCommand { get; }

        /// <summary>
        ///     Gets the songs.
        /// </summary>
        public ObservableCollection<Song> Songs { get; }

        /// <summary>
        ///     Gets the load songs command.
        /// </summary>
        public Command LoadSongsCommand { get; }

        /// <summary>
        ///     Gets the song tapped command.
        /// </summary>
        public Command<Song> SongTapped { get; }

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
        ///     Loads the songs.
        /// </summary>
        /// <returns>The <see cref="Task"/> that completed loading the songs.</returns>
        async Task ExecuteLoadSongsCommand()
        {
            this.IsBusy = true;

            try
            {
                this.Songs.Clear();
                IEnumerable<Song> songs = await this.DataStore.GetAllSongsAsync();
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
        public void OnAppearing()
        {
            this.IsBusy = true;
            this.SelectedSong = null;
        }


        /// <summary>
        ///     Handles when the song selected event is fired.
        /// </summary>
        /// <param name="song">The song.</param>
        async void OnSongSelected(Song song)
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