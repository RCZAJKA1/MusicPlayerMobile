namespace MusicPlayerMobile.Views
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ObservableCollection<Song> Items { get; set; }

        public ListPage()
        {
            this.InitializeComponent();

            this.Items = new ObservableCollection<Song>();

            for (int i = 0; i < 20; i++)
            {
                this.Items.Add(new Song { Name = $"song {i}" });
            }

            this.BindingContext = new SongsViewModel
            {
                AllSongs = this.Items.ToList()
            };
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await this.DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
