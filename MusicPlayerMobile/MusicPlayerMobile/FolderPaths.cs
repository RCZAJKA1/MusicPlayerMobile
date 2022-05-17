namespace MusicPlayerMobile
{
    using System;

    /// <summary>
    ///     Contains folder paths for device storage locations.
    /// </summary>
    public static class FolderPaths
    {
        /// <summary>
        ///     The base android folder path for external files.
        /// </summary>
        private static readonly string AndroidExternalStoragePath = "/storage/emulated/0";

        /// <summary>
        ///     The android folder path for music files in external storage.
        /// </summary>
        public static string MusicFolderPath = AndroidExternalStoragePath + "/Music";

        /// <summary>
        ///     The android folder path for playlist files in internal application storage.
        /// </summary>
        public static string PlaylistsFolderPath => Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Playlists";
    }
}
