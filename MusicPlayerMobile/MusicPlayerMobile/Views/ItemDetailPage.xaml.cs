namespace MusicPlayerMobile.Views
{
    using System.ComponentModel;

    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;

    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}