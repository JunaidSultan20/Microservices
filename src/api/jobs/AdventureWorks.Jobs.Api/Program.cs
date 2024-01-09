using AdventureWorks.Common.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(options =>
                                 options.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                        .UseSimpleAssemblyNameTypeSerializer()
                                        .UseRecommendedSerializerSettings()
                                        .UseSqlServerStorage(
                                             builder.Configuration.GetConnectionString(Constants.DefaultConnection),
                                             new SqlServerStorageOptions
                                             {
                                                 CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                                 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                                 QueuePollInterval = TimeSpan.Zero,
                                                 UseRecommendedIsolationLevel = true,
                                                 DisableGlobalLocks = true
                                             }));

builder.Services.AddHangfireServer();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseHangfireDashboard("/dashboard", new DashboardOptions
{
    Authorization = new[] { new HangFireAuthorizationFilter() }
});

app.UseAuthorization();

app.MapControllers();

app.Run();