namespace MusicPlayerMobile.Views
{
    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    ///     The code behind for the <see cref="HomePage"/> view.
    /// </summary>
    public partial class HomePage : ContentPage
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        public HomePage()
        {
            this.InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }

        /// <summary>
        ///     Prepares the home view model while the page is appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}