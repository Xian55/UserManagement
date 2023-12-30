using UserManagement.Domain.Primitives;

namespace UserManagement.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class User
    {
        /// <summary>
        /// Gets the user not found error.
        /// </summary>
        internal static Error NotFound => new Error("User.NotFound", "The user with the specified identifier was not found.");

        /// <summary>
        /// Gets the user identifier is required error.
        /// </summary>
        internal static Error IdentifierIsRequired => new Error("User.IdentifierIsRequired", "The user identifier is required.");

        internal static Error EmailIsInvalid => new Error("User.EmailIsInvalid", "The user email is invalid.");

        internal static Error WebsiteIsRequired => new Error("User.WebsiteIsRequired", "The user website is required.");
    }
}
