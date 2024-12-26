namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for shaping data from an object.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Shapes the data of the specified object into an <see cref="ExpandoObject"/> based on the provided fields.
    /// </summary>
    /// <typeparam name="TSource">The type of the object to shape.</typeparam>
    /// <param name="source">The object to shape.</param>
    /// <param name="fields">A comma-separated list of field names to include in the shaped data. If null or empty, all properties are included.</param>
    /// <returns>An <see cref="ExpandoObject"/> representing the shaped data.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="source"/> is null.</exception>
    /// <exception cref="Exception">Thrown when a specified property in <paramref name="fields"/> is not found on <typeparamref name="TSource"/>.</exception>
    public static ExpandoObject ShapeData<TSource>(this TSource source, string? fields)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        ExpandoObject dataShapedObject = new ExpandoObject();

        if (string.IsNullOrWhiteSpace(fields))
        {
            PropertyInfo[] propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object? propertyValue = propertyInfo.GetValue(source);
                (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, value: propertyValue ?? string.Empty);
            }

            return dataShapedObject;
        }

        string[] fieldsAfterSplit = fields.Split(',');
        foreach (string field in fieldsAfterSplit)
        {
            string propertyName = field.Trim();
            PropertyInfo propertyInfo = typeof(TSource)
                                        .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ??
                                        throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");

            object? propertyValue = propertyInfo.GetValue(source);
            (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, value: propertyValue ?? string.Empty);
        }

        return dataShapedObject;
    }
}