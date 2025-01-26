using System.Text.Json;

namespace Infrastructure.Helpers
{
    internal static class JsonFileHandler
    {
        private static string GetFilePath(string fileName)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(directory, "Data", fileName);
        }

        public static void WriteToFile<T>(string fileName, List<T> data)
        {
            var filePath = GetFilePath(fileName);
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static List<T> ReadFromFile<T>(string fileName)
        {
            var filePath = GetFilePath(fileName);

            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                return new List<T>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }


    }
}
