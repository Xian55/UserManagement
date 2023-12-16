using FluentValidation;

using UserManagement.Application.Extensions;
using UserManagement.Application.Validation;

namespace UserManagement.Application.Core.Users.Commands.RemoveUser;

/// <summary>
/// Represents the <see cref="RemoveUserCommand"/> validator.
/// </summary>
public sealed class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveUserCommandValidator"/> class.
    /// </summary>
    public RemoveUserCommandValidator() =>
        RuleFor(x => x.UserId).GreaterThan(0).WithError(ValidationErrors.User.IdentifierIsRequired);
}
