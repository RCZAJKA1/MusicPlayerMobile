namespace MusicPlayerMobile.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    /// <inheritdoc cref="INavigationService"/>
    internal sealed class NavigationService : INavigationService
    {
        /// <inheritdoc />
        public async Task NavigateToPageAsync(string pageName, CancellationToken cancellationToken = default)
        {
            pageName.ThrowIfNull(nameof(pageName));
            pageName.ThrowIfEmptyOrWhiteSpace(nameof(pageName));

            cancellationToken.ThrowIfCancellationRequested();

            await Shell.Current.GoToAsync($"//{pageName}");
        }
    }
}
