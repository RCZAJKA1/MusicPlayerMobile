namespace MusicPlayerMobile.Tests
{
    using Moq;

    using Xunit;

    public sealed class FolderPathsTests
    {
        private readonly MockRepository _mockRepository;

        public FolderPathsTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void FolderPaths_GetMusicFolderPath_VerifyPath()
        {
            Assert.Equal("/storage/emulated/0/Music", FolderPaths.MusicFolderPath);

            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void FolderPaths_GetPlaylistsFolderPath_VerifyPath()
        {
            Assert.Equal("/storage/emulated/0/Playlists", FolderPaths.PlaylistsFolderPath);

            this._mockRepository.VerifyAll();
        }
    }
}
