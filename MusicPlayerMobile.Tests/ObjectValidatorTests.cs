namespace MusicPlayerMobile.Tests
{
    using System;

    using Moq;

    using Xunit;

    public sealed class ObjectValidatorTests
    {
        private readonly MockRepository _mockRepository;

        public ObjectValidatorTests()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void ThrowIfNull_ObjectNull_Throws()
        {
            object? test = null;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => test.ThrowIfNull(nameof(test)));
            Assert.Equal("Value cannot be null. (Parameter 'test')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void ThrowIfNull_ObjectNotNull_NoExceptionThrown()
        {
            string test = "test";
            ArgumentNullException? exception = null;

            try
            {
                test.ThrowIfNull(nameof(test));
            }
            catch (ArgumentNullException ex)
            {
                exception = ex;
            }

            Assert.Null(exception);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void IsEmptyOrWhiteSpace_StringNull_Throws()
        {
            string? test = null;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => test.IsEmptyOrWhiteSpace());
            Assert.Equal("Value cannot be null. (Parameter 'str')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void IsEmptyOrWhiteSpace_StringWhiteSpace_ReturnsTrue()
        {
            string? test = " ";

            bool result = test.IsEmptyOrWhiteSpace();
            Assert.True(result);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void IsEmptyOrWhiteSpace_StringNotWhiteSpace_ReturnsFalse()
        {
            string? test = " test ";

            bool result = test.IsEmptyOrWhiteSpace();
            Assert.False(result);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void ThrowIfEmptyOrWhiteSpace_StringWhiteSpace_Throws()
        {
            string? test = " ";

            ArgumentEmptyException exception = Assert.Throws<ArgumentEmptyException>(() => test.ThrowIfEmptyOrWhiteSpace(nameof(test)));
            Assert.Equal("The argument cannot be empty or only contain white space. (Parameter 'test')", exception.Message);
            this._mockRepository.VerifyAll();
        }

        [Fact]
        public void ThrowIfEmptyOrWhiteSpace_StringNotWhiteSpace_DoesNotThrow()
        {
            string? test = " test ";
            ArgumentEmptyException? exception = null;

            try
            {
                test.ThrowIfEmptyOrWhiteSpace(nameof(test));
            }
            catch (ArgumentEmptyException ex)
            {
                exception = ex;
            }

            Assert.Null(exception);
            this._mockRepository.VerifyAll();
        }
    }
}
