namespace MusicPlayerMobile.Views
{
    using System.Collections.Generic;

    using MusicPlayerMobile.Models;

    /// <summary>
    ///     Handles UI components on the <see cref="SongsPage"/>.
    /// </summary>
    internal interface ISongsPage
    {
        /// <summary>
        ///     Gets and sets the selected song.
        /// </summary>
        Song SelectedSong { get; set; }

        /// <summary>
        ///     Gets and sets the played songs list.
        /// </summary>
        List<int> SongHistory { get; set; }

    }
}