namespace MusicPlayerMobile.Tests.TestHelpers
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    internal class MockPlatformServices : IPlatformServices
    {
        readonly Action<Action> _invokeOnMainThread;
        readonly Action<Uri> _openUriAction;
        readonly Func<Uri, CancellationToken, Task<Stream>> _getStreamAsync;

        public MockPlatformServices(Action<Action>? invokeOnMainThread = null,
                                    Action<Uri>? openUriAction = null,
                                    Func<Uri, CancellationToken, Task<Stream>>? getStreamAsync = null)
        {
            this._invokeOnMainThread = invokeOnMainThread;
            this._openUriAction = openUriAction;
            this._getStreamAsync = getStreamAsync;
        }

        public string GetMD5Hash(string input)
        {
            throw new NotImplementedException();
        }

        static int hex(int v)
        {
            if (v < 10)
                return '0' + v;
            return 'a' + v - 10;
        }

        public double GetNamedSize(NamedSize size, Type targetElement, bool useOldSizes)
        {
            return size switch
            {
                NamedSize.Default => 10,
                NamedSize.Micro => 4,
                NamedSize.Small => 8,
                NamedSize.Medium => 12,
                NamedSize.Large => 16,
                NamedSize.Body => 1,
                NamedSize.Header => 2,
                NamedSize.Title => 3,
                NamedSize.Subtitle => 5,
                NamedSize.Caption => 6,
                _ => throw new ArgumentOutOfRangeException(nameof(size)),
            };
        }

        public void OpenUriAction(Uri uri)
        {
            if (this._openUriAction != null)
                this._openUriAction(uri);
            else
                throw new NotImplementedException();
        }

        public bool IsInvokeRequired => false;

        public string RuntimePlatform { get; set; }

        public OSAppTheme RequestedTheme => OSAppTheme.Light;

        public void BeginInvokeOnMainThread(Action action)
        {
            if (this._invokeOnMainThread == null)
                action();
            else
                this._invokeOnMainThread(action);
        }

        public Ticker CreateTicker()
        {
            return new MockTicker();
        }

        public void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            Timer? timer = null;
            TimerCallback onTimeout = o => this.BeginInvokeOnMainThread(() =>
            {
                if (callback())
                    return;

                timer?.Dispose();
            });
            timer = new Timer(onTimeout, null, interval, interval);
        }

        public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (this._getStreamAsync == null)
                throw new NotImplementedException();
            return this._getStreamAsync(uri, cancellationToken);
        }

        public Assembly[] GetAssemblies()
        {
            return new Assembly[0];
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            throw new NotImplementedException();
        }

        public string GetHash(string input)
        {
            throw new NotImplementedException();
        }

        public Color GetNamedColor(string name)
        {
            throw new NotImplementedException();
        }

        public void QuitApplication()
        {
            throw new NotImplementedException();
        }

        public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            throw new NotImplementedException();
        }
    }
}
