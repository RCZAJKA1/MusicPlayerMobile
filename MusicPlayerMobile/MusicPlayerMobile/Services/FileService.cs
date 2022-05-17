namespace MusicPlayerMobile.Services
{
    using System;
    using System.IO;
    using System.Text;
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
            if (playlistName == null)
            {
                throw new ArgumentNullException(nameof(playlistName));
            }
            if (playlistName.Trim() == string.Empty)
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(playlistName));
            }
            if (contents == null)
            {
                throw new ArgumentNullException(nameof(playlistName));
            }
            if (contents.Trim() == string.Empty)
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(contents));
            }

            cancellationToken.ThrowIfCancellationRequested();

            string filePath = Path.Combine(FolderPaths.PlaylistsFolderPath, playlistName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                byte[] encodedText = Encoding.Unicode.GetBytes(contents);
                await fileStream.WriteAsync(encodedText, 0, contents.Length);
            }
        }
    }
}
