using FluentValidation;

using UserManagement.Application.Extensions;
using UserManagement.Application.Validation;

namespace UserManagement.Application.Core.Users.Commands.CreateUser;

/// <summary>
/// Represents the <see cref="CreateUserCommand"/> validator.
/// </summary>
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
    /// </summary>
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithError(ValidationErrors.User.EmailIsInvalid);

        RuleFor(x => x.Website)
            .Must(uri => Uri.TryCreate(uri, UriKind.Relative, out _))
            .When(x => !string.IsNullOrEmpty(x.Website))
            .WithError(ValidationErrors.User.WebsiteIsRequired);
    }
}
