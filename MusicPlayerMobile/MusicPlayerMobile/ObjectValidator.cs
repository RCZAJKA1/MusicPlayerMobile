namespace MusicPlayerMobile
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     Handles common object validations.
    /// </summary>
    public static class ObjectValidator
    {
        #region Objects

        /// <summary>
        ///     Throws an exception if the object is null.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="objName">The object name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull(this object obj, string objName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(objName);
            }
        }

        #endregion

        #region Strings

        /// <summary>
        ///     Determines if the string is empty or only contains white space.
        /// </summary>
        /// <param name="str">The string.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmptyOrWhiteSpace(this string str)
        {
            str.ThrowIfNull(nameof(str));

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsWhiteSpace(str[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Throws an exception if the string is empty.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="strName">The string name.</param>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfEmptyOrWhiteSpace(this string str, string strName)
        {
            if (str.IsEmptyOrWhiteSpace())
            {
                throw new ArgumentException("The string cannot be empty or only contain white space.", strName);
            }
        }

        #endregion
    }
}
