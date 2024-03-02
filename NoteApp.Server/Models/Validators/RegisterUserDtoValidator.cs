using FluentValidation;
using NoteApp.Server.Entities;

namespace NoteApp.Server.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(NoteAppContext dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("The \"Email\" field cannot be empty.")
                .EmailAddress()
                .WithMessage("The \"Email password\" field does not contain a valid email address.");

            RuleFor(u => u.FirstName)
                .NotEmpty()
                .WithMessage("The \"First name\" field cannot be empty.");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .WithMessage("The \"Last name\" field cannot be empty.");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("The \"Password\" field cannot be empty.")
                .MinimumLength(8)
                .WithMessage("The \"Password\" field must contain at least 8 characters.");

            RuleFor(u => u.ConfirmPassword)
                .NotEmpty()
                .WithMessage("The \"Confirm password\" field cannot be empty.");

            RuleFor(u => u.Password)
                .Equal(u => u.ConfirmPassword)
                .WithMessage("The \"Confirm password\" field must match the \"Password\" field.");

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailTaken = dbContext.Users.Any(u => u.Email == value);

                    if (emailTaken)
                    {
                        context.AddFailure("Email", "The Email address is already taken");
                    }
                });
            
        }
    }
}
