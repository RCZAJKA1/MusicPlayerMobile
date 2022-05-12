namespace MusicPlayerMobile.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using MusicPlayerMobile.ViewModels;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public partial class SongsPage : ContentPage
    {
        /// <summary>
        ///     The songs view model.
        /// </summary>
        private readonly SongsViewModel songsViewModel;

        /// <summary>
        ///     Creates a new instance of the <see cref="SongsPage"/> class.
        /// </summary>
        public SongsPage()
        {
            this.InitializeComponent();

            this.BindingContext = this.songsViewModel = new SongsViewModel();
            this.songsViewModel = this.BindingContext as SongsViewModel;
        }

        /// <summary>
        ///     Prepares the view model while the page is appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.songsViewModel.OnAppearing();
        }

        private async void ButtonClicked_GetFiles(object sender, EventArgs e)
        {
            PermissionStatus checkStatusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (checkStatusRead != PermissionStatus.Granted)
            {
                PermissionStatus requestStatusRead = await Permissions.RequestAsync<Permissions.StorageRead>();
                if (requestStatusRead != PermissionStatus.Granted)
                {
                    this.Label_GetFiles.Text = "Read permissions not granted";
                    return;
                }
            }
            PermissionStatus checkStatusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (checkStatusWrite != PermissionStatus.Granted)
            {
                PermissionStatus requestStatusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (requestStatusWrite != PermissionStatus.Granted)
                {
                    this.Label_GetFiles.Text = "Write permissions not granted";
                    return;
                }
            }

            try
            {
                //ISongService songsRepo = DependencyService.Get<ISongService>();
                //IEnumerable<Song> songs = await songsRepo.GetAllSongsAsync();
                if (!Directory.Exists(Constants.AndroidFolderPathMusic))
                {
                    this.Label_GetFiles.Text = "Path does not exist";
                }

                IEnumerable<string> songFiles = Directory.EnumerateFiles(Constants.AndroidFolderPathMusic);
            }
            catch (Exception ex)
            {
                this.Label_GetFiles.Text = $"{ex.Message}";
            }
        }

        private async void ButtonClicked_GetLocation(object sender, EventArgs e)
        {
            PermissionStatus checkStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (checkStatus != PermissionStatus.Granted)
            {
                PermissionStatus requestStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (requestStatus != PermissionStatus.Granted)
                {
                    this.Label_GetLocation.Text = "Permissions not granted";
                    return;
                }
            }

            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location == null)
                {
                    this.Label_GetLocation.Text = $"Location unknown";
                }

                this.Label_GetLocation.Text = $"{location.Latitude} {location.Longitude}";
            }
            catch (Exception ex)
            {
                this.Label_GetLocation.Text = $"{ex.Message}";
            }
        }

        private async void ButtonClicked_WriteFile(object sender, EventArgs e)
        {
            PermissionStatus checkStatusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (checkStatusWrite != PermissionStatus.Granted)
            {
                PermissionStatus requestStatusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (requestStatusWrite != PermissionStatus.Granted)
                {
                    this.Label_GetFiles.Text = "Write permissions not granted";
                    return;
                }
            }

            try
            {
                if (!Directory.Exists(Constants.AndroidFolderPathMusic))
                {
                    this.Label_WriteFile.Text = "Path does not exist";
                }
            }
            catch (Exception ex)
            {
                this.Label_WriteFile.Text = $"{ex.Message}";
            }
        }
    }
}