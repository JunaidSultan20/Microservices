namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesTerritoryHistoryConfiguration : IEntityTypeConfiguration<SalesTerritoryHistory>
{
    public void Configure(EntityTypeBuilder<SalesTerritoryHistory> entity)
    {
        entity.HasKey(keyExpression: expression => new
        {
            expression.BusinessEntityId,
            expression.StartDate,
            expression.TerritoryId
        })
              .HasName(name: "PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID");

        entity.ToTable(name: "SalesTerritoryHistory", buildAction: table
                               => table.HasComment(comment: "Sales representative transfers to other sales territories."));

        entity.HasIndex(indexExpression: expression => expression.Rowguid, "AK_SalesTerritoryHistory_rowguid")
              .IsUnique();

        entity.Property(propertyExpression: expression => expression.BusinessEntityId)
              .HasColumnName(name: "BusinessEntityID")
              .HasComment(comment: "Primary key. The sales rep.  Foreign key to SalesPerson.BusinessEntityID.");

        entity.Property(propertyExpression: expression => expression.StartDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Primary key. Date the sales representative started work in the territory.");

        entity.Property(propertyExpression: expression => expression.TerritoryId)
              .HasColumnName(name: "TerritoryID")
              .HasComment(comment: "Primary key. Territory identification number. Foreign key to SalesTerritory.SalesTerritoryID.");

        entity.Property(propertyExpression: expression => expression.EndDate)
              .HasColumnType(typeName: "datetime")
              .HasComment(comment: "Date the sales representative left work in the territory.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Rowguid)
              .HasColumnName(name: "rowguid")
              .HasDefaultValueSql(sql: "(newid())")
              .HasComment(comment: "ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample expression.");

        entity.HasOne(navigationExpression: expression => expression.BusinessEntity)
              .WithMany(navigationExpression: expression => expression.SalesTerritoryHistories)
              .HasForeignKey(foreignKeyExpression: expression => expression.BusinessEntityId)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);

        entity.HasOne(navigationExpression: expression => expression.Territory)
              .WithMany(navigationExpression: expression => expression.SalesTerritoryHistories)
              .HasForeignKey(foreignKeyExpression: expression => expression.TerritoryId)
              .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
    }
}