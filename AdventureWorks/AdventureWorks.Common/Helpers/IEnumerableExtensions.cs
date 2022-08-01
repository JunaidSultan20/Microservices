namespace AdventureWorks.Common.Helpers;

public static class IEnumerableExtensions
{
    public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source,
        string? fields)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        List<ExpandoObject> expandoObjectList = new List<ExpandoObject>();

        List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            PropertyInfo[] propertyInfos =
                typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public |
                                              BindingFlags.Instance);
            propertyInfoList.AddRange(propertyInfos);
        }
        else
        {
            string[] fieldsAfterSplit = fields.Split(',');
            foreach (string field in fieldsAfterSplit)
            {
                string propertyName = field.Trim();
                var propertyInfo = typeof(TSource).GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                propertyInfoList.Add(propertyInfo);
            }
        }

        foreach (TSource sourceObject in source)
        {
            ExpandoObject dataShapedObject = new ExpandoObject();
            foreach (PropertyInfo? propertyInfo in propertyInfoList)
            {
                var propertyValue = propertyInfo.GetValue(sourceObject);
                (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, propertyValue);
                //((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
            }
            expandoObjectList.Add(dataShapedObject);
        }

        return expandoObjectList;
    }
}