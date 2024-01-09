namespace AdventureWorks.Common.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// Object extension method for data shaping the contents of the source
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ExpandoObject ShapeData<TSource>(this TSource source, string? fields)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var dataShapedObject = new ExpandoObject();

        if (string.IsNullOrWhiteSpace(fields))
        {
            var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(source);

                (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, value: propertyValue ?? string.Empty);
            }

            return dataShapedObject;
        }

        var fieldsAfterSplit = fields.Split(',');

        foreach (var field in fieldsAfterSplit)
        {
            var propertyName = field.Trim();

            var propertyInfo = typeof(TSource)
                               .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ??
                               throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");

            var propertyValue = propertyInfo.GetValue(source);

            (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, value: propertyValue ?? string.Empty);
        }

        return dataShapedObject;
    }
}