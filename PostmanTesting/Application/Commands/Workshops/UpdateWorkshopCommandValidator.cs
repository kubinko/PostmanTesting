using FluentValidation;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="UpdateWorkshopCommand"/>.
    /// </summary>
    public class UpdateWorkshopCommandValidator : AbstractValidator<UpdateWorkshopCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public UpdateWorkshopCommandValidator()
        {
            RuleFor(x => x.Name).WorkshopNameValidation();
            RuleFor(x => x.Description).WorkshopDescriptionValidation();
            RuleFor(x => x.Date).WorkshopDateValidation();
            RuleFor(x => x.Price).WorkshopPriceValidation();
        }
    }
}
