namespace AdventureWorks.Sales.Infrastructure.Context;

/// <summary>
/// Represents the database context for the AdventureWorks Sales database.
/// </summary>
public partial class AdventureWorksSalesContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdventureWorksSalesContext"/> class.
    /// </summary>
    public AdventureWorksSalesContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AdventureWorksSalesContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public AdventureWorksSalesContext(DbContextOptions<AdventureWorksSalesContext> options) : base(options: options)
    {
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{CountryRegionCurrency}"/> for the CountryRegionCurrency table.
    /// </summary>
    public virtual DbSet<CountryRegionCurrency> CountryRegionCurrencies { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{CreditCard}"/> for the CreditCard table.
    /// </summary>
    public virtual DbSet<CreditCard> CreditCards { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Currency}"/> for the Currency table.
    /// </summary>
    public virtual DbSet<Currency> Currencies { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{CurrencyRate}"/> for the CurrencyRate table.
    /// </summary>
    public virtual DbSet<CurrencyRate> CurrencyRates { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Customer}"/> for the Customer table.
    /// </summary>
    public virtual DbSet<Customer> Customers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{PersonCreditCard}"/> for the PersonCreditCard table.
    /// </summary>
    public virtual DbSet<PersonCreditCard> PersonCreditCards { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesOrderDetail}"/> for the SalesOrderDetail table.
    /// </summary>
    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesOrderHeader}"/> for the SalesOrderHeader table.
    /// </summary>
    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesOrderHeaderSalesReason}"/> for the SalesOrderHeaderSalesReason table.
    /// </summary>
    public virtual DbSet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesPerson}"/> for the SalesPerson table.
    /// </summary>
    public virtual DbSet<SalesPerson> SalesPeople { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesPersonQuotaHistory}"/> for the SalesPersonQuotaHistory table.
    /// </summary>
    public virtual DbSet<SalesPersonQuotaHistory> SalesPersonQuotaHistories { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesReason}"/> for the SalesReason table.
    /// </summary>
    public virtual DbSet<SalesReason> SalesReasons { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesTaxRate}"/> for the SalesTaxRate table.
    /// </summary>
    public virtual DbSet<SalesTaxRate> SalesTaxRates { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesTerritory}"/> for the SalesTerritory table.
    /// </summary>
    public virtual DbSet<SalesTerritory> SalesTerritories { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SalesTerritoryHistory}"/> for the SalesTerritoryHistory table.
    /// </summary>
    public virtual DbSet<SalesTerritoryHistory> SalesTerritoryHistories { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{ShoppingCartItem}"/> for the ShoppingCartItem table.
    /// </summary>
    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SpecialOffer}"/> for the SpecialOffer table.
    /// </summary>
    public virtual DbSet<SpecialOffer> SpecialOffers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{SpecialOfferProduct}"/> for the SpecialOfferProduct table.
    /// </summary>
    public virtual DbSet<SpecialOfferProduct> SpecialOfferProducts { get; set; } = null!;

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Store}"/> for the Store table.
    /// </summary>
    public virtual DbSet<Store> Stores { get; set; } = null!;

    /// <summary>
    /// Configures the options for the database context.
    /// </summary>
    /// <param name="optionsBuilder">The options builder to configure.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseLazyLoadingProxies();

        optionsBuilder.EnableSensitiveDataLogging(false);

        //optionsBuilder.UseSecondLevelCache();
    }

    /// <summary>
    /// Configures the model for the context.
    /// </summary>
    /// <param name="modelBuilder">The model builder to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(schema: "Sales");

        modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());

        OnModelCreatingPartial(modelBuilder);
    }

    /// <summary>
    /// Allows derived classes to add custom configurations to the model during the creation process.
    /// </summary>
    /// <param name="modelBuilder">The model builder to configure the model.</param>
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}