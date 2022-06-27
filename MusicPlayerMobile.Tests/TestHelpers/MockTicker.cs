namespace MusicPlayerMobile.Tests.TestHelpers
{
    using Xamarin.Forms.Internals;

    internal class MockTicker : Ticker
    {
        bool _enabled;

        protected override void EnableTimer()
        {
            this._enabled = true;

            while (this._enabled)
            {
                this.SendSignals(16);
            }
        }

        protected override void DisableTimer()
        {
            this._enabled = false;
        }
    }
}
