using System.Text.Json;

namespace Backend.Helpers;

/// <summary>
/// Contiene métodos para leer y escribir en archivos JSON.
/// </summary>
public class JSONFiles
{
    /// <summary>
    /// Agrega un objeto a una lista de objetos en un archivo JSON.
    /// </summary>
    /// <param name="path">El directorio del archivo JSON</param>
    /// <param name="newObject">El objeto que se guardará</param>
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
    public static void WriteOverJSONFile<Type>(Type[] allObjects, string path)
    {
        string jsonString = JsonSerializer.Serialize<Type[]>(allObjects);

        File.WriteAllText(path, jsonString);
    }


    /// <summary>
    /// Lee el contenido de un archivo JSON con un array
    /// </summary>
    /// <param name="path">el directorio del archivo JSON</param>
    public static Type ReadJSONFile<Type>(string path)
    {
        string jsonString = File.ReadAllText(path);
        Type objectInstance = JsonSerializer.Deserialize<Type>(jsonString);

        return objectInstance;
    }
}