namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesPersonConfiguration : IEntityTypeConfiguration<SalesPerson>
{
    public void Configure(EntityTypeBuilder<SalesPerson> entity)
    {
        entity.HasKey(keyExpression: expression => expression.BusinessEntityId)
              .HasName(name: "PK_SalesPerson_BusinessEntityID");

        entity.ToTable(name: "SalesPerson", buildAction: table => table.HasComment(comment: "Sales representative current information."));

        entity.HasIndex(indexExpression: expression => expression.Rowguid, name: "AK_SalesPerson_rowguid")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.BusinessEntityId)
              .ValueGeneratedNever()
              .HasColumnName(name: "BusinessEntityID")
              .HasComment(comment: "Primary key for SalesPerson records. Foreign key to EmployeeExpression.BusinessEntityID");

        entity.Property(propertyExpression: expression => expression.Bonus)
              .HasColumnType(typeName: "money")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Bonus due if quota is met.");

        entity.Property(propertyExpression: expression => expression.CommissionPct)
              .HasColumnType(typeName: "smallmoney")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Commission percent received per sale expression.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.Property(propertyExpression: expression => expression.SalesLastYear)
              .HasColumnType(typeName: "money")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Sales total of previous year.");

        entity.Property(propertyExpression: expression => expression.SalesQuota)
              .HasColumnType(typeName: "money")
              .HasComment(comment: "Projected yearly sales.");

        entity.Property(propertyExpression: expression => expression.SalesYtd)
              .HasColumnType(typeName: "money")
              .HasColumnName(name: "SalesYTD")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Sales total year to date expression.");

        entity.Property(propertyExpression: expression => expression.TerritoryId)
              .HasColumnName(name: "TerritoryID")
              .HasComment(comment: "Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.");

        entity.HasOne(navigationExpression: expression => expression.Territory)
              .WithMany(navigationExpression: expression => expression.SalesPeople)
              .HasForeignKey(foreignKeyExpression: expression => expression.TerritoryId);
    }
}