using System.Text.Json;

namespace TravelbilityApp.Infrastructure.Data
{
    internal static class JsonSeeder
    {
        public static List<T> GetData<T>(string fileName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/Configurations/Seeders");

            fileName = fileName.EndsWith("y")
                ? fileName.Replace("y", "ies")
                : $"{fileName}s";

            var json = File.ReadAllText($"{path}/{fileName}.json");
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            
            return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
        }
    }
}
