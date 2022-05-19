namespace MusicPlayerMobile.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    /// <inheritdoc cref="INavigationService"/>
    internal sealed class NavigationService : INavigationService
    {
        /// <inheritdoc />
        public async Task NavigateToPageAsync(string pageName, CancellationToken cancellationToken = default)
        {
            if (pageName == null)
            {
                throw new ArgumentNullException(nameof(pageName));
            }
            if (pageName.Trim() == string.Empty)
            {
                throw new ArgumentException("The argument cannot be empty or only contain white space.", nameof(pageName));
            }

            cancellationToken.ThrowIfCancellationRequested();

            await Shell.Current.GoToAsync($"//{pageName}");
        }
    }
}
