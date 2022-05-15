namespace MusicPlayerMobile
{
    /// <summary>
    ///     Constant values for mobile ease of use.
    /// </summary>
    public sealed class Constants
    {
        /// <summary>
        ///     The base android folder path for external files.
        /// </summary>
        private const string AndroidExternalStoragePath = "/storage/emulated/0";

        /// <summary>
        ///     The android folder path for music files in external storage.
        /// </summary>
        public const string AndroidExternalMusicFolderPath = AndroidExternalStoragePath + "/Music";
    }
}
