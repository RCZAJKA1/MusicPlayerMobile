namespace MusicPlayerMobile
{
    using System.IO;

    /// <summary>
    ///     Constant values for mobile ease of use.
    /// </summary>
    public sealed class Constants
    {
        /// <summary>
        ///     The base android folder path for external files.
        /// </summary>
        private const string AndroidBaseFolderPath = "/storage/emulated/0";

        /// <summary>
        ///     The android folder path for external music files.
        /// </summary>
        public readonly string AndroidFolderPathMusic = Path.Combine(AndroidBaseFolderPath, "Music");

        /// <summary>
        ///     The android folder path for external download files.
        /// </summary>
        public readonly string AndroidFolderPathDownloads = Path.Combine(AndroidBaseFolderPath, "Download");
    }
}
