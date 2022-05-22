namespace MusicPlayerMobile.Tests.Services
{

    using Moq;

    using MusicPlayerMobile.Services;

    public sealed class AndroidToastServiceTests
    {
        private readonly MockRepository _mockRepository;

        public AndroidToastServiceTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
        }

        // TODO: fix
        //[Fact]
        //public void DisplayToastMessage_MessageNull_Throws()
        //{
        //    AndroidToastService service = CreateService();

        //    ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.DisplayToastMessage(null, ToastLength.Short));
        //    Assert.Equal("Value cannot be null. (Parameter 'message')", exception.Message);
        //    this._mockRepository.VerifyAll();
        //}

        //[Fact]
        //public void DisplayToastMessage_MessageEmpty_Throws()
        //{
        //    string message = string.Empty;
        //    AndroidToastService service = CreateService();

        //    ArgumentEmptyException exception = Assert.Throws<ArgumentEmptyException>(() => service.DisplayToastMessage(message));
        //    Assert.Equal("The argument cannot be empty or white space. (Parameter 'message')", exception.Message);
        //    this._mockRepository.VerifyAll();
        //}

        //[Fact]
        //public void DisplayToastMessage_MessageWhiteSpace_Throws()
        //{
        //    string message = " ";
        //    AndroidToastService service = CreateService();

        //    ArgumentEmptyException exception = Assert.Throws<ArgumentEmptyException>(() => service.DisplayToastMessage(message));
        //    Assert.Equal("The argument cannot be empty or white space. (Parameter 'message')", exception.Message);
        //    this._mockRepository.VerifyAll();
        //}

        private static AndroidToastService CreateService()
        {
            return new AndroidToastService();
        }
    }
}
