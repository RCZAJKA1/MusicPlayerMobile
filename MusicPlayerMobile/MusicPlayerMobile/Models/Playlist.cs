namespace MusicPlayerMobile.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     The playlist model.
    /// </summary>
    public sealed class Playlist
    {
        /// <summary>
        ///     Gets and sets the playlist identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets and sets the playlist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets and sets the playlist songs.
        /// </summary>
        public List<Song> Songs { get; set; }
    }
}
