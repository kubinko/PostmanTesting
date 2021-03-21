using FluentValidation;
using System;

namespace PostmanTesting.Application.Commands
{
    /// <summary>
    /// Base validation rules.
    /// </summary>
    public static class ValidationRules
    {
        #region Person

        /// <summary>
        /// Validation rule for person name.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> PersonNameValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(50);

        /// <summary>
        /// Validation rule for person surname.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> SurnameValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(100);

        /// <summary>
        /// Validation rule for street.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> StreetValidation<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(255);

        /// <summary>
        /// Validation rule for postcode.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> PostCodeValidation<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(20);

        /// <summary>
        /// Validation rule for city.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> CityValidation<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(255);

        /// <summary>
        /// Validation rule for country.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> CountryValidation<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(255);

        /// <summary>
        /// Validation rule for phone number.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> PhoneNumberValidation<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(40);

        /// <summary>
        /// Validation rule for e-mail.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> EmailValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(255)
                .EmailAddress();

        #endregion

        #region Workshop

        /// <summary>
        /// Validation rule for workshop name.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> WorkshopNameValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(100);

        /// <summary>
        /// Validation rule for workshop description.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> WorkshopDescriptionValidation<T>(this IRuleBuilder<T, string> rule)
            => rule.MaximumLength(500);

        /// <summary>
        /// Validation rule for workshop date.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, DateTime> WorkshopDateValidation<T>(this IRuleBuilder<T, DateTime> rule)
            => rule
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.UtcNow);

        /// <summary>
        /// Validation rule for workshop price.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal> WorkshopPriceValidation<T>(this IRuleBuilder<T, decimal> rule)
            => rule.GreaterThanOrEqualTo(0);

        #endregion

        #region Attendance

        /// <summary>
        /// Validation rule for attendance IDs.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, long> IdValidation<T>(this IRuleBuilder<T, long> rule)
            => rule.NotEmpty();

        #endregion
    }
}
