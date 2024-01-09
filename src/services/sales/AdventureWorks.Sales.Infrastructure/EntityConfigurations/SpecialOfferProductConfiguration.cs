namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SpecialOfferProductConfiguration : IEntityTypeConfiguration<SpecialOfferProduct>
{
    public void Configure(EntityTypeBuilder<SpecialOfferProduct> entity)
    {
        entity.HasKey(keyExpression: expression => new { expression.SpecialOfferId, expression.ProductId })
              .HasName(name: "PK_SpecialOfferProduct_SpecialOfferID_ProductID");

        entity.ToTable(name: "SpecialOfferProduct", buildAction: table
                               => table.HasComment(comment: "Cross-reference table mapping products to special offer discounts."));

        entity.HasIndex(indexExpression: expression => expression.Rowguid, name: "AK_SpecialOfferProduct_rowguid")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.ProductId, name: "IX_SpecialOfferProduct_ProductID");

        entity.Property(propertyExpression: expression => expression.SpecialOfferId)
              .HasColumnName(name: "SpecialOfferID")
              .HasComment(comment: "Primary key for SpecialOfferProduct records.");

        entity.Property(propertyExpression: expression => expression.ProductId)
              .HasColumnName(name: "ProductID")
              .HasComment(comment: "Product identification number. Foreign key to Product.ProductID.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.HasOne(navigationExpression: expression => expression.SpecialOffer)
              .WithMany(navigationExpression: expression => expression.SpecialOfferProducts)
              .HasForeignKey(foreignKeyExpression: expression => expression.SpecialOfferId)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}