namespace Sales.Infrastructure.Persistence;

public partial class AdventureWorksSalesContext : DbContext
{
    public AdventureWorksSalesContext()
    {
    }

    public AdventureWorksSalesContext(DbContextOptions<AdventureWorksSalesContext> options) : base(options)
    {
    }

    public virtual DbSet<CountryRegionCurrency> CountryRegionCurrencies { get; set; } = null!;
    public virtual DbSet<CreditCard> CreditCards { get; set; } = null!;
    public virtual DbSet<Currency> Currencies { get; set; } = null!;
    public virtual DbSet<CurrencyRate> CurrencyRates { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<PersonCreditCard> PersonCreditCards { get; set; } = null!;
    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; } = null!;
    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; } = null!;
    public virtual DbSet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; set; } = null!;
    public virtual DbSet<SalesPerson> SalesPeople { get; set; } = null!;
    public virtual DbSet<SalesPersonQuotaHistory> SalesPersonQuotaHistories { get; set; } = null!;
    public virtual DbSet<SalesReason> SalesReasons { get; set; } = null!;
    public virtual DbSet<SalesTaxRate> SalesTaxRates { get; set; } = null!;
    public virtual DbSet<SalesTerritory> SalesTerritories { get; set; } = null!;
    public virtual DbSet<SalesTerritoryHistory> SalesTerritoryHistories { get; set; } = null!;
    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;
    public virtual DbSet<SpecialOffer> SpecialOffers { get; set; } = null!;
    public virtual DbSet<SpecialOfferProduct> SpecialOfferProducts { get; set; } = null!;
    public virtual DbSet<Store> Stores { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=host.docker.internal;Database=AdventureWorks.Sales;User ID=AdventureWorksSales;Password=adventureworkssales;Trusted_Connection=False;");
        }

        //optionsBuilder.UseSecondLevelCache();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Sales");

        modelBuilder.Entity<CountryRegionCurrency>(entity =>
        {
            entity.HasKey(e => new { e.CountryRegionCode, e.CurrencyCode })
                .HasName("PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode");

            entity.ToTable("CountryRegionCurrency");

            entity.HasComment("Cross-reference table mapping ISO currency codes to a country or region.");

            entity.HasIndex(e => e.CurrencyCode, "IX_CountryRegionCurrency_CurrencyCode");

            entity.Property(e => e.CountryRegionCode)
                .HasMaxLength(3)
                .HasComment("ISO code for countries and regions. Foreign key to CountryRegion.CountryRegionCode.");

            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("ISO standard currency code. Foreign key to Currency.CurrencyCode.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.CurrencyCodeNavigation)
                .WithMany(p => p.CountryRegionCurrencies)
                .HasForeignKey(d => d.CurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.ToTable("CreditCard");

            entity.HasComment("Customer credit card information.");

            entity.HasIndex(e => e.CardNumber, "AK_CreditCard_CardNumber")
                .IsUnique();

            entity.Property(e => e.CreditCardId)
                .HasColumnName("CreditCardID")
                .HasComment("Primary key for CreditCard records.");

            entity.Property(e => e.CardNumber)
                .HasMaxLength(25)
                .HasComment("Credit card number.");

            entity.Property(e => e.CardType)
                .HasMaxLength(50)
                .HasComment("Credit card name.");

            entity.Property(e => e.ExpMonth).HasComment("Credit card expiration month.");

            entity.Property(e => e.ExpYear).HasComment("Credit card expiration year.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyCode)
                .HasName("PK_Currency_CurrencyCode");

            entity.ToTable("Currency");

            entity.HasComment("Lookup table containing standard ISO currencies.");

            entity.HasIndex(e => e.Name, "AK_Currency_Name")
                .IsUnique();

            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("The ISO code for the Currency.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Currency name.");
        });

        modelBuilder.Entity<CurrencyRate>(entity =>
        {
            entity.ToTable("CurrencyRate");

            entity.HasComment("Currency exchange rates.");

            entity.HasIndex(e => new { e.CurrencyRateDate, e.FromCurrencyCode, e.ToCurrencyCode }, "AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode")
                .IsUnique();

            entity.Property(e => e.CurrencyRateId)
                .HasColumnName("CurrencyRateID")
                .HasComment("Primary key for CurrencyRate records.");

            entity.Property(e => e.AverageRate)
                .HasColumnType("money")
                .HasComment("Average exchange rate for the day.");

            entity.Property(e => e.CurrencyRateDate)
                .HasColumnType("datetime")
                .HasComment("Date and time the exchange rate was obtained.");

            entity.Property(e => e.EndOfDayRate)
                .HasColumnType("money")
                .HasComment("Final exchange rate for the day.");

            entity.Property(e => e.FromCurrencyCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Exchange rate was converted from this currency code.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ToCurrencyCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Exchange rate was converted to this currency code.");

            entity.HasOne(d => d.FromCurrencyCodeNavigation)
                .WithMany(p => p.CurrencyRateFromCurrencyCodeNavigations)
                .HasForeignKey(d => d.FromCurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ToCurrencyCodeNavigation)
                .WithMany(p => p.CurrencyRateToCurrencyCodeNavigations)
                .HasForeignKey(d => d.ToCurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasComment("Current customer information. Also see the Person and Store tables.");

            entity.HasIndex(e => e.AccountNumber, "AK_Customer_AccountNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_Customer_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.TerritoryId, "IX_Customer_TerritoryID");

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasComment("Primary key.");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComputedColumnSql("(isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),''))", false)
                .HasComment("Unique number identifying the customer assigned by the accounting system.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasComment("Foreign key to Person.BusinessEntityID");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StoreId)
                .HasColumnName("StoreID")
                .HasComment("Foreign key to Store.BusinessEntityID");

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasComment("ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.StoreId);

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.TerritoryId);
        });

        modelBuilder.Entity<PersonCreditCard>(entity =>
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.CreditCardId })
                .HasName("PK_PersonCreditCard_BusinessEntityID_CreditCardID");

            entity.ToTable("PersonCreditCard");

            entity.HasComment("Cross-reference table mapping people to their credit card information in the CreditCard table. ");

            entity.Property(e => e.BusinessEntityId)
                .HasColumnName("BusinessEntityID")
                .HasComment("Business entity identification number. Foreign key to Person.BusinessEntityID.");

            entity.Property(e => e.CreditCardId)
                .HasColumnName("CreditCardID")
                .HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.CreditCard)
                .WithMany(p => p.PersonCreditCards)
                .HasForeignKey(d => d.CreditCardId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId })
                .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

            entity.ToTable("SalesOrderDetail");

            entity.HasComment("Individual products associated with a specific sales order. See SalesOrderHeader.");

            entity.HasIndex(e => e.Rowguid, "AK_SalesOrderDetail_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.ProductId, "IX_SalesOrderDetail_ProductID");

            entity.Property(e => e.SalesOrderId)
                .HasColumnName("SalesOrderID")
                .HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

            entity.Property(e => e.SalesOrderDetailId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SalesOrderDetailID")
                .HasComment("Primary key. One incremental unique number per product sold.");

            entity.Property(e => e.CarrierTrackingNumber)
                .HasMaxLength(25)
                .HasComment("Shipment tracking number supplied by the shipper.");

            entity.Property(e => e.LineTotal)
                .HasColumnType("numeric(38, 6)")
                .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", false)
                .HasComment("Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OrderQty).HasComment("Quantity ordered per product.");

            entity.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .HasComment("Product sold to customer. Foreign key to Product.ProductID.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SpecialOfferId)
                .HasColumnName("SpecialOfferID")
                .HasComment("Promotional code. Foreign key to SpecialOffer.SpecialOfferID.");

            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasComment("Selling price of a single product.");

            entity.Property(e => e.UnitPriceDiscount)
                .HasColumnType("money")
                .HasComment("Discount amount.");

            entity.HasOne(d => d.SalesOrder)
                .WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => d.SalesOrderId);

            entity.HasOne(d => d.SpecialOfferProduct)
                .WithMany(p => p.SalesOrderDetails)
                .HasForeignKey(d => new { d.SpecialOfferId, d.ProductId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrderDetail_SpecialOfferProduct_SpecialOfferIDProductID");
        });

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId)
                .HasName("PK_SalesOrderHeader_SalesOrderID");

            entity.ToTable("SalesOrderHeader");

            entity.HasComment("General sales order information.");

            entity.HasIndex(e => e.SalesOrderNumber, "AK_SalesOrderHeader_SalesOrderNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_SalesOrderHeader_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.CustomerId, "IX_SalesOrderHeader_CustomerID");

            entity.HasIndex(e => e.SalesPersonId, "IX_SalesOrderHeader_SalesPersonID");

            entity.Property(e => e.SalesOrderId)
                .HasColumnName("SalesOrderID")
                .HasComment("Primary key.");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(15)
                .HasComment("Financial accounting number reference.");

            entity.Property(e => e.BillToAddressId)
                .HasColumnName("BillToAddressID")
                .HasComment("Customer billing address. Foreign key to Address.AddressID.");

            entity.Property(e => e.Comment)
                .HasMaxLength(128)
                .HasComment("Sales representative comments.");

            entity.Property(e => e.CreditCardApprovalCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("Approval code provided by the credit card company.");

            entity.Property(e => e.CreditCardId)
                .HasColumnName("CreditCardID")
                .HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.");

            entity.Property(e => e.CurrencyRateId)
                .HasColumnName("CurrencyRateID")
                .HasComment("Currency exchange rate used. Foreign key to CurrencyRate.CurrencyRateID.");

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasComment("Customer identification number. Foreign key to Customer.BusinessEntityID.");

            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasComment("Date the order is due to the customer.");

            entity.Property(e => e.Freight)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Shipping cost.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OnlineOrderFlag)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");

            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Dates the sales order was created.");

            entity.Property(e => e.PurchaseOrderNumber)
                .HasMaxLength(25)
                .HasComment("Customer purchase order number reference. ");

            entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesOrderNumber)
                .HasMaxLength(25)
                .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
                .HasComment("Unique sales order identification number.");

            entity.Property(e => e.SalesPersonId)
                .HasColumnName("SalesPersonID")
                .HasComment("Sales person who created the sales order. Foreign key to SalesPerson.BusinessEntityID.");

            entity.Property(e => e.ShipDate)
                .HasColumnType("datetime")
                .HasComment("Date the order was shipped to the customer.");

            entity.Property(e => e.ShipMethodId)
                .HasColumnName("ShipMethodID")
                .HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");

            entity.Property(e => e.ShipToAddressId)
                .HasColumnName("ShipToAddressID")
                .HasComment("Customer shipping address. Foreign key to Address.AddressID.");

            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");

            entity.Property(e => e.SubTotal)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");

            entity.Property(e => e.TaxAmt)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Tax amount.");

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasComment("Territory in which the sale was made. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.Property(e => e.TotalDue)
                .HasColumnType("money")
                .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false)
                .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.");

            entity.HasOne(d => d.CreditCard)
                .WithMany(p => p.SalesOrderHeaders)
                .HasForeignKey(d => d.CreditCardId);

            entity.HasOne(d => d.CurrencyRate)
                .WithMany(p => p.SalesOrderHeaders)
                .HasForeignKey(d => d.CurrencyRateId);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.SalesOrderHeaders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SalesPerson)
                .WithMany(p => p.SalesOrderHeaders)
                .HasForeignKey(d => d.SalesPersonId);

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.SalesOrderHeaders)
                .HasForeignKey(d => d.TerritoryId);
        });

        modelBuilder.Entity<SalesOrderHeaderSalesReason>(entity =>
        {
            entity.HasKey(e => new { e.SalesOrderId, e.SalesReasonId })
                .HasName("PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID");

            entity.ToTable("SalesOrderHeaderSalesReason");

            entity.HasComment("Cross-reference table mapping sales orders to sales reason codes.");

            entity.Property(e => e.SalesOrderId)
                .HasColumnName("SalesOrderID")
                .HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

            entity.Property(e => e.SalesReasonId)
                .HasColumnName("SalesReasonID")
                .HasComment("Primary key. Foreign key to SalesReason.SalesReasonID.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.SalesOrder)
                .WithMany(p => p.SalesOrderHeaderSalesReasons)
                .HasForeignKey(d => d.SalesOrderId);

            entity.HasOne(d => d.SalesReason)
                .WithMany(p => p.SalesOrderHeaderSalesReasons)
                .HasForeignKey(d => d.SalesReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesPerson>(entity =>
        {
            entity.HasKey(e => e.BusinessEntityId)
                .HasName("PK_SalesPerson_BusinessEntityID");

            entity.ToTable("SalesPerson");

            entity.HasComment("Sales representative current information.");

            entity.HasIndex(e => e.Rowguid, "AK_SalesPerson_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .ValueGeneratedNever()
                .HasColumnName("BusinessEntityID")
                .HasComment("Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID");

            entity.Property(e => e.Bonus)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Bonus due if quota is met.");

            entity.Property(e => e.CommissionPct)
                .HasColumnType("smallmoney")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Commision percent received per sale.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesLastYear)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales total of previous year.");

            entity.Property(e => e.SalesQuota)
                .HasColumnType("money")
                .HasComment("Projected yearly sales.");

            entity.Property(e => e.SalesYtd)
                .HasColumnType("money")
                .HasColumnName("SalesYTD")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales total year to date.");

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasComment("Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.SalesPeople)
                .HasForeignKey(d => d.TerritoryId);
        });

        modelBuilder.Entity<SalesPersonQuotaHistory>(entity =>
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.QuotaDate })
                .HasName("PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate");

            entity.ToTable("SalesPersonQuotaHistory");

            entity.HasComment("Sales performance tracking.");

            entity.HasIndex(e => e.Rowguid, "AK_SalesPersonQuotaHistory_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .HasColumnName("BusinessEntityID")
                .HasComment("Sales person identification number. Foreign key to SalesPerson.BusinessEntityID.");

            entity.Property(e => e.QuotaDate)
                .HasColumnType("datetime")
                .HasComment("Sales quota date.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesQuota)
                .HasColumnType("money")
                .HasComment("Sales quota amount.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.SalesPersonQuotaHistories)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesReason>(entity =>
        {
            entity.ToTable("SalesReason");

            entity.HasComment("Lookup table of customer purchase reasons.");

            entity.Property(e => e.SalesReasonId)
                .HasColumnName("SalesReasonID")
                .HasComment("Primary key for SalesReason records.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Sales reason description.");

            entity.Property(e => e.ReasonType)
                .HasMaxLength(50)
                .HasComment("Category the sales reason belongs to.");
        });

        modelBuilder.Entity<SalesTaxRate>(entity =>
        {
            entity.ToTable("SalesTaxRate");

            entity.HasComment("Tax rate lookup table.");

            entity.HasIndex(e => new { e.StateProvinceId, e.TaxType }, "AK_SalesTaxRate_StateProvinceID_TaxType")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_SalesTaxRate_rowguid")
                .IsUnique();

            entity.Property(e => e.SalesTaxRateId)
                .HasColumnName("SalesTaxRateID")
                .HasComment("Primary key for SalesTaxRate records.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Tax rate description.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StateProvinceId)
                .HasColumnName("StateProvinceID")
                .HasComment("State, province, or country/region the sales tax applies to.");

            entity.Property(e => e.TaxRate)
                .HasColumnType("smallmoney")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Tax rate amount.");

            entity.Property(e => e.TaxType).HasComment("1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales (retail and wholesale) transactions.");
        });

        modelBuilder.Entity<SalesTerritory>(entity =>
        {
            entity.HasKey(e => e.TerritoryId)
                .HasName("PK_SalesTerritory_TerritoryID");

            entity.ToTable("SalesTerritory");

            entity.HasComment("Sales territory lookup table.");

            entity.HasIndex(e => e.Name, "AK_SalesTerritory_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_SalesTerritory_rowguid")
                .IsUnique();

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasComment("Primary key for SalesTerritory records.");

            entity.Property(e => e.CostLastYear)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Business costs in the territory the previous year.");

            entity.Property(e => e.CostYtd)
                .HasColumnType("money")
                .HasColumnName("CostYTD")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Business costs in the territory year to date.");

            entity.Property(e => e.CountryRegionCode)
                .HasMaxLength(3)
                .HasComment("ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. ");

            entity.Property(e => e.Group)
                .HasMaxLength(50)
                .HasComment("Geographic area to which the sales territory belong.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Sales territory description");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesLastYear)
                .HasColumnType("money")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales in the territory the previous year.");

            entity.Property(e => e.SalesYtd)
                .HasColumnType("money")
                .HasColumnName("SalesYTD")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales in the territory year to date.");
        });

        modelBuilder.Entity<SalesTerritoryHistory>(entity =>
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.StartDate, e.TerritoryId })
                .HasName("PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID");

            entity.ToTable("SalesTerritoryHistory");

            entity.HasComment("Sales representative transfers to other sales territories.");

            entity.HasIndex(e => e.Rowguid, "AK_SalesTerritoryHistory_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .HasColumnName("BusinessEntityID")
                .HasComment("Primary key. The sales rep.  Foreign key to SalesPerson.BusinessEntityID.");

            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasComment("Primary key. Date the sales representive started work in the territory.");

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasComment("Primary key. Territory identification number. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasComment("Date the sales representative left work in the territory.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.SalesTerritoryHistories)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.SalesTerritoryHistories)
                .HasForeignKey(d => d.TerritoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ShoppingCartItem>(entity =>
        {
            entity.ToTable("ShoppingCartItem");

            entity.HasComment("Contains online customer orders until the order is submitted or cancelled.");

            entity.HasIndex(e => new { e.ShoppingCartId, e.ProductId }, "IX_ShoppingCartItem_ShoppingCartID_ProductID");

            entity.Property(e => e.ShoppingCartItemId)
                .HasColumnName("ShoppingCartItemID")
                .HasComment("Primary key for ShoppingCartItem records.");

            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date the time the record was created.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .HasComment("Product ordered. Foreign key to Product.ProductID.");

            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("((1))")
                .HasComment("Product quantity ordered.");

            entity.Property(e => e.ShoppingCartId)
                .HasMaxLength(50)
                .HasColumnName("ShoppingCartID")
                .HasComment("Shopping cart identification number.");
        });

        modelBuilder.Entity<SpecialOffer>(entity =>
        {
            entity.ToTable("SpecialOffer");

            entity.HasComment("Sale discounts lookup table.");

            entity.HasIndex(e => e.Rowguid, "AK_SpecialOffer_rowguid")
                .IsUnique();

            entity.Property(e => e.SpecialOfferId)
                .HasColumnName("SpecialOfferID")
                .HasComment("Primary key for SpecialOffer records.");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasComment("Group the discount applies to such as Reseller or Customer.");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasComment("Discount description.");

            entity.Property(e => e.DiscountPct)
                .HasColumnType("smallmoney")
                .HasDefaultValueSql("((0.00))")
                .HasComment("Discount precentage.");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasComment("Discount end date.");

            entity.Property(e => e.MaxQty).HasComment("Maximum discount percent allowed.");

            entity.Property(e => e.MinQty).HasComment("Minimum discount percent allowed.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasComment("Discount start date.");

            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasComment("Discount type category.");
        });

        modelBuilder.Entity<SpecialOfferProduct>(entity =>
        {
            entity.HasKey(e => new { e.SpecialOfferId, e.ProductId })
                .HasName("PK_SpecialOfferProduct_SpecialOfferID_ProductID");

            entity.ToTable("SpecialOfferProduct");

            entity.HasComment("Cross-reference table mapping products to special offer discounts.");

            entity.HasIndex(e => e.Rowguid, "AK_SpecialOfferProduct_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.ProductId, "IX_SpecialOfferProduct_ProductID");

            entity.Property(e => e.SpecialOfferId)
                .HasColumnName("SpecialOfferID")
                .HasComment("Primary key for SpecialOfferProduct records.");

            entity.Property(e => e.ProductId)
                .HasColumnName("ProductID")
                .HasComment("Product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.SpecialOffer)
                .WithMany(p => p.SpecialOfferProducts)
                .HasForeignKey(d => d.SpecialOfferId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.BusinessEntityId)
                .HasName("PK_Store_BusinessEntityID");

            entity.ToTable("Store");

            entity.HasComment("Customers (resellers) of Adventure Works products.");

            entity.HasIndex(e => e.Rowguid, "AK_Store_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.SalesPersonId, "IX_Store_SalesPersonID");

            entity.HasIndex(e => e.Demographics, "PXML_Store_Demographics");

            entity.Property(e => e.BusinessEntityId)
                .ValueGeneratedNever()
                .HasColumnName("BusinessEntityID")
                .HasComment("Primary key. Foreign key to Customer.BusinessEntityID.");

            entity.Property(e => e.Demographics)
                .HasColumnType("xml")
                .HasComment("Demographic informationg about the store such as the number of employees, annual sales and store type.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Name of the store.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesPersonId)
                .HasColumnName("SalesPersonID")
                .HasComment("ID of the sales person assigned to the customer. Foreign key to SalesPerson.BusinessEntityID.");

            entity.HasOne(d => d.SalesPerson)
                .WithMany(p => p.Stores)
                .HasForeignKey(d => d.SalesPersonId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}