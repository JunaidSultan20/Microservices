namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesTerritoryExpression : IEntityTypeConfiguration<SalesTerritory>
{
    public void Configure(EntityTypeBuilder<SalesTerritory> entity)
    {
        entity.HasKey(keyExpression: expression => expression.TerritoryId)
              .HasName(name: "PK_SalesTerritory_TerritoryID");

        entity.ToTable(name: "SalesTerritory", buildAction: table => table.HasComment(comment: "Sales territory lookup table expression."));

        entity.HasIndex(indexExpression: expression => expression.Name, name: "AK_SalesTerritory_Name")
              .IsUnique();

        entity.HasIndex(indexExpression: expression => expression.Rowguid, name: "AK_SalesTerritory_rowguid")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.TerritoryId)
              .HasColumnName(name: "TerritoryID")
              .HasComment(comment: "Primary key for SalesTerritory records.");

        entity.Property(propertyExpression: expression => expression.CostLastYear)
              .HasColumnType(typeName: "money")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Business costs in the territory the previous year.");

        entity.Property(propertyExpression: expression => expression.CostYtd)
              .HasColumnType(typeName: "money")
              .HasColumnName(name: "CostYTD")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Business costs in the territory year to date expression.");

        entity.Property(propertyExpression: expression => expression.CountryRegionCode)
              .HasMaxLength(maxLength: 3)
              .HasComment(comment: "ISO standard country or region code expression. Foreign key to CountryRegion.CountryRegionCodeExpression.");

        entity.Property(propertyExpression: expression => expression.Group)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Geographic area to which the sales territory belong.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Name)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Sales territory description");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.Property(propertyExpression: expression => expression.SalesLastYear)
              .HasColumnType(typeName: "money")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Sales in the territory the previous year.");

        entity.Property(propertyExpression: expression => expression.SalesYtd)
              .HasColumnType(typeName: "money")
              .HasColumnName(name: "SalesYTD")
              .HasDefaultValueSql(sql: "((0.00))")
              .HasComment(comment: "Sales in the territory year to date expression.");
    }
}