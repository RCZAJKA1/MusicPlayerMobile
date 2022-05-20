namespace MusicPlayerMobile.Services
{
    using System.Collections.Generic;
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

        /// <summary>
        ///     Determines if the specified directory exists.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if the specified directory exists, otherwise <c>false</c>.</returns>
        Task<bool> DoesDirectoryExistAsync(string directoryPath, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all file names from the specified directory path.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task{TResult}"/> that completed getting the files names.</returns>
        Task<IEnumerable<string>> GetFileNamesAsync(string directoryPath, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets the file name of the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task{TResult}"/> that completed getting the file name.</returns>
        Task<string> GetFileNameAsync(string filePath, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Reads all text from the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task{TResult}"/> that completed reading all text.</returns>
        Task<string> ReadAllTextAsync(string filePath, CancellationToken cancellationToken = default);
    }
}