using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MusicPlayerMobile;
using MusicPlayerMobile.Models;
using MusicPlayerMobile.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SongService))]
namespace MusicPlayerMobile
{
    /// <inheritdoc cref="ISongService"/>
    public sealed class SongService : ISongService
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<Song>> GetAllSongsAsync(CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(Constants.AndroidFolderPathMusic))
            {
                return await Task.FromResult(new List<Song>());
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<Song> allSongs = new List<Song>();
            List<string> songFiles = Directory.EnumerateFiles(Constants.AndroidFolderPathMusic).ToList();
            for (int i = 0; i < songFiles.Count; i++)
            {
                string fileName = Path.GetFileName(songFiles[i]);
                Song song = new Song
                {
                    Id = i,
                    Name = fileName,
                    FilePath = songFiles[i]
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