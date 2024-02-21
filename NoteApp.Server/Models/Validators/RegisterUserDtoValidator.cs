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
                .EmailAddress();

            RuleFor(u => u.FirstName)
                .NotEmpty();

            RuleFor(u => u.LastName)
                .NotEmpty();

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(u => u.ConfirmPassword)
                .NotEmpty();

            RuleFor(u => u.Password).Equal(u => u.ConfirmPassword);

            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailTaken = dbContext.Users.Any(u => u.Email == value);

                    if (emailTaken)
                    {
                        context.AddFailure("Email", "Email is already taken");
                    }
                });
        }
    }
}
