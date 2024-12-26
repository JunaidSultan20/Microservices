namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IDistributedCache"/>.
/// </summary>
public static class DistributedCacheExtensions
{
    /// <summary>
    /// Asynchronously sets the specified value in the distributed cache with default options.
    /// </summary>
    /// <typeparam name="T">The type of the value to be cached.</typeparam>
    /// <param name="cache">The <see cref="IDistributedCache"/> instance to use for caching.</param>
    /// <param name="key">The key under which the value will be stored.</param>
    /// <param name="value">The value to be cached.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value)
        => SetAsync(cache, key, value, new DistributedCacheEntryOptions());

    /// <summary>
    /// Asynchronously sets the specified value in the distributed cache with the given options.
    /// </summary>
    /// <typeparam name="T">The type of the value to be cached.</typeparam>
    /// <param name="cache">The <see cref="IDistributedCache"/> instance to use for caching.</param>
    /// <param name="key">The key under which the value will be stored.</param>
    /// <param name="value">The value to be cached.</param>
    /// <param name="options">The <see cref="DistributedCacheEntryOptions"/> to use for caching.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(value, GetJsonSerializerOptions()));
        return cache.SetAsync(key, bytes, options);
    }

    /// <summary>
    /// Tries to get the value associated with the specified key from the distributed cache.
    /// </summary>
    /// <typeparam name="T">The type of the value to be retrieved.</typeparam>
    /// <param name="cache">The <see cref="IDistributedCache"/> instance to use for retrieving the value.</param>
    /// <param name="key">The key of the value to be retrieved.</param>
    /// <param name="cacheValue">The value retrieved from the cache, or the default value if not found.</param>
    /// <returns><c>true</c> if the value was found and retrieved; otherwise, <c>false</c>.</returns>
    public static bool TryGetValue<T>(this IDistributedCache cache, string key, out T? cacheValue)
    {
        byte[]? value = cache.Get(key);
        cacheValue = default;
        if (value is null)
            return false;
        cacheValue = System.Text.Json.JsonSerializer.Deserialize<T>(value, GetJsonSerializerOptions());
        return true;
    }

    /// <summary>
    /// Gets the <see cref="JsonSerializerOptions"/> used for JSON serialization and deserialization.
    /// </summary>
    /// <returns>The <see cref="JsonSerializerOptions"/> instance with configured settings.</returns>
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