using Microsoft.Extensions.Caching.Distributed;

namespace AdventureWorks.Common.Test.Extensions;

public class DistributedCacheExtensionsTest
{
    private readonly Mock<IDistributedCache> _cacheMock = new();

    [Fact]
    public async Task SetAsync_ShouldStoreValueInCache_WithDefaultOptions()
    {
        // Arrange
        string key = "testKey";
        var value = new TestClass { Id = 1, Name = "Test" };
        var serializedValue = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));

        _cacheMock.Setup(cache => cache.SetAsync(key, serializedValue, It.IsAny<DistributedCacheEntryOptions>(), default))
                  .Returns(Task.CompletedTask)
                  .Verifiable();

        // Act
        await _cacheMock.Object.SetAsync(key, value);

        // Assert
        _cacheMock.Verify();
    }

    [Fact]
    public async Task SetAsync_ShouldStoreValueInCache_WithCustomOptions()
    {
        // Arrange
        string key = "testKey";
        var value = new TestClass { Id = 1, Name = "Test" };
        var options = new DistributedCacheEntryOptions();
        var serializedValue = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));

        _cacheMock.Setup(cache => cache.SetAsync(key, serializedValue, options, default))
                  .Returns(Task.CompletedTask)
                  .Verifiable();

        // Act
        await _cacheMock.Object.SetAsync(key, value, options);

        // Assert
        _cacheMock.Verify();
    }

    [Fact]
    public void TryGetValue_ShouldReturnFalse_WhenKeyDoesNotExist()
    {
        // Arrange
        string key = "testKey";
        _cacheMock.Setup(cache => cache.Get(key)).Returns((byte[])null);

        // Act
        var result = _cacheMock.Object.TryGetValue<TestClass>(key, out var cacheValue);

        // Assert
        result.Should().BeFalse();
        cacheValue.Should().BeNull();
    }

    [Fact]
    public void TryGetValue_ShouldReturnTrue_WhenKeyExists()
    {
        // Arrange
        string key = "testKey";
        var value = new TestClass { Id = 1, Name = "Test" };
        var serializedValue = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, GetJsonSerializerOptions()));

        _cacheMock.Setup(cache => cache.Get(key)).Returns(serializedValue);

        // Act
        var result = _cacheMock.Object.TryGetValue<TestClass>(key, out var cacheValue);

        // Assert
        result.Should().BeTrue();
        cacheValue.Should().BeEquivalentTo(value);
    }

    private JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
    }

    private class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}