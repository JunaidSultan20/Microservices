namespace AdventureWorks.Common.Helpers;

public static class ObjectExtensions
{
    public static ExpandoObject ShapeData<TSource>(this TSource source, string fields)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        ExpandoObject dataShapedObject = new ExpandoObject();
        if (string.IsNullOrWhiteSpace(fields))
        {
            // all public properties should be in the ExpandoObject 
            PropertyInfo[] propertyInfos =
                typeof(TSource).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                // get the value of the property on the source object
                object? propertyValue = propertyInfo.GetValue(source);

                // add the field to the ExpandoObject
                //((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
                (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, propertyValue);
            }
            return dataShapedObject;
        }

        // the field are separated by ",", so we split it.
        string[] fieldsAfterSplit = fields.Split(',');

        foreach (string field in fieldsAfterSplit)
        {
            // trim each field, as it might contain leading 
            // or trailing spaces. Can't trim the var in foreach,
            // so use another var.
            string propertyName = field.Trim();

            // use reflection to get the property on the source object
            // we need to include public and instance, b/c specifying a 
            // binding flag overwrites the already-existing binding flags.
            PropertyInfo? propertyInfo = typeof(TSource).GetProperty(propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new Exception($"Property {propertyName} wasn't found " +
                    $"on {typeof(TSource)}");
            }

            // get the value of the property on the source object
            object? propertyValue = propertyInfo.GetValue(source);

            // add the field to the ExpandoObject
            //((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
            (dataShapedObject as IDictionary<string, object>).Add(propertyInfo.Name, propertyValue);
        }

        // return the list
        return dataShapedObject;
    }
}