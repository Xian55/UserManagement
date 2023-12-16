using FluentValidation;

using UserManagement.Application.Extensions;
using UserManagement.Application.Validation;

namespace UserManagement.Application.Core.Users.Queries.GetUsers;

/// <summary>
/// Represents the <see cref="GetUsersQuery"/> validator.
/// </summary>
public sealed class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersQueryValidator"/> class.
    /// </summary>
    public GetUsersQueryValidator()
    {
        //TODO: validation rules
        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress().WithError(ValidationErrors.User.EmailIsInvalid);
        });
    }
}
