namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SpecialOfferConfiguration : IEntityTypeConfiguration<SpecialOffer>
{
    public void Configure(EntityTypeBuilder<SpecialOffer> entity)
    {
        entity.ToTable(name: "SpecialOffer", buildAction: table => table.HasComment(comment: "Sale discounts lookup table expression."));

        entity.HasIndex(indexExpression: expression => expression.Rowguid, "AK_SpecialOffer_rowguid")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.SpecialOfferId)
              .HasColumnName(name: "SpecialOfferID")
              .HasComment(comment: "Primary key for SpecialOffer records.");

        entity.Property(propertyExpression: expression => expression.Category)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Group the discount applies to such as Reseller or Customer.");

        entity.Property(propertyExpression: expression => expression.Description)
              .HasMaxLength(maxLength: 255)
              .HasComment(comment: "Discount description.");

        entity.Property(propertyExpression: expression => expression.DiscountPct)
              .HasColumnType(typeName: "smallmoney")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Discount percentage expression.");

        entity.Property(propertyExpression: expression => expression.EndDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Discount end date expression.");

        entity.Property(propertyExpression: expression => expression.MaxQty)
              .HasComment(comment: "Maximum discount percent allowed.");

        entity.Property(propertyExpression: expression => expression.MinQty)
              .HasComment(comment: "Minimum discount percent allowed.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
            .HasColumnType(typeName: "datetime")
            .HasDefaultValueSql(sql: "(getdate())")
            .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.Property(propertyExpression: expression => expression.StartDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Discount start date expression.");

        entity.Property(propertyExpression: expression => expression.Type)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Discount type category.");
    }
}