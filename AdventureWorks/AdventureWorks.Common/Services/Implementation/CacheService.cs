namespace AdventureWorks.Common.Services.Implementation;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    /// <summary>
    /// Method to get the data from cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<T> GetAsync<T>(string key)
    {
        string value = await _distributedCache.GetStringAsync(key, default);
        return (value != null ? JsonSerializer.Deserialize<T>(value) : default);
    }

    /// <summary>
    /// Sets the value in the cache with default expiration options.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task SetAsync<T>(string key, T value)
    {
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(3),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
    }

    /// <summary>
    /// Sets the value in cache with custom expiration options.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task SetWithOptionsAsync<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
    }
}