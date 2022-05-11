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
        ///     The home view model.
        /// </summary>
        private readonly HomeViewModel homeViewModel;

        /// <summary>
        ///     Creates a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        public HomePage()
        {
            this.InitializeComponent();
            this.BindingContext = new HomeViewModel();
            this.homeViewModel = this.BindingContext as HomeViewModel;
        }

        /// <summary>
        ///     Prepares the home view model while the page is appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this.homeViewModel.OnAppearing();
        }
    }
}