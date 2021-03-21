using FluentValidation;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="CreateWorkshopCommand"/>.
    /// </summary>
    public class CreateWorkshopCommandValidator : AbstractValidator<CreateWorkshopCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public CreateWorkshopCommandValidator()
        {
            RuleFor(x => x.Name).WorkshopNameValidation();
            RuleFor(x => x.Description).WorkshopDescriptionValidation();
            RuleFor(x => x.Date).WorkshopDateValidation();
            RuleFor(x => x.Price).WorkshopPriceValidation();
        }
    }
}
