namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable(name: "Customer", buildAction: table =>
                               table.HasComment("Current customer information. Also see the Person and Store tables."));

        entity.HasIndex(indexExpression: expression => expression.AccountNumber, name: "AK_Customer_AccountNumber")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.Rowguid, name: "AK_Customer_rowguid")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.TerritoryId, name: "IX_Customer_TerritoryID");

        entity.Property(propertyExpression: expression => expression.CustomerId)
              .HasColumnName(name: "CustomerID")
              .HasComment(comment: "Primary key.");

        entity.Property(propertyExpression: expression => expression.AccountNumber)
              .HasMaxLength(maxLength: 10)
              .IsUnicode(unicode: false)
              .HasComputedColumnSql(sql: "(isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),''))", stored: false)
              .HasComment(comment: "Unique number identifying the customer assigned by the accounting system.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.PersonId)
              .HasColumnName(name: "PersonID")
              .HasComment(comment: "Foreign key to Person.BusinessEntityID");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.Property(propertyExpression: expression => expression.StoreId)
              .HasColumnName(name: "StoreID")
              .HasComment(comment: "Foreign key to Store.BusinessEntityID");

        entity.Property(propertyExpression: expression => expression.TerritoryId)
              .HasColumnName(name: "TerritoryID")
              .HasComment(comment: "ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.");

        entity.HasOne(navigationExpression: expression => expression.Store)
              .WithMany(navigationExpression: expression => expression.Customers)
              .HasForeignKey(foreignKeyExpression: expression => expression.StoreId);

        entity.HasOne(navigationExpression: expression => expression.Territory)
              .WithMany(navigationExpression: expression => expression.Customers)
              .HasForeignKey(foreignKeyExpression: expression => expression.TerritoryId);
    }
}