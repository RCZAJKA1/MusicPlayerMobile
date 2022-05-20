namespace MusicPlayerMobile.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <inheritdoc cref="IFileService"/>
    internal sealed class FileService : IFileService
    {
        /// <inheritdoc/>
        public async Task CreatePlaylistFolderAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Task.FromResult(Directory.CreateDirectory(FolderPaths.PlaylistsFolderPath));
        }

        /// <inheritdoc/>
        public async Task SavePlaylistAsync(string playlistName, string contents, CancellationToken cancellationToken = default)
        {
            playlistName.ThrowIfNull(nameof(playlistName));
            playlistName.ThrowIfEmptyOrWhiteSpace(nameof(playlistName));
            playlistName.ThrowIfNull(nameof(contents));
            playlistName.ThrowIfEmptyOrWhiteSpace(nameof(contents));

            cancellationToken.ThrowIfCancellationRequested();

            string filePath = Path.Combine(FolderPaths.PlaylistsFolderPath, playlistName);
            await File.WriteAllTextAsync(filePath, contents, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<bool> DoesDirectoryExistAsync(string directoryPath, CancellationToken cancellationToken = default)
        {
            directoryPath.ThrowIfNull(nameof(directoryPath));
            directoryPath.ThrowIfEmptyOrWhiteSpace(nameof(directoryPath));

            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(Directory.Exists(directoryPath));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetFileNamesAsync(string directoryPath, CancellationToken cancellationToken = default)
        {
            directoryPath.ThrowIfNull(nameof(directoryPath));
            directoryPath.ThrowIfEmptyOrWhiteSpace(nameof(directoryPath));

            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(Directory.EnumerateFiles(directoryPath));
        }

        /// <inheritdoc/>
        public async Task<string> GetFileNameAsync(string filePath, CancellationToken cancellationToken = default)
        {
            filePath.ThrowIfNull(nameof(filePath));
            filePath.ThrowIfEmptyOrWhiteSpace(nameof(filePath));

            cancellationToken.ThrowIfCancellationRequested();

            return await Task.FromResult(Path.GetFileName(filePath));
        }

        /// <inheritdoc/>
        public async Task<string> ReadAllTextAsync(string filePath, CancellationToken cancellationToken = default)
        {
            filePath.ThrowIfNull(nameof(filePath));
            filePath.ThrowIfEmptyOrWhiteSpace(nameof(filePath));

            cancellationToken.ThrowIfCancellationRequested();

            return await File.ReadAllTextAsync(filePath, cancellationToken).ConfigureAwait(false);
        }
    }
}
