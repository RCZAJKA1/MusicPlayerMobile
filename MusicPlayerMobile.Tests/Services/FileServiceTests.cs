namespace MusicPlayerMobile.Tests.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Moq;

    using MusicPlayerMobile.Services;

    using Xunit;

    public sealed class FileServiceTests
    {
        private readonly MockRepository _mockRepository;

        public FileServiceTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public async Task CreatePlaylistFolderAsync_OperationCancelled_ThrowsAsync()
        {
            CancellationToken cancellationToken = new CancellationToken(true);
            FileService fileService = this.CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.CreatePlaylistFolderAsync(cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_OperationCancelled_ThrowsAsync()
        {
            string playlistName = "testPlaylistName";
            string contents = "testContents";
            CancellationToken cancellationToken = new CancellationToken(true);
            FileService fileService = this.CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.SavePlaylistAsync(playlistName, contents, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DoesDirectoryExistAsync_OperationCancelled_ThrowsAsync()
        {
            string directoryPath = "testPath";
            CancellationToken cancellationToken = new CancellationToken(true);
            FileService fileService = this.CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.DoesDirectoryExistAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNamesAsync_OperationCancelled_ThrowsAsync()
        {
            string directoryPath = "testPath";
            CancellationToken cancellationToken = new CancellationToken(true);
            FileService fileService = this.CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.GetFileNamesAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNameAsync_OperationCancelled_ThrowsAsync()
        {
            string filePath = "testPath";
            CancellationToken cancellationToken = new CancellationToken(true);
            FileService fileService = this.CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.GetFileNameAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ReadAllTextAsync_OperationCancelled_ThrowsAsync()
        {
            string filePath = "testPath";
            CancellationToken cancellationToken = new CancellationToken(true);
            FileService fileService = this.CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.ReadAllTextAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        private FileService CreateService()
        {
            return new FileService();
        }
    }
}
