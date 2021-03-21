using FluentValidation;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="CreatePersonCommand"/>.
    /// </summary>
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Name).PersonNameValidation();
            RuleFor(x => x.Surname).SurnameValidation();
            RuleFor(x => x.Street).StreetValidation();
            RuleFor(x => x.PostCode).PostCodeValidation();
            RuleFor(x => x.City).CityValidation();
            RuleFor(x => x.Country).CountryValidation();
            RuleFor(x => x.PhoneNumber).PhoneNumberValidation();
            RuleFor(x => x.Email).EmailValidation();
        }
    }
}
