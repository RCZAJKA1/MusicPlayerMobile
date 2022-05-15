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
                throw new InvalidOperationException($"The directory '{Constants.AndroidFolderPathMusic}' does not exist.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<Song> allSongs = new List<Song>();
            List<string> songFiles = Directory.EnumerateFiles(Constants.AndroidFolderPathMusic).ToList();
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
    }
}