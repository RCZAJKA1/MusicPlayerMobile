namespace MusicPlayerMobile.ViewModels
{
    using System.Collections.ObjectModel;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Views;

    using Xamarin.Forms;

    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            this.Title = "Browse";
            this.Items = new ObservableCollection<Item>();

            this.ItemTapped = new Command<Item>(this.OnItemSelected);

            this.AddItemCommand = new Command(this.OnAddItem);
        }

        public void OnAppearing()
        {
            this.IsBusy = true;
            this.SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => this._selectedItem;
            set
            {
                this.SetProperty(ref this._selectedItem, value);
                this.OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}