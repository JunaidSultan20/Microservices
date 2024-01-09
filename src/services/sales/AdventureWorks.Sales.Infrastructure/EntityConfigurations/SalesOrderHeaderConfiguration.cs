namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesOrderHeaderConfiguration : IEntityTypeConfiguration<SalesOrderHeader>
{
    public void Configure(EntityTypeBuilder<SalesOrderHeader> entity)
    {
        entity.HasKey(keyExpression: expression => expression.SalesOrderId)
              .HasName(name: "PK_SalesOrderHeader_SalesOrderID");

        entity.ToTable(name: "SalesOrderHeader", buildAction: table => table.HasComment(comment: "General sales order information."));

        entity.HasIndex(indexExpression: expression => expression.SalesOrderNumber, name: "AK_SalesOrderHeader_SalesOrderNumber")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.Rowguid, name: "AK_SalesOrderHeader_rowguid")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.CustomerId, name: "IX_SalesOrderHeader_CustomerID");

        entity.HasIndex(indexExpression: expression => expression.SalesPersonId, name: "IX_SalesOrderHeader_SalesPersonID");

        entity.Property(propertyExpression: expression => expression.SalesOrderId)
              .HasColumnName(name: "SalesOrderID")
              .HasComment(comment: "Primary key.");

        entity.Property(propertyExpression: expression => expression.AccountNumber)
              .HasMaxLength(maxLength: 15)
              .HasComment(comment: "Financial accounting number reference expression.");

        entity.Property(propertyExpression: expression => expression.BillToAddressId)
              .HasColumnName(name: "BillToAddressID")
              .HasComment(comment: "Customer billing address. Foreign key to Address.AddressID.");

        entity.Property(propertyExpression: expression => expression.Comment)
              .HasMaxLength(maxLength: 128)
              .HasComment(comment: "Sales representative comments.");

        entity.Property(propertyExpression: expression => expression.CreditCardApprovalCode)
              .HasMaxLength(maxLength: 15)
              .IsUnicode(unicode: false)
              .HasComment(comment: "Approval code provided by the credit card company.");

        entity.Property(propertyExpression: expression => expression.CreditCardId)
              .HasColumnName(name: "CreditCardID")
              .HasComment(comment: "Credit card identification number. Foreign key to CreditCard.CreditCardID.");

        entity.Property(propertyExpression: expression => expression.CurrencyRateId)
              .HasColumnName(name: "CurrencyRateID")
              .HasComment(comment: "Currency exchange rate used. Foreign key to CurrencyRateExpression.CurrencyRateID.");

        entity.Property(propertyExpression: expression => expression.CustomerId)
              .HasColumnName(name: "CustomerID")
              .HasComment(comment: "Customer identification number. Foreign key to Customer.BusinessEntityID.");

        entity.Property(propertyExpression: expression => expression.DueDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Date the order is due to the customer.");

        entity.Property(propertyExpression: expression => expression.Freight)
              .HasColumnType(typeName: "money")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Shipping cost.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.OnlineOrderFlag)
              .IsRequired()
              .HasDefaultValueSql(sql: "((1))")
              .HasComment(comment: "0 = Order placed by sales person. 1 = Order placed online by customer.");

        entity.Property(propertyExpression: expression => expression.OrderDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Dates the sales order was created.");

        entity.Property(propertyExpression: expression => expression.PurchaseOrderNumber)
              .HasMaxLength(maxLength: 25)
              .HasComment(comment: "Customer purchase order number reference expression.");

        entity.Property(propertyExpression: expression => expression.RevisionNumber)
              .HasComment(comment: "Incremental number to track changes to the sales order over time expression.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.Property(propertyExpression: expression => expression.SalesOrderNumber)
              .HasMaxLength(maxLength: 25)
              .HasComputedColumnSql(sql: "(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
              .HasComment(comment: "Unique sales order identification number.");

        entity.Property(propertyExpression: expression => expression.SalesPersonId)
              .HasColumnName(name: "SalesPersonID")
              .HasComment(comment: "Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.");

        entity.Property(propertyExpression: expression => expression.ShipDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Date the order was shipped to the customer.");

        entity.Property(propertyExpression: expression => expression.ShipMethodId)
              .HasColumnName(name: "ShipMethodID")
              .HasComment(comment: "Shipping method. Foreign key to ShipMethod.ShipMethodID.");

        entity.Property(propertyExpression: expression => expression.ShipToAddressId)
              .HasColumnName(name: "ShipToAddressID")
              .HasComment(comment: "Customer shipping address. Foreign key to Address.AddressID.");

        entity.Property(propertyExpression: expression => expression.Status)
              .HasDefaultValueSql(sql: "((1))")
              .HasComment(comment: "Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");

        entity.Property(propertyExpression: expression => expression.SubTotal)
              .HasColumnType(typeName: "money")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");

        entity.Property(propertyExpression: expression => expression.TaxAmt)
              .HasColumnType(typeName: "money")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Tax amount.");

        entity.Property(propertyExpression: expression => expression.TerritoryId)
              .HasColumnName(name: "TerritoryID")
              .HasComment(comment: "Territory in which the sale was made expression. Foreign key to SalesTerritory.SalesTerritoryID.");

        entity.Property(propertyExpression: expression => expression.TotalDue)
              .HasColumnType(typeName: "money")
              .HasComputedColumnSql(sql: "(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", stored: false)
              .HasComment(comment: "Total due from customer. Computed as Subtotal + TaxAmt + Freight.");

        entity.HasOne(navigationExpression: expression => expression.CreditCard)
              .WithMany(navigationExpression: expression => expression.SalesOrderHeaders)
              .HasForeignKey(foreignKeyExpression: expression => expression.CreditCardId);

        entity.HasOne(navigationExpression: expression => expression.CurrencyRate)
              .WithMany(navigationExpression: expression => expression.SalesOrderHeaders)
              .HasForeignKey(foreignKeyExpression: expression => expression.CurrencyRateId);

        entity.HasOne(navigationExpression: expression => expression.Customer)
              .WithMany(navigationExpression: expression => expression.SalesOrderHeaders)
              .HasForeignKey(foreignKeyExpression: expression => expression.CustomerId)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);

        entity.HasOne(navigationExpression: expression => expression.SalesPerson)
              .WithMany(navigationExpression: expression => expression.SalesOrderHeaders)
              .HasForeignKey(foreignKeyExpression: expression => expression.SalesPersonId);

        entity.HasOne(navigationExpression: expression => expression.Territory)
              .WithMany(navigationExpression: expression => expression.SalesOrderHeaders)
              .HasForeignKey(foreignKeyExpression: expression => expression.TerritoryId);
    }
}