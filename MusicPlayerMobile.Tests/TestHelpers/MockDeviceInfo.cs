namespace MusicPlayerMobile.Tests.TestHelpers
{
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    internal class MockDeviceInfo : DeviceInfo
    {
        public override Size PixelScreenSize => Size.Zero;

        public override Size ScaledScreenSize => Size.Zero;

        public override double ScalingFactor => 1;
    }
}
