using Kros.KORM;
using Kros.KORM.Converter;
using Kros.KORM.Metadata;
using Kros.Utils;
using Microsoft.Extensions.DependencyInjection;
using PostmanTesting.Infrastructure.Entities;
using System;

namespace PostmanTesting.Infrastructure
{
    /// <summary>
    /// KORM database configurator.
    /// </summary>
    public class DatabaseConfiguration : DatabaseConfigurationBase
    {
        /// <summary>
        /// Name of Workshops table in database.
        /// </summary>
        public const string WorkshopsTableName = "Workshops";

        /// <summary>
        /// Name of People table in database.
        /// </summary>
        public const string PeopleTableName = "People";

        private readonly IServiceCollection _services;
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Service provider.
        /// </summary>
        protected IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider ??= _services.BuildServiceProvider();
            }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public DatabaseConfiguration(IServiceCollection services)
        {
            _services = Check.NotNull(services, nameof(services));
        }

        /// <inheritdoc />
        public override void OnModelCreating(ModelConfigurationBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Workshop>()
                .HasTableName(WorkshopsTableName)
                .HasPrimaryKey(f => f.Id).AutoIncrement()
                .UseConverterForProperties<string>(NullAndTrimStringConverter.ConvertNull)
                .Property(f => f.CreatedTimestamp).UseCurrentTimeValueGenerator(ValueGenerated.OnInsert)
                .Property(f => f.LastModifiedTimestamp).UseCurrentTimeValueGenerator(ValueGenerated.OnInsertOrUpdate)
                .Property(f => f.CreatedBy).UseValueGeneratorOnInsert(new CurrentUserValueGenerator(ServiceProvider))
                .Property(f => f.LastModifiedBy).UseValueGeneratorOnInsertOrUpdate(new CurrentUserValueGenerator(ServiceProvider));

            modelBuilder.Entity<Person>()
                .HasTableName(PeopleTableName)
                .HasPrimaryKey(f => f.Id).AutoIncrement()
                .UseConverterForProperties<string>(NullAndTrimStringConverter.ConvertNull)
                .Property(f => f.CreatedTimestamp).UseCurrentTimeValueGenerator(ValueGenerated.OnInsert)
                .Property(f => f.LastModifiedTimestamp).UseCurrentTimeValueGenerator(ValueGenerated.OnInsertOrUpdate)
                .Property(f => f.CreatedBy).UseValueGeneratorOnInsert(new CurrentUserValueGenerator(ServiceProvider))
                .Property(f => f.LastModifiedBy).UseValueGeneratorOnInsertOrUpdate(new CurrentUserValueGenerator(ServiceProvider));
        }
    }
}
