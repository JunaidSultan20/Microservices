namespace AdventureWorks.Common.Helpers;

public static class XmlCommentsHelper
{
    public static string XmlCommentsFilePath<T>()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var fileName = typeof(T).GetTypeInfo().Assembly.GetName().Name + ".xml";
        return Path.Combine(basePath, fileName);
    }
}