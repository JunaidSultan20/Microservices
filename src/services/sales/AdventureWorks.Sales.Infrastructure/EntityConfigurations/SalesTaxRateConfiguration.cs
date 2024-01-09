namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesTaxRateConfiguration : IEntityTypeConfiguration<SalesTaxRate>
{
    public void Configure(EntityTypeBuilder<SalesTaxRate> entity)
    {
        entity.ToTable(name: "SalesTaxRate", buildAction: table => table.HasComment(comment: "Tax rate lookup table expression."));

        entity.HasIndex(indexExpression: expression => new { expression.StateProvinceId, expression.TaxType },
                                                                  name: "AK_SalesTaxRate_StateProvinceID_TaxType")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.Rowguid, "AK_SalesTaxRate_rowguid")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.SalesTaxRateId)
              .HasColumnName(name: "SalesTaxRateID")
              .HasComment(comment: "Primary key for SalesTaxRate records.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Name)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Tax rate description.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.Property(propertyExpression: expression => expression.StateProvinceId)
              .HasColumnName(name: "StateProvinceID")
              .HasComment(comment: "State, province, or country/region the sales tax applies to.");

        entity.Property(propertyExpression: expression => expression.TaxRate)
              .HasColumnType(typeName: "smallmoney")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Tax rate amount.");

        entity.Property(propertyExpression: expression => expression.TaxType)
              .HasComment(comment: "1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales (retail and wholesale) transactions.");
    }
}