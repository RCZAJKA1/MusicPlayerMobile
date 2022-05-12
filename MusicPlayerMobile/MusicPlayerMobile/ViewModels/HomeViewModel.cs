namespace MusicPlayerMobile.ViewModels
{
    using System.Windows.Input;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    ///     The home view model.
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="HomeViewModel"/> class.
        /// </summary>
        public HomeViewModel()
        {
            this.Title = "Home";
            this.OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        /// <summary>
        ///     Gets the open web command.
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}