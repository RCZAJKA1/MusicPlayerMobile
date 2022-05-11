namespace MusicPlayerMobile.Droid
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;

    /// <inheritdoc cref="ISongsRepo{T}"/>
    internal sealed class SongsRepo : ISongsRepo<Song>
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<Song>> GetAllSongsAsync(bool forceRefresh = false, CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(Constants.AndroidMusicFolderPath))
            {
                return await Task.FromResult(new List<Song>());
            }

            cancellationToken.ThrowIfCancellationRequested();

            IList<Song> allSongs = new List<Song>();
            IEnumerable<string> songFiles = Directory.EnumerateFiles(Constants.AndroidMusicFolderPath);
            foreach (string songFile in songFiles)
            {
                Song song = new Song
                {
                    Name = songFile
                };

                allSongs.Add(song);
            }

            return await Task.FromResult(allSongs);
        }

        /// <inheritdoc/>
        public Task<Song> GetSongAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}