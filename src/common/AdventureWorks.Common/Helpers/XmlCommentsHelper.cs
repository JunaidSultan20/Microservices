namespace AdventureWorks.Common.Helpers;

/// <summary>
/// Helper class to provide the file path for XML comments documentation files.
/// </summary>
public static class XmlCommentsHelper
{
    /// <summary>
    /// Gets the file path for the XML comments file of the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type whose XML comments file path is required.</typeparam>
    /// <returns>The full path to the XML comments file.</returns>
    public static string XmlCommentsFilePath<T>()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string fileName = typeof(T).GetTypeInfo().Assembly.GetName().Name + ".xml";
        return Path.Combine(basePath, fileName);
    }
}