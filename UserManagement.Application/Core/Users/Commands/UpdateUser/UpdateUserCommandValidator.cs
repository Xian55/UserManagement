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
        RuleFor(x => x.Email).EmailAddress().WithError(ValidationErrors.User.EmailIsInvalid);

        RuleFor(x => x.Website)
            .Must(uri => Uri.TryCreate(uri, UriKind.Relative, out _))
            .When(x => !string.IsNullOrEmpty(x.Website))
            .WithError(ValidationErrors.User.WebsiteIsRequired);
    }
}
