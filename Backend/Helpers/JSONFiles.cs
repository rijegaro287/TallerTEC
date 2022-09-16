using System.Text.Json;

namespace Backend.Helpers;

/// <summary>
/// This class contains methods to read and write JSON files.
/// </summary>
public class JSONFiles
{
    /// <summary>
    /// Writes an object to a JSON file.
    /// </summary>
    /// <param name="path">The path of the JSON file.</param>
    /// <param name="newObject">The object to write.</param>
    public static void WriteJSONFile<Type>(Type newObject, string path)
    {
        Type[] allObjects = ReadJSONFile<Type[]>(path);
        Type[] newObjects = allObjects
            .Append(newObject).ToArray<Type>();

        string jsonString = JsonSerializer.Serialize<Type[]>(newObjects);

        File.WriteAllText(path, jsonString);
    }

    /// <summary>
    /// Writes Over all the JSON file.
    /// </summary>
    /// <param name="path">The path of the JSON file.</param>
    /// <param name="allObject">The object written in the file.</param>
    public static void WriteOverJSONFile<Type>(Type[] allObjects , string path)
    {
        string jsonString = JsonSerializer.Serialize<Type[]>(allObjects);

        File.WriteAllText(path, jsonString);
    }


    /// <summary>
    /// Reads a JSON file.
    /// </summary>
    /// <param name="path">The path of the JSON file.</param>
    public static Type ReadJSONFile<Type>(string path)
    {
        string jsonString = File.ReadAllText(path);
        Type objectInstance = JsonSerializer.Deserialize<Type>(jsonString);

        return objectInstance;
    }
}