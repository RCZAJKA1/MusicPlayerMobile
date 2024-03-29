﻿namespace MusicPlayerMobile.Views
{
    using System;

    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    ///     The code behind for the <see cref="CreatePlaylistPage"/> view.
    /// </summary>
    public partial class CreatePlaylistPage : ContentPage
    {
        /// <summary>
        ///     The create playlists view model.
        /// </summary>
        private readonly CreatePlaylistViewModel _createPlaylistsViewModel;

        /// <summary>
        ///     Creates a new instance of the <see cref="CreatePlaylistPage"/>.
        /// </summary>
        public CreatePlaylistPage()
        {
            this.InitializeComponent();

            this.BindingContext = this._createPlaylistsViewModel = new CreatePlaylistViewModel();
            this._createPlaylistsViewModel = this.BindingContext as CreatePlaylistViewModel;
        }

        /// <summary>
        ///     Handles the create button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private async void CreateButton_Clicked(object sender, EventArgs e)
        {
            await this._createPlaylistsViewModel.CreateNewPlaylistAsync();
        }

        /// <summary>
        ///     Handles the clear button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private async void ClearButton_Clicked(object sender, EventArgs e)
        {
            string message = $"Would you like to clear all selected songs?";
            bool clearSongs = await this.DisplayAlert("Clear Selection?", message, "Yes", "No");

            if (clearSongs)
            {
                this._createPlaylistsViewModel.ClearSelectedSongs();
                this.SelectableSongsCollection.SelectedItems = null;
            }
        }

        /// <summary>
        ///     Prepares the create playlist view model while the page is appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this._createPlaylistsViewModel.LoadSelectableSongsAsync().ConfigureAwait(false);
        }
    }
}