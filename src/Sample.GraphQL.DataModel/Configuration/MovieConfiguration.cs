using Sample.GraphQL.Domain;

namespace Sample.GraphQL.Persistence.Configuration;

internal class MovieEntityConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Title); //example
    }
}

