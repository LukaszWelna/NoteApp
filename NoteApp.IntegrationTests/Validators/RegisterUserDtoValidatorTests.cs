using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;
using NoteApp.Server.Models.Validators;

namespace NoteApp.IntegrationTests.Validators
{
    public class RegisterUserDtoValidatorTests
    {
        private readonly NoteAppContext _dbContext;

        public RegisterUserDtoValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<NoteAppContext>();
            builder.UseInMemoryDatabase("NoteAppDb");

            _dbContext = new NoteAppContext(builder.Options);

            Seed();
        }

        private void Seed()
        {
            var users = new List<User>()
            {
                new User()
                {
                    FirstName = "TestFirstName1",
                    LastName = "TestLastName1",
                    Email = "test@test.com",
                    PasswordHash = "TestPasswordHash"
                }, 
                new User()
                {
                    FirstName = "TestFirstName2",
                    LastName = "TestLastName2",
                    Email = "test2@test.com",
                    PasswordHash = "TestPasswordHash2"
                }
            };

            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();
        }

        public static IEnumerable<object[]> GetSampleValidModels()
        {
            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "testtest@test.com",
                    Password = "TestPassword",
                    ConfirmPassword = "TestPassword"
                }
            };
            
            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "Test2FirstName",
                    LastName = "Test2LastName",
                    Email = "testtest2@test.com",
                    Password = "Test2Password",
                    ConfirmPassword = "Test2Password"
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleValidModels))]
        public void Validate_ForValidModel_ReturnsSuccess(RegisterUserDto dtoModel)
        {
            // Arrange

            var validator = new RegisterUserDtoValidator(_dbContext);

            // Act
            var result = validator.TestValidate(dtoModel);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        public static IEnumerable<object[]> GetSampleInvalidModels()
        {
            yield return new object[] {
                new RegisterUserDto()
                {
                    LastName = "TestLastName",
                    Email = "testtest@test.com",
                    Password = "TestPassword",
                    ConfirmPassword = "TestPassword"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    Email = "test@test.com",
                    Password = "TestPassword",
                    ConfirmPassword = "TestPassword"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Password = "TestPassword",
                    ConfirmPassword = "TestPassword"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "test@test.com",
                    ConfirmPassword = "TestPassword"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "test@test.com",
                    Password = "TestPassword"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "testInvalidEmail",
                    Password = "TestPassword",
                    ConfirmPassword = "TestPassword"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "testInvalidEmail",
                    Password = "test",
                    ConfirmPassword = "test"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "testInvalidEmail",
                    Password = "TestPassword",
                    ConfirmPassword = "WrongConfirmPassword"
                }
            };

            yield return new object[] {
                new RegisterUserDto()
                {
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    Email = "test@test.com",
                    Password = "TestPassword",
                    ConfirmPassword = "TestPassword"
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidModels))]
        public void Validate_ForInvalidModel_ReturnsFailure(RegisterUserDto dtoModel)
        {
            // Arrange

            var validator = new RegisterUserDtoValidator(_dbContext);

            // Act
            var result = validator.TestValidate(dtoModel);

            // Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
