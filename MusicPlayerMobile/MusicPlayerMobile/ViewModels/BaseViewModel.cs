namespace MusicPlayerMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using MusicPlayerMobile.Models;
    using MusicPlayerMobile.Services;

    using Xamarin.Forms;

    /// <summary>
    ///     The base view model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     The data store.
        /// </summary>
        public IDataStore<Song> DataStore => DependencyService.Get<IDataStore<Song>>();

        /// <summary>
        ///     The is busy.
        /// </summary>
        private bool _isBusy = false;

        /// <summary>
        ///     The title.
        /// </summary>
        string _title = string.Empty;

        /// <summary>
        ///     Gets and sets the is busy.
        /// </summary>
        public bool IsBusy
        {
            get => this._isBusy;
            set => this.SetProperty(ref this._isBusy, value);
        }

        /// <summary>
        ///     Gets and sets the title.
        /// </summary>
        public string Title
        {
            get => this._title;
            set => this.SetProperty(ref this._title, value);
        }

        /// <summary>
        ///     Sets the referenced property using the specified value.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="backingStore">The backing store.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="onChanged">The on changed action.</param>
        /// <returns><c>true</c> if the property was successfully changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        /// <summary>
        ///     The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Executes upon the property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
