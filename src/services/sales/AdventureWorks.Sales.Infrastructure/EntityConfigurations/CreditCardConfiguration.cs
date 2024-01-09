namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

internal class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> entity)
    {
        entity.ToTable(name: "CreditCard", buildAction: table => table.HasComment(comment: "Customer credit card information."));

        entity.HasIndex(indexExpression: expression => expression.CardNumber, name: "AK_CreditCard_CardNumber")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.CreditCardId)
              .HasColumnName(name: "CreditCardID")
              .HasComment(comment: "Primary key for CreditCard records.");

        entity.Property(propertyExpression: expression => expression.CardNumber)
              .HasMaxLength(maxLength: 25)
              .HasComment(comment: "Credit card number.");

        entity.Property(propertyExpression: expression => expression.CardType)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Credit card name.");

        entity.Property(propertyExpression: expression => expression.ExpMonth)
              .HasComment(comment: "Credit card expiration month.");

        entity.Property(propertyExpression: expression => expression.ExpYear)
              .HasComment(comment: "Credit card expiration year.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");
    }
}