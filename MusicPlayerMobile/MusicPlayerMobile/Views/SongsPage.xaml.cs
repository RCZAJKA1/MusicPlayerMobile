namespace MusicPlayerMobile.Views
{
    using System;
    using System.Collections.Generic;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    ///     The code behind for the page <see cref="SongsPage"/>.
    /// </summary>
    public partial class SongsPage : ContentPage, ISongsPage
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

        /// <inheritdoc />
        public Song SelectedSong { get; set; }

        /// <inheritdoc />
        public List<int> SongHistory { get; set; }

        /// <summary>
        ///     Prepares the view model while the page is appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this.songsViewModel.OnAppearing();
        }

        /// <summary>
        ///     Handles the play button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            this.songsViewModel.PlayButtonClicked();

            // TODO: use icons instead of text & update image sources
            switch (this.PlayButton.Text)
            {
                case "Play":
                    this.PlayButton.Text = "Pause";
                    break;
                case "Pause":
                    this.PlayButton.Text = "Play";
                    break;
                default:
                    throw new InvalidOperationException("The play button text must only be 'Play' or 'Pause'.");
            }
        }

        /// <summary>
        ///     Handles the previous button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void PrevButton_Clicked(object sender, EventArgs e)
        {
            this.songsViewModel.PlayPreviousSong();
        }

        /// <summary>
        ///     Handles the next button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void NextButton_Clicked(object sender, EventArgs e)
        {
            this.songsViewModel.PlayNextSong();
        }
    }
}