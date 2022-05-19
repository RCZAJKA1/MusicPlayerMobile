namespace MusicPlayerMobile.Services
{
    using Android.Widget;

    /// <inheritdoc cref="IAndroidToastService"/>
    internal sealed class AndroidToastService : IAndroidToastService
    {
        /// <inheritdoc />
        public void DisplayToastMessage(string message, ToastLength toastLength = ToastLength.Short)
        {
            message.ThrowIfNull(nameof(message));
            message.ThrowIfEmptyOrWhiteSpace(nameof(message));

            Toast playlistNameRequiredMsg = Toast.MakeText(Android.App.Application.Context, message, toastLength);
            playlistNameRequiredMsg.Show();
        }
    }
}
