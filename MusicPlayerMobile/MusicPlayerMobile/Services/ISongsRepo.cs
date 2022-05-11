namespace MusicPlayerMobile.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Handles operations against song data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISongsRepo<T>
    {
        /// <summary>
        ///     Gets a single song by its identifier.
        /// </summary>
        /// <param name="id">The song identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Song"/>.</returns>
        Task<T> GetSongAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Gets all songs from internal storage.
        /// </summary>
        /// <param name="forceRefresh">The force refresh.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all songs.</returns>
        Task<IEnumerable<T>> GetAllSongsAsync(bool forceRefresh = false, CancellationToken cancellationToken = default);
    }
}
