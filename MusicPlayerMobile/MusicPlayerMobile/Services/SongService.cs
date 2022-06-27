namespace MusicPlayerMobile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;

    using Newtonsoft.Json;

    using Xamarin.Forms;

    /// <inheritdoc cref="ISongService"/>
    public sealed class SongService : ISongService
    {
        /// <summary>
        ///     The file service.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        ///     Creates a new instance of the <see cref="SongService"/> class.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public SongService()
        {
            this._fileService = DependencyService.Get<IFileService>() ?? throw new InvalidOperationException("Unable to get dependency IFileService.");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Song>> GetAllSongsAsync(CancellationToken cancellationToken = default)
        {
            bool directoryExists = await this._fileService.DoesDirectoryExistAsync(FolderPaths.MusicFolderPath, cancellationToken).ConfigureAwait(false);
            if (!directoryExists)
            {
                throw new InvalidOperationException($"The directory '{FolderPaths.MusicFolderPath}' does not exist.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<Song> allSongs = new List<Song>();
            IEnumerable<string> songsEnumerable = await this._fileService.GetFileNamesAsync(FolderPaths.MusicFolderPath).ConfigureAwait(false);
            if (songsEnumerable == null)
            {
                throw new InvalidOperationException("The songs could not be retrieved from the device storage.");
            }
            List<string> songFiles = songsEnumerable.ToList();
            songFiles.Sort();
            for (int i = 0; i < songFiles.Count; i++)
            {
                string filePath = songFiles[i];
                string fileName = Path.GetFileName(filePath);
                string songNameWithArtist = Path.GetFileNameWithoutExtension(fileName);
                Song song = new Song
                {
                    Id = i,
                    Name = songNameWithArtist,
                    FilePath = songFiles[i]
                };

                allSongs.Add(song);
            }

            return await Task.FromResult(allSongs);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Playlist>> GetAllPlaylistsAsync(CancellationToken cancellationToken = default)
        {
            bool directoryExists = await this._fileService.DoesDirectoryExistAsync(FolderPaths.PlaylistsFolderPath, cancellationToken).ConfigureAwait(false);
            if (!directoryExists)
            {
                await this._fileService.CreatePlaylistFolderAsync().ConfigureAwait(false);
                return await Task.FromResult(new List<Playlist>());
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<string> playlistFileNames = await this._fileService.GetFileNamesAsync(FolderPaths.PlaylistsFolderPath, cancellationToken).ConfigureAwait(false) as List<string>;
            if (playlistFileNames?.Count == 0)
            {
                return await Task.FromResult(new List<Playlist>());
            }

            List<Playlist> allPlaylists = new List<Playlist>();
            playlistFileNames.Sort();
            for (int i = 0; i < playlistFileNames.Count; i++)
            {
                string filePath = playlistFileNames[i];
                Playlist playlist = await this.GetPlaylistFromFileAsync(filePath, cancellationToken).ConfigureAwait(false);

                allPlaylists.Add(playlist);
            }

            return await Task.FromResult(allPlaylists);
        }

        /// <summary>
        ///     Reads the specified file and returns the converted <see cref="Playlist"/> object.
        /// </summary>
        /// <param name="playlistFilePath">The playlist file path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Playlist"/>.</returns>
        private async Task<Playlist> GetPlaylistFromFileAsync(string playlistFilePath, CancellationToken cancellationToken)
        {
            string playlistContentsJson = await this._fileService.ReadAllTextAsync(playlistFilePath, cancellationToken);
            return JsonConvert.DeserializeObject<Playlist>(playlistContentsJson);
        }
    }
}