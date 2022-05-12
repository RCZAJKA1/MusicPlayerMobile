namespace MusicPlayerMobile
{

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
    }
}
