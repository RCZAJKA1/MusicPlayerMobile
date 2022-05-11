namespace MusicPlayerMobile.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Handles operations against song data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        ///     Gets a single song by its identifier.
        /// </summary>
        /// <param name="id">The song identifier.</param>
        /// <returns>The <see cref="Song"/>.</returns>
        Task<T> GetSongAsync(int id);

        /// <summary>
        ///     Gets all songs from internal storage.
        /// </summary>
        /// <param name="forceRefresh">The force refresh.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all songs.</returns>
        Task<IEnumerable<T>> GetAllSongsAsync(bool forceRefresh = false);
    }
}
