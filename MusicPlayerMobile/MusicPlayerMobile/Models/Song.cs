namespace MusicPlayerMobile.Models
{
    /// <summary>
    ///     The song model.
    /// </summary>
    public sealed class Song
    {
        /// <summary>
        ///     Gets and sets the song identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets and sets the song name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets and sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets and sets the song file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///     Gets or sets the is selected.
        /// </summary>
        public bool? IsSelected { get; set; }
    }
}
