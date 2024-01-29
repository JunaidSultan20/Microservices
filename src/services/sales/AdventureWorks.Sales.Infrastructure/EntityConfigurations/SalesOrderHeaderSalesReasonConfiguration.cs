namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesOrderHeaderSalesReasonConfiguration : IEntityTypeConfiguration<SalesOrderHeaderSalesReason>
{
    public void Configure(EntityTypeBuilder<SalesOrderHeaderSalesReason> entity)
    {
        entity.HasKey(keyExpression: expression => new { expression.SalesOrderId, expression.SalesReasonId })
              .HasName(name: "PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID");

        entity.ToTable(name: "SalesOrderHeaderSalesReason", buildAction: table
                           => table.HasComment(comment: "Cross-reference table mapping sales orders to sales reason codes."));

        entity.Property(propertyExpression: expression => expression.SalesOrderId)
              .HasColumnName(name: "SalesOrderID")
              .HasComment(comment: "Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

        entity.Property(propertyExpression: expression => expression.SalesReasonId)
              .HasColumnName(name: "SalesReasonID")
              .HasComment(comment: "Primary key. Foreign key to SalesReason.SalesReasonID.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.HasOne(navigationExpression: expression => expression.SalesOrder)
              .WithMany(navigationExpression: expression => expression.SalesOrderHeaderSalesReasons)
              .HasForeignKey(foreignKeyExpression: expression => expression.SalesOrderId);

        entity.HasOne(navigationExpression: expression => expression.SalesReason)
              .WithMany(navigationExpression: expression => expression.SalesOrderHeaderSalesReasons)
              .HasForeignKey(foreignKeyExpression: expression => expression.SalesReasonId)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}