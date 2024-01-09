namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesPersonQuotaHistoryConfiguration : IEntityTypeConfiguration<SalesPersonQuotaHistory>
{
    public void Configure(EntityTypeBuilder<SalesPersonQuotaHistory> entity)
    {
        entity.HasKey(keyExpression: expression => new { expression.BusinessEntityId, expression.QuotaDate })
              .HasName(name: "PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate");

        entity.ToTable(name: "SalesPersonQuotaHistory", buildAction: table => table.HasComment(comment: "Sales performance tracking."));

        entity.HasIndex(indexExpression: expression => expression.Rowguid, "AK_SalesPersonQuotaHistory_rowguid")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.BusinessEntityId)
              .HasColumnName(name: "BusinessEntityID")
              .HasComment(comment: "Sales person identification number. Foreign key to SalesPerson.BusinessEntityID.");

        entity.Property(propertyExpression: expression => expression.QuotaDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Sales quota date expression.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.Property(propertyExpression: expression => expression.SalesQuota)
              .HasColumnType(typeName: "money")
              .HasComment(comment: "Sales quota amount.");

        entity.HasOne(navigationExpression: expression => expression.BusinessEntity)
              .WithMany(navigationExpression: expression => expression.SalesPersonQuotaHistories)
              .HasForeignKey(foreignKeyExpression: expression => expression.BusinessEntityId)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}