namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

internal class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> entity)
    {
        entity.ToTable(name: "CurrencyRate", buildAction: table => table.HasComment(comment: "Currency exchange rates."));

        entity.HasIndex(indexExpression: expression => new
        {
            expression.CurrencyRateDate,
            expression.FromCurrencyCode,
            expression.ToCurrencyCode
        },
        name: "AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.CurrencyRateId)
              .HasColumnName(name: "CurrencyRateID")
              .HasComment(comment: "Primary key for CurrencyRate records.");

        entity.Property(propertyExpression: expression => expression.AverageRate)
              .HasColumnType(typeName: "money")
              .HasComment(comment: "Average exchange rate for the day.");

        entity.Property(propertyExpression: expression => expression.CurrencyRateDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Date and time the exchange rate was obtained.");

        entity.Property(propertyExpression: expression => expression.EndOfDayRate)
              .HasColumnType(typeName: "money")
              .HasComment(comment: "Final exchange rate for the day.");

        entity.Property(propertyExpression: expression => expression.FromCurrencyCode)
              .HasMaxLength(maxLength: 3)
              .IsFixedLength()
              .HasComment(comment: "Exchange rate was converted from this currency code.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.ToCurrencyCode)
              .HasMaxLength(maxLength: 3)
              .IsFixedLength()
              .HasComment(comment: "Exchange rate was converted to this currency code.");

        entity.HasOne(navigationExpression: expression => expression.FromCurrencyCodeNavigation)
              .WithMany(navigationExpression: expression => expression.CurrencyRateFromCurrencyCodeNavigations)
              .HasForeignKey(foreignKeyExpression: expression => expression.FromCurrencyCode)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);

        entity.HasOne(navigationExpression: expression => expression.ToCurrencyCodeNavigation)
              .WithMany(navigationExpression: expression => expression.CurrencyRateToCurrencyCodeNavigations)
              .HasForeignKey(foreignKeyExpression: expression => expression.ToCurrencyCode)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}