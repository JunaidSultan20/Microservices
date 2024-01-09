namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> entity)
    {
        entity.HasKey(keyExpression: expression => expression.BusinessEntityId)
              .HasName(name: "PK_Store_BusinessEntityID");

        entity.ToTable(name: "Store", buildAction: table => table.HasComment(comment: "Customers (resellers) of Adventure Works products."));

        entity.HasIndex(indexExpression: expression => expression.Rowguid, name: "AK_Store_rowguid")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.SalesPersonId, name: "IX_Store_SalesPersonID");

        entity.HasIndex(indexExpression: expression => expression.Demographics, name: "PXML_Store_Demographics");

        entity.Property(propertyExpression: expression => expression.BusinessEntityId)
              .ValueGeneratedNever()
              .HasColumnName(name: "BusinessEntityID")
              .HasComment(comment: "Primary key. Foreign key to Customer.BusinessEntityID.");

        entity.Property(propertyExpression: expression => expression.Demographics)
              .HasColumnType(typeName: "xml")
              .HasComment(comment: "Demographic information about the store such as the number of employees, annual sales and store type expression.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Name)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Name of the store expression.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.Property(propertyExpression: expression => expression.SalesPersonId)
              .HasColumnName(name: "SalesPersonID")
              .HasComment(comment: "ID of the sales person assigned to the customer. Foreign key to SalesPerson.BusinessEntityID.");

        entity.HasOne(navigationExpression: expression => expression.SalesPerson)
              .WithMany(navigationExpression: expression => expression.Stores)
              .HasForeignKey(foreignKeyExpression: expression => expression.SalesPersonId);
    }
}