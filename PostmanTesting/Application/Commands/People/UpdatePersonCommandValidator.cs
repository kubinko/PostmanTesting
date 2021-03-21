using FluentValidation;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="UpdatePersonCommand"/>.
    /// </summary>
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public UpdatePersonCommandValidator()
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
