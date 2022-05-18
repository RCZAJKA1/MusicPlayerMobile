namespace MusicPlayerMobile
{
    using System;

    /// <summary>
    ///     The exception that is thrown when one of the string arguments provided to a method is empty.
    /// </summary>
    public sealed class ArgumentEmptyException : ArgumentException
    {
        private const string ArgumentEmptyMessage = "The argument cannot be empty or only contain white space.";

        /// <summary>
        ///     Creates a new instance of the <see cref="ArgumentEmptyException"/> class.
        /// </summary>
        public ArgumentEmptyException() : base(ArgumentEmptyMessage) { }

        /// <summary>
        ///     Creates a new instance of the <see cref="ArgumentEmptyException"/> class.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        public ArgumentEmptyException(string paramName) : base(ArgumentEmptyMessage, paramName) { }

        /// <summary>
        ///     Creates a new instance of the <see cref="ArgumentEmptyException"/> class.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="innerException">The inner exception.</param>
        public ArgumentEmptyException(string paramName, Exception innerException) : base(ArgumentEmptyMessage, paramName, innerException) { }
    }
}
