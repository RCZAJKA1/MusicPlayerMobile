namespace MusicPlayerMobile.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using MusicPlayerMobile.Models;

    /// <summary>
    ///     Handles operations against song data.
    /// </summary>
    public interface ISongService
    {
        /// <summary>
        ///     Gets all songs from internal storage.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing all songs.</returns>
        Task<IEnumerable<Song>> GetAllSongsAsync(CancellationToken cancellationToken = default);
    }
}
