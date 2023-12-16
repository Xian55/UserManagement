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


        internal static Error NameIsRequired => new Error("User.NameIsRequired", "The user name is required.");

        internal static Error UsernameIsRequired => new Error("User.UsernameIsRequired", "The user username is required.");

        internal static Error EmailIsRequired => new Error("User.EmailIsRequired", "The user email is required.");
        internal static Error EmailIsInvalid => new Error("User.EmailIsInvalid", "The user email is invalid.");

        internal static Error AddressIsRequired => new Error("User.AddressIsRequired", "The user address is required.");

        internal static Error PhoneIsRequired => new Error("User.PhoneIsRequired", "The user phone is required.");

        internal static Error WebsiteIsRequired => new Error("User.WebsiteIsRequired", "The user website is required.");

        internal static Error CompanyIsRequired => new Error("User.CompanyIsRequired", "The user company is required.");
    }
}
