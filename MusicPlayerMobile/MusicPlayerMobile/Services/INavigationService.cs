namespace MusicPlayerMobile.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Handles page navigation.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///     Navigates to the specified page.
        /// </summary>
        /// <param name="pageName">The page name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> that completed navigating to the page.</returns>
        Task NavigateToPageAsync(string pageName, CancellationToken cancellationToken = default);
    }
}