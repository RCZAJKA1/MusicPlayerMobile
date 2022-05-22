namespace MusicPlayerMobile.Tests.Services
{

    using Moq;

    using MusicPlayerMobile;
    using MusicPlayerMobile.Services;

    using Xamarin.Forms;

    public sealed class SongServiceTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IFileService> _mockFileService;

        public SongServiceTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
            this._mockFileService = this._mockRepository.Create<IFileService>();
        }

        //[Fact]
        //public async Task GetAllSongsAsync_DirectoryDoesNotExist_ThrowsAsync()
        //{
        //    CancellationToken cancellationToken = new(false);
        //    SongService service = CreateService();

        //    this._mockFileService.Setup(s => s.DoesDirectoryExistAsync(It.Is<string>(y => y == FolderPaths.MusicFolderPath), It.Is<CancellationToken>(y => y == cancellationToken))).ReturnsAsync(false);

        //    InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetAllSongsAsync(cancellationToken)).ConfigureAwait(false);
        //    Assert.Equal("The directory '/storage/emulated/0/Music' does not exist.", exception.Message);
        //    this._mockRepository.VerifyAll();
        //}

        private static SongService CreateService()
        {
            DependencyService.Register<ISongService, SongService>();
            return new SongService();
        }
    }
}
