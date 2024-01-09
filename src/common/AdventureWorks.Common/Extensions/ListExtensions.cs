namespace AdventureWorks.Common.Extensions;

public static class ListExtensions
{
    /// <summary>
    /// List extension method for data shaping the contents of the source
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<ExpandoObject> ShapeData<TSource>(this List<TSource> source, string? fields)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        List<ExpandoObject> expandoObjectList = new List<ExpandoObject>();

        List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            var propertyInfos = typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            propertyInfoList.AddRange(propertyInfos);
        }
        else
        {
            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();

                var propertyInfo = typeof(TSource)
                                   .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ??
                                   throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");

                propertyInfoList.Add(propertyInfo);
            }
        }

        foreach (TSource sourceObject in source)
        {
            var dataShapedObject = new ExpandoObject();

            propertyInfoList.ForEach(propertyInfo =>
            {
                var propertyValue = propertyInfo.GetValue(sourceObject);

                (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, value: propertyValue ?? string.Empty);
            });

            expandoObjectList.Add(dataShapedObject);
        }

        return expandoObjectList;
    }
}