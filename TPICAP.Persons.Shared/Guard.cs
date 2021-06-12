using System;

namespace TPICAP.Persons.Shared
{
    public static class Guard
    {
        public static void NotNull<T>(this T argument)
        {
            if (argument == null)
                throw new ArgumentNullException($"{nameof(argument)} must not be null.");
        }

        public static void NotNullOrEmpty(this string argument)
        {
            if (argument == null)
                throw new ArgumentNullException($"{nameof(argument)} must not be null.");
            else if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentException($"{nameof(argument)} must not be empty.");
        }

        public static void MaxLength(this string argument, int maxLength) =>
            argument.Must(a => a.Length <= maxLength, $"{nameof(argument)} length must not be greater than { maxLength } characters.");

        public static void Must<T>(this T argument, Func<T, bool> condition, string message)
        {
            if (!condition(argument)) throw new ArgumentException(message);
        }
    }
}
