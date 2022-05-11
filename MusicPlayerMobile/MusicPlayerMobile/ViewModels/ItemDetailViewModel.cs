namespace MusicPlayerMobile.ViewModels
{

    using Xamarin.Forms;

    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }

        public string Description
        {
            get => this.description;
            set => this.SetProperty(ref this.description, value);
        }

        public string ItemId
        {
            get => this.itemId;
            set => this.itemId = value;
        }
    }
}
