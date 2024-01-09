namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> entity)
    {
        entity.ToTable(name: "ShoppingCartItem", buildAction: table
                               => table.HasComment(comment: "Contains online customer orders until the order is submitted or cancelled."));

        entity.HasIndex(indexExpression: expression => new { expression.ShoppingCartId, expression.ProductId },
                                                                     name: "IX_ShoppingCartItem_ShoppingCartID_ProductID");

        entity.Property(propertyExpression: expression => expression.ShoppingCartItemId)
              .HasColumnName(name: "ShoppingCartItemID")
              .HasComment(comment: "Primary key for ShoppingCartItem records.");

        entity.Property(propertyExpression: expression => expression.DateCreated)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date the time the record was created.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.ProductId)
              .HasColumnName(name: "ProductID")
              .HasComment(comment: "Product ordered. Foreign key to Product.ProductID.");

        entity.Property(propertyExpression: expression => expression.Quantity)
              .HasDefaultValueSql(sql: "((1))")
              .HasComment(comment: "Product quantity ordered.");

        entity.Property(propertyExpression: expression => expression.ShoppingCartId)
              .HasMaxLength(maxLength: 50)
              .HasColumnName(name: "ShoppingCartID")
              .HasComment(comment: "Shopping cart identification number.");
    }
}