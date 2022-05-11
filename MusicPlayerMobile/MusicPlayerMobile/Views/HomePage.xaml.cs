namespace MusicPlayerMobile.Views
{
    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }
    }
}