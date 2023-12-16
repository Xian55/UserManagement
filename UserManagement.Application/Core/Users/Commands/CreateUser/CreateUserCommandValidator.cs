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
        // TODO: add validation rules.

        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.User.NameIsRequired);
        RuleFor(x => x.Username).NotEmpty().WithError(ValidationErrors.User.UsernameIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.User.EmailIsRequired);
        RuleFor(x => x.Email).EmailAddress().WithError(ValidationErrors.User.EmailIsInvalid);

        RuleFor(x => x.Phone).NotEmpty().WithError(ValidationErrors.User.PhoneIsRequired);

        RuleFor(x => x.Website)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.Website))
            .WithError(ValidationErrors.User.WebsiteIsRequired);

        RuleFor(x => x.Address).NotEmpty().WithError(ValidationErrors.User.AddressIsRequired);
        RuleFor(x => x.Company).NotEmpty().WithError(ValidationErrors.User.CompanyIsRequired);
    }
}
