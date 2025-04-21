using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Unknown1.Source.Data;

internal abstract class FileManager<T> where T : class, new()
{
    public T Data { get; private set; }


    public FileManager(string filename)
    {
        string dataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "artimelessGames", "Unknown1");
        string dataFilePath = Path.Combine(dataFolderPath, filename);

        if (File.Exists(dataFilePath))
        {
            try
            {
                string data = File.ReadAllText(dataFilePath);
                Data = JsonSerializer.Deserialize<T>(data);
                CleanNullProperties(Data);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Invalid JSON ({filename}), properties will not be read. Error: {e}");
                Data = new();
            }
        }
        else Data = new();

        // Override the file with the new data.
        // Ensure that any missing properties, which were assigned default values, are written to the file.
        Directory.CreateDirectory(dataFolderPath);
        File.WriteAllText(dataFilePath, JsonSerializer.Serialize(Data));

        // In case we have a new message to show, we override everytime the file.
        CreateWarningFile(dataFolderPath);
    }

    /// <summary>Clean properties that could be setted null by the user or by an intentional bug.</summary>
    /// <param name="Data">Unclean deserialized data</param>
    protected abstract void CleanNullProperties(T Data);

    private static void CreateWarningFile(string dataFolderPath)
    {
        string readmeFilePath = Path.Combine(dataFolderPath, "README.txt");
        File.WriteAllText(readmeFilePath,
            "*********************************WARNING*********************************\n" +
            "* Please make a backup before editing one of those files manually.  :(  *\n" +
            "* Making an error in one will reset it automatically to default values. *\n" +
            "*************************************************************************\n"
        );
    }
}
