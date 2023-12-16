using FluentValidation;

using UserManagement.Application.Extensions;
using UserManagement.Application.Validation;

namespace UserManagement.Application.Core.Users.Commands.UpdateUser;

/// <summary>
/// Represents the <see cref="UpdateUserCommand"/> validator.
/// </summary>
public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserCommandValidator"/> class.
    /// </summary>
    public UpdateUserCommandValidator()
    {
        // TODO: Add validation rules.
        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.User.EmailIsRequired);
    }
}
