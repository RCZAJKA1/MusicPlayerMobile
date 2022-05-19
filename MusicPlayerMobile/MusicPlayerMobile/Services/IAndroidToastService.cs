namespace MusicPlayerMobile.Services
{
    using Android.Widget;

    /// <summary>
    ///     Handles android toast message operations.
    /// </summary>
    public interface IAndroidToastService
    {
        /// <summary>
        ///     Displays a toast message to the user.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="toastLength">The length of message visibility. The default is short.</param>
        void DisplayToastMessage(string message, ToastLength toastLength = ToastLength.Short);
    }
}