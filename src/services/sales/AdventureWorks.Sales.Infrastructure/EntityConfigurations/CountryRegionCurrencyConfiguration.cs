namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class CountryRegionCurrencyConfiguration : IEntityTypeConfiguration<CountryRegionCurrency>
{
    public void Configure(EntityTypeBuilder<CountryRegionCurrency> entity)
    {
        entity.HasKey(keyExpression: expression => new { expression.CountryRegionCode, expression.CurrencyCode })
              .HasName(name: "PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode");

        entity.ToTable(name: "CountryRegionCurrency", schema: "Sales", buildAction: table => table
                          .HasComment(comment: "Cross-reference table mapping ISO currency codes to a country or region."));

        entity.HasIndex(indexExpression: expression => expression.CurrencyCode,
                        name: "IX_CountryRegionCurrency_CurrencyCode");

        entity.Property(propertyExpression: expression => expression.CountryRegionCode)
              .HasMaxLength(maxLength: 3)
              .HasComment(comment: "ISO code for countries and regions. Foreign key to CountryRegion.CountryRegionCode.");

        entity.Property(propertyExpression: expression => expression.CurrencyCode)
              .HasMaxLength(maxLength: 3)
              .IsFixedLength()
              .HasComment(comment: "ISO standard currency code. Foreign key to Currency.CurrencyCode.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.HasOne(navigationExpression: expression => expression.CurrencyCodeNavigation)
              .WithMany(navigationExpression: expression => expression.CountryRegionCurrencies)
              .HasForeignKey(foreignKeyExpression: expression => expression.CurrencyCode)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}