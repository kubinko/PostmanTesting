using FluentValidation;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="AddPersonToWorkshopCommand"/>.
    /// </summary>
    public class AddPersonToWorkshopCommandValidator : AbstractValidator<AddPersonToWorkshopCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public AddPersonToWorkshopCommandValidator()
        {
            RuleFor(x => x.WorkshopId).IdValidation();
            RuleFor(x => x.PersonId).IdValidation();
        }
    }
}
