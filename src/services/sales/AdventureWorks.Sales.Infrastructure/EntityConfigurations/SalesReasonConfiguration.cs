namespace AdventureWorks.Sales.Infrastructure.EntityConfigurations;

public class SalesReasonConfiguration : IEntityTypeConfiguration<SalesReason>
{
    public void Configure(EntityTypeBuilder<SalesReason> entity)
    {
        entity.ToTable(name: "SalesReason", buildAction: table
                           => table.HasComment(comment: "Lookup table of customer purchase reasons."));

        entity.Property(propertyExpression: expression => expression.SalesReasonId)
              .HasColumnName(name: "SalesReasonID")
              .HasComment(comment: "Primary key for SalesReason records.");

        entity.Property(propertyExpression: expression => expression.ModifiedDate)
              .HasColumnType(typeName: "datetime")
              .HasDefaultValueSql(sql: "(getdate())")
              .HasComment(comment: "Date and time the record was last updated.");

        entity.Property(propertyExpression: expression => expression.Name)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Sales reason description.");

        entity.Property(propertyExpression: expression => expression.ReasonType)
              .HasMaxLength(maxLength: 50)
              .HasComment(comment: "Category the sales reason belongs to.");
    }
}