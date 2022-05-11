namespace MusicPlayerMobile.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;

    /// <inheritdoc cref="ISongsRepo{T}"/>
    public class MockDataStore /*: ISongsRepo<Song>*/
    {
        /// <summary>
        ///     The songs.
        /// </summary>
        private readonly IEnumerable<Song> _songs;

        /// <summary>
        ///     Creates a new instance of the <see cref="MockDataStore"/> class.
        /// </summary>
        public MockDataStore()
        {
        }

        /// <inheritdoc/>
        public async Task<Song> GetSongAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(this._songs.FirstOrDefault(s => s.Id == id));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Song>> GetAllSongsAsync(bool forceRefresh = false, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(this._songs);
        }
    }
}