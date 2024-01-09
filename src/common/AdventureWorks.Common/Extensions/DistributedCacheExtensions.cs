namespace AdventureWorks.Common.Extensions;

public static class DistributedCacheExtensions
{
    /// <summary>
    /// Extension method for setting the value in the cache database with key and value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key" example="exampleKey1"></param>
    /// <param name="value" example="exampleValue1"></param>
    /// <returns></returns>
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
    {
        return SetAsync(cache, key, value, new DistributedCacheEntryOptions());
    }

    /// <summary>
    /// Extension method for setting the value in the cache database with key, value and cache options.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key" example="exampleKey1"></param>
    /// <param name="value" example="exampleValue1"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
    {
        var bytes = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
        return cache.SetAsync(key, bytes, options);
    }

    /// <summary>
    /// Extension method for getting the value from the cache database via key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key" example="exampleKey1"></param>
    /// <param name="cacheValue" example="exampleValue1"></param>
    /// <returns></returns>
    public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? cacheValue)
    {
        byte[]? value = cache.Get(key);
        cacheValue = default;
        if (value is null)
            return false;
        cacheValue = System.Text.Json.JsonSerializer.Deserialize<T>(value, GetJsonSerializerOptions());
        return true;
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }
}