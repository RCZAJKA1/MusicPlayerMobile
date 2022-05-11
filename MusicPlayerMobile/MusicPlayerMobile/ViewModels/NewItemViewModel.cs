namespace MusicPlayerMobile.ViewModels
{
    using System;

    using Xamarin.Forms;

    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;

        public NewItemViewModel()
        {
            this.CancelCommand = new Command(this.OnCancel);
            this.PropertyChanged +=
                (_, __) => this.SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(this.text)
                && !String.IsNullOrWhiteSpace(this.description);
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
