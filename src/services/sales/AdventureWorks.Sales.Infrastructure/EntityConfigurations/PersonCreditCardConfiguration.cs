namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class PersonCreditCardConfiguration : IEntityTypeConfiguration<PersonCreditCard>
{
    public void Configure(EntityTypeBuilder<PersonCreditCard> entity)
    {
        entity.HasKey(keyExpression: expression => new { expression.BusinessEntityId, expression.CreditCardId })
              .HasName(name: "PK_PersonCreditCard_BusinessEntityID_CreditCardID");

        entity.ToTable(name: "PersonCreditCard", buildAction: table =>
                           table.HasComment(comment: "Cross-reference table mapping people to their credit card information in the CreditCard table."));

        entity.Property(propertyExpression: expression => expression.BusinessEntityId)
              .HasColumnName(name: "BusinessEntityID")
              .HasComment(comment: "Business entity identification number. Foreign key to Person.BusinessEntityID.");

        entity.Property(propertyExpression: expression => expression.CreditCardId)
              .HasColumnName(name: "CreditCardID")
              .HasComment(comment: "Credit card identification number. Foreign key to CreditCard.CreditCardID.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.HasOne(navigationExpression: expression => expression.CreditCard)
              .WithMany(navigationExpression: expression => expression.PersonCreditCards)
              .HasForeignKey(foreignKeyExpression: expression => expression.CreditCardId)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}