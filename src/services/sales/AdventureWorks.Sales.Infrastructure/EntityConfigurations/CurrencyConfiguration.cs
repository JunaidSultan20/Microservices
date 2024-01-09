namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> entity)
    {
        entity.HasKey(keyExpression: expression => expression.CurrencyCode).HasName(name: "PK_Currency_CurrencyCode");

        entity.ToTable(name: "Currency", buildAction: table => table.HasComment(comment: "Lookup table containing standard ISO currencies."));

        entity.HasIndex(indexExpression: expression => expression.Name, name: "AK_Currency_Name").IsUnique();

        entity.Property(propertyExpression: expression => expression.CurrencyCode)
              .HasMaxLength(maxLength: 3)
              .IsFixedLength()
              .HasComment(comment: "The ISO code for the Currency.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Name)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Currency name.");
    }
}
