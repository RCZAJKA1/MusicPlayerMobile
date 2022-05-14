namespace MusicPlayerMobile.Models
{
    /// <summary>
    ///     The song entity.
    /// </summary>
    public sealed class Song
    {
        /// <summary>
        ///     Gets and sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets and sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets and sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets and sets the length.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        ///     Gets and sets the file path.
        /// </summary>
        public string FilePath { get; set; }
    }
}
