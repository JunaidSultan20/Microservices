namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesOrderDetailConfiguration : IEntityTypeConfiguration<SalesOrderDetail>
{
    public void Configure(EntityTypeBuilder<SalesOrderDetail> entity)
    {
        entity.HasKey(keyExpression: expression => new { expression.SalesOrderId, expression.SalesOrderDetailId })
              .HasName(name: "PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

        entity.ToTable(name: "SalesOrderDetail", buildAction: table =>
                               table.HasComment(comment: "Individual products associated with a specific sales order. See SalesOrderHeader."));

        entity.HasIndex(indexExpression: expression => expression.Rowguid, name: "AK_SalesOrderDetail_rowguid")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.ProductId, name: "IX_SalesOrderDetail_ProductID");

        entity.Property(propertyExpression: expression => expression.SalesOrderId)
              .HasColumnName(name: "SalesOrderID")
              .HasComment(comment: "Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

        entity.Property(propertyExpression: expression => expression.SalesOrderDetailId)
              .ValueGeneratedOnAdd()
              .HasColumnName(name: "SalesOrderDetailID")
              .HasComment(comment: "Primary key. One incremental unique number per product sold.");

        entity.Property(propertyExpression: expression => expression.CarrierTrackingNumber)
              .HasMaxLength(maxLength: 25)
              .HasComment(comment: "Shipment tracking number supplied by the shipper.");

        entity.Property(propertyExpression: expression => expression.LineTotal)
              .HasColumnType(typeName: "numeric(38, 6)")
              .HasComputedColumnSql(sql: "(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", stored: false)
              .HasComment(comment: "Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.OrderQty)
              .HasComment(comment: "Quantity ordered per product.");

        entity.Property(propertyExpression: expression => expression.ProductId)
              .HasColumnName(name: "ProductID")
              .HasComment(comment: "Product sold to customer. Foreign key to Product.ProductID.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication samplexpression.");

        entity.Property(propertyExpression: expression => expression.SpecialOfferId)
              .HasColumnName(name: "SpecialOfferID")
              .HasComment(comment: "Promotional codexpression. Foreign key to SpecialOffer.SpecialOfferID.");

        entity.Property(propertyExpression: expression => expression.UnitPrice)
              .HasColumnType(typeName: "money")
              .HasComment(comment: "Selling price of a single product.");

        entity.Property(propertyExpression: expression => expression.UnitPriceDiscount)
              .HasColumnType(typeName: "money")
              .HasComment(comment: "Discount amount.");

        entity.HasOne(navigationExpression: expression => expression.SalesOrder)
              .WithMany(navigationExpression: expression => expression.SalesOrderDetails)
              .HasForeignKey(foreignKeyExpression: expression => expression.SalesOrderId);

        entity.HasOne(navigationExpression: expression => expression.SpecialOfferProduct)
            .WithMany(navigationExpression: expression => expression.SalesOrderDetails)
            .HasForeignKey(foreignKeyExpression: expression => new { expression.SpecialOfferId, expression.ProductId })
            .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull)
            .HasConstraintName(name: "FK_SalesOrderDetail_SpecialOfferProduct_SpecialOfferIDProductID");
    }
}