namespace MusicPlayerMobile.Tests.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;

    using MusicPlayerMobile.Services;

    using Xunit;

    public sealed class NavigationServiceTests
    {
        private readonly MockRepository _mockRepository;

        public NavigationServiceTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public async Task NavigateToPageAsync_PageNameNull_ThrowsAsync()
        {
            CancellationToken cancellationToken = new(false);
            NavigationService service = CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.NavigateToPageAsync(null, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("Value cannot be null. (Parameter 'pageName')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task NavigateToPageAsync_PageNameEmpty_ThrowsAsync()
        {
            string pageName = string.Empty;
            CancellationToken cancellationToken = new(false);
            NavigationService service = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await service.NavigateToPageAsync(pageName, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'pageName')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task NavigateToPageAsync_PageNameWhiteSpace_ThrowsAsync()
        {
            string pageName = " ";
            CancellationToken cancellationToken = new(false);
            NavigationService service = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await service.NavigateToPageAsync(pageName, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'pageName')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        // TODO: fix
        //[Fact]
        //public async Task NavigateToPageAsync_PageNameValid_NavigatesToPageAsync()
        //{
        //    string pageName = "TestPage1";
        //    CancellationToken cancellationToken = new(false);
        //    NavigationService service = CreateService();
        //    const string MainPageTitle = "testMainShell";

        //    // Shell.Current
        //    Init();
        //    Application.Current = new Application
        //    {
        //        MainPage = new Shell
        //        {
        //            CurrentItem = 
        //        }
        //    };
        //    Routing.RegisterRoute(pageName, typeof(MockPage));
        //    Routing.RegisterRoute("TestPage2", typeof(MockPage));

        //    await service.NavigateToPageAsync(pageName, cancellationToken).ConfigureAwait(false);
        //    Assert.Equal($"//{pageName}", Shell.Current.CurrentState);
        //    this._mockRepository.VerifyAll();
        //}

        //private static void Init()
        //{
        //    Device.Info = new MockDeviceInfo();
        //    Device.PlatformServices = new MockPlatformServices();

        //    DependencyService.Register<MockResourcesProvider>();
        //    DependencyService.Register<MockDeserializer>();
        //}

        private static NavigationService CreateService()
        {
            return new NavigationService();
        }
    }
}
