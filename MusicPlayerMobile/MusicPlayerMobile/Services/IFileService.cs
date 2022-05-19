namespace MusicPlayerMobile.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Handles file I/O operations.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        ///     Saves a playlist using the specified file name and contents.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="contents">The file contents.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed saving the playlist to the device.</returns>
        Task SavePlaylistAsync(string fileName, string contents, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates the playlist folder on the device.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed creating the playlist folder.</returns>
        Task CreatePlaylistFolderAsync(CancellationToken cancellationToken = default);
    }
}