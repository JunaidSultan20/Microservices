namespace AdventureWorks.Sales.Infrastructure.Context;

public partial class AdventureWorksSalesContext : DbContext
{
    public AdventureWorksSalesContext()
    {
    }

    public AdventureWorksSalesContext(DbContextOptions<AdventureWorksSalesContext> options) : base(options: options)
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

        optionsBuilder.EnableSensitiveDataLogging(false);

        //optionsBuilder.UseSecondLevelCache();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(schema: "Sales");

        modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}