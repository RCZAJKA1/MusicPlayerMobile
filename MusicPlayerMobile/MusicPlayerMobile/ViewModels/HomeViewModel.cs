namespace MusicPlayerMobile.ViewModels
{
    using System.Windows.Input;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            this.Title = "Home";
            this.OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}