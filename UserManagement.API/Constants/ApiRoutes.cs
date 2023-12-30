namespace UserManagement.Api.Constants;

/// <summary>
/// Contains the API endpoint routes.
/// </summary>
internal static class ApiRoutes
{
    /// <summary>
    /// Contains the users routes.
    /// </summary>
    internal static class Users
    {
        /// <summary>
        /// The create user route.
        /// </summary>
        internal const string CreateUser = "users";

        /// <summary>
        /// The start user route.
        /// </summary>
        internal const string UpdateUser = "users/{userId}";

        /// <summary>
        /// The remove user route.
        /// </summary>
        internal const string RemoveUser = "users/{userId}";

        /// <summary>
        /// The get user by id route.
        /// </summary>
        internal const string GetUserById = "users/{userId}";

        /// <summary>
        /// The get users route.
        /// </summary>
        internal const string GetUsers = "users";
    }
}
