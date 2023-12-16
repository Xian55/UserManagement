
using UserManagement.Domain.Primitives;

namespace UserManagement.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        // TODO: Add user domain errors.

        public static Error UserNameMustBeUnique => new Error("User.UserNameMustBeUnique", "The username must be unique.");

        public static Error EmailMustBeUnique => new Error("User.EmailMustBeUnique", "The email must be unique.");
    }
}
