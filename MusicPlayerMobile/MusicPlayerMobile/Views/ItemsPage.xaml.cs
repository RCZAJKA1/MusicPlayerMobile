namespace MusicPlayerMobile.Views
{
    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    public partial class ItemsPage : ContentPage
    {
        readonly ItemsViewModel _viewModel;

        public ItemsPage()
        {
            this.InitializeComponent();

            this.BindingContext = this._viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this._viewModel.OnAppearing();
        }
    }
}