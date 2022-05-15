using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MusicPlayerMobile;
using MusicPlayerMobile.Models;
using MusicPlayerMobile.Services;

using Newtonsoft.Json;

[assembly: Xamarin.Forms.Dependency(typeof(SongService))]
namespace MusicPlayerMobile
{
    /// <inheritdoc cref="ISongService"/>
    public sealed class SongService : ISongService
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<Song>> GetAllSongsAsync(CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(Constants.AndroidExternalMusicFolderPath))
            {
                throw new InvalidOperationException($"The directory '{Constants.AndroidExternalMusicFolderPath}' does not exist.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<Song> allSongs = new List<Song>();
            List<string> songFiles = Directory.EnumerateFiles(Constants.AndroidExternalMusicFolderPath).ToList();
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
            string playlistDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Playlists");
            if (!Directory.Exists(playlistDirectory))
            {
                throw new InvalidOperationException($"The directory '{playlistDirectory}' does not exist.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            List<string> playlistFileNames = Directory.EnumerateFiles(playlistDirectory).ToList();
            if (playlistFileNames?.Count == 0)
            {
                return await Task.FromResult(new List<Playlist>());
            }

            List<Playlist> allPlaylists = new List<Playlist>();
            playlistFileNames.Sort();
            for (int i = 0; i < playlistFileNames.Count; i++)
            {
                string filePath = playlistFileNames[i];
                Playlist playlist = this.GetPlaylistFromFile(filePath);

                allPlaylists.Add(playlist);
            }

            return await Task.FromResult(allPlaylists);
        }

        /// <summary>
        ///     Reads the specified file and returns the converted <see cref="Playlist"/> object.
        /// </summary>
        /// <param name="playlistFilePath">The playlist file path.</param>
        /// <returns>The <see cref="Playlist"/>.</returns>
        private Playlist GetPlaylistFromFile(string playlistFilePath)
        {
            string playlistContentsJson = File.ReadAllText(playlistFilePath);
            return JsonConvert.DeserializeObject<Playlist>(playlistContentsJson);
        }
    }
}