namespace AdventureWorks.Common.Extensions;

/// <summary>
/// Provides extension methods for shaping data from an enumerable collection of objects.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Shapes the data of the objects in the specified enumerable collection into a collection of <see cref="ExpandoObject"/> based on the provided fields.
    /// </summary>
    /// <typeparam name="TSource">The type of the objects in the enumerable collection.</typeparam>
    /// <param name="source">The enumerable collection of objects to shape.</param>
    /// <param name="fields">A comma-separated list of field names to include in the shaped data. If null or empty, all properties are included.</param>
    /// <returns>An <see cref="IEnumerable{ExpandoObject}"/> representing the shaped data of the objects in the enumerable collection.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="source"/> is null.</exception>
    /// <exception cref="Exception">Thrown when a specified property in <paramref name="fields"/> is not found on <typeparamref name="TSource"/>.</exception>
    public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string? fields)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        List<ExpandoObject> expandoObjectList = new List<ExpandoObject>();
        List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            PropertyInfo[] propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            propertyInfoList.AddRange(propertyInfos);
        }
        else
        {
            string[] fieldsAfterSplit = fields.Split(',');
            foreach (string field in fieldsAfterSplit)
            {
                string propertyName = field.Trim();
                PropertyInfo propertyInfo = typeof(TSource)
                                            .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ?? 
                                            throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                propertyInfoList.Add(propertyInfo);
            }
        }

        foreach (TSource sourceObject in source)
        {
            ExpandoObject dataShapedObject = new ExpandoObject();
            propertyInfoList.ForEach(propertyInfo =>
            {
                object? propertyValue = propertyInfo.GetValue(sourceObject);
                (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, value: propertyValue ?? string.Empty);
            });
            expandoObjectList.Add(dataShapedObject);
        }

        return expandoObjectList;
    }
}