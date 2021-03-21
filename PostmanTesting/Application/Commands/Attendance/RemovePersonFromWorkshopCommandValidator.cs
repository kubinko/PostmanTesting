using FluentValidation;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="RemovePersonFromWorkshopCommand"/>.
    /// </summary>
    public class RemovePersonFromWorkshopCommandValidator : AbstractValidator<RemovePersonFromWorkshopCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public RemovePersonFromWorkshopCommandValidator()
        {
            RuleFor(x => x.WorkshopId).IdValidation();
            RuleFor(x => x.PersonId).IdValidation();
        }
    }
}
