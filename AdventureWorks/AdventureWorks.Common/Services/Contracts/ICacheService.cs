namespace AdventureWorks.Common.Services.Contracts;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key);

    Task SetAsync<T>(string key, T value);

    Task SetWithOptionsAsync<T>(string key, T value, DistributedCacheEntryOptions options);
}