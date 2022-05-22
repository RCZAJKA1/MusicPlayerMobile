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
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.CreatePlaylistFolderAsync(cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_PlaylistNameNull_ThrowsAsync()
        {
            string contents = "testContents";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await fileService.SavePlaylistAsync(null, contents, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("Value cannot be null. (Parameter 'playlistName')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_PlaylistNameEmpty_ThrowsAsync()
        {
            string playlistName = String.Empty;
            string contents = "testContents";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.SavePlaylistAsync(playlistName, contents, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'playlistName')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_PlaylistNameWhiteSpace_ThrowsAsync()
        {
            string playlistName = " ";
            string contents = "testContents";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.SavePlaylistAsync(playlistName, contents, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'playlistName')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_ContentsNull_ThrowsAsync()
        {
            string playlistName = "testPlaylistName";
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await fileService.SavePlaylistAsync(playlistName, null, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("Value cannot be null. (Parameter 'contents')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_ContentsEmpty_ThrowsAsync()
        {
            string playlistName = "testPlaylistName";
            string contents = String.Empty;
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.SavePlaylistAsync(playlistName, contents, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'contents')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_ContentsWhiteSpace_ThrowsAsync()
        {
            string playlistName = "testPlaylistName";
            string contents = " ";
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.SavePlaylistAsync(playlistName, contents, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'contents')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePlaylistAsync_OperationCancelled_ThrowsAsync()
        {
            string playlistName = "testPlaylistName";
            string contents = "testContents";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.SavePlaylistAsync(playlistName, contents, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DoesDirectoryExistAsync_DirectoryPathNull_ThrowsAsync()
        {
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await fileService.DoesDirectoryExistAsync(null, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("Value cannot be null. (Parameter 'directoryPath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DoesDirectoryExistAsync_DirectoryPathEmpty_ThrowsAsync()
        {
            string directoryPath = string.Empty;
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.DoesDirectoryExistAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'directoryPath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DoesDirectoryExistAsync_DirectoryPathWhiteSpace_ThrowsAsync()
        {
            string directoryPath = " ";
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.DoesDirectoryExistAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'directoryPath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DoesDirectoryExistAsync_OperationCancelled_ThrowsAsync()
        {
            string directoryPath = "testPath";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.DoesDirectoryExistAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNamesAsync_DirectoryPathNull_ThrowsAsync()
        {
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await fileService.GetFileNamesAsync(null, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("Value cannot be null. (Parameter 'directoryPath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNamesAsync_DirectoryPathEmpty_ThrowsAsync()
        {
            string directoryPath = string.Empty;
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.GetFileNamesAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'directoryPath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNamesAsync_DirectoryPathWhiteSpace_ThrowsAsync()
        {
            string directoryPath = " ";
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.GetFileNamesAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'directoryPath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNamesAsync_OperationCancelled_ThrowsAsync()
        {
            string directoryPath = "testPath";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.GetFileNamesAsync(directoryPath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNameAsync_DirectoryPathNull_ThrowsAsync()
        {
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await fileService.GetFileNameAsync(null, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("Value cannot be null. (Parameter 'filePath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNameAsync_DirectoryPathEmpty_ThrowsAsync()
        {
            string filePath = string.Empty;
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.GetFileNameAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'filePath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNameAsync_DirectoryPathWhiteSpace_ThrowsAsync()
        {
            string filePath = " ";
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.GetFileNameAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'filePath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetFileNameAsync_OperationCancelled_ThrowsAsync()
        {
            string filePath = "testPath";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.GetFileNameAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ReadAllTextAsync_DirectoryPathNull_ThrowsAsync()
        {
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await fileService.ReadAllTextAsync(null, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("Value cannot be null. (Parameter 'filePath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ReadAllTextAsync_DirectoryPathEmpty_ThrowsAsync()
        {
            string filePath = string.Empty;
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.ReadAllTextAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'filePath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ReadAllTextAsync_DirectoryPathWhiteSpace_ThrowsAsync()
        {
            string filePath = " ";
            CancellationToken cancellationToken = new(false);
            FileService fileService = CreateService();

            ArgumentEmptyException exception = await Assert.ThrowsAsync<ArgumentEmptyException>(async () => await fileService.ReadAllTextAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'filePath')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ReadAllTextAsync_OperationCancelled_ThrowsAsync()
        {
            string filePath = "testPath";
            CancellationToken cancellationToken = new(true);
            FileService fileService = CreateService();

            OperationCanceledException exception = await Assert.ThrowsAsync<OperationCanceledException>(async () => await fileService.ReadAllTextAsync(filePath, cancellationToken)).ConfigureAwait(false);
            Assert.Equal("The operation was canceled.", exception.Message);
            this._mockRepository.VerifyAll();
        }

        private static FileService CreateService()
        {
            return new FileService();
        }
    }
}
