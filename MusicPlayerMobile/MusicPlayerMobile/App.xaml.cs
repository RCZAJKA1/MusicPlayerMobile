namespace MusicPlayerMobile
{
    using MusicPlayerMobile.Services;

    using Xamarin.Forms;

    /// <summary>
    ///     The app.
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        ///     Creates a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            this.MainPage = new AppShell();

            this.RegisterDependencies();
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
        }

        /// <inheritdoc/>
        protected override void OnSleep()
        {
        }

        /// <inheritdoc/>
        protected override void OnResume()
        {
        }

        private void RegisterDependencies()
        {
            DependencyService.Register<ISongService, SongService>();
            DependencyService.Register<IFileService, FileService>();
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IAndroidToastService, AndroidToastService>();
        }
    }
}
