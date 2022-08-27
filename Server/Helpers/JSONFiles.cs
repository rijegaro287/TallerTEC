using System.Text.Json;

namespace Server.Helpers;

public class JSONFiles
{
    public static void WriteJSONFile<Type>(Type newObject, string path)
    {
        Type[] allObjects = ReadJSONFile<Type[]>(path);
        Type[] newObjects = allObjects
            .Append(newObject).ToArray<Type>();

        string jsonString = JsonSerializer.Serialize<Type[]>(newObjects);

        File.WriteAllText(path, jsonString);
    }

    public static Type? ReadJSONFile<Type>(string path)
    {
        string jsonString = File.ReadAllText(path);
        Type? objectInstance = JsonSerializer.Deserialize<Type>(jsonString);

        return objectInstance;
    }
}