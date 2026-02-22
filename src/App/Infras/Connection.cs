using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Infras
{
    public class Connection
    {
        private static readonly string ConnectionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AljazSkafar", "TehnicniPregledi", "connections");

        private static string ConnectionsPath => Path.Combine(ConnectionsDir, "connections.json");

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            WriteIndented = true
        };

        public static ConnectionsFile Load()
        {
            Directory.CreateDirectory(ConnectionsDir);

            if (!File.Exists(ConnectionsPath))
            {
                var empty = new ConnectionsFile();
                Save(empty);
                return empty;
            }

            var json = File.ReadAllText(ConnectionsPath);
            return JsonSerializer.Deserialize<ConnectionsFile>(json, JsonOpts) ?? new ConnectionsFile();
        }

        public static void Save(ConnectionsFile data)
        {
            Directory.CreateDirectory(ConnectionsDir);
            var json = JsonSerializer.Serialize(data, JsonOpts);
            File.WriteAllText(ConnectionsPath, json);
        }

        public static void AddOrUpdate(string name, string baseUrl)
        {
            var data = Load();

            baseUrl = NormalizeBaseUrl(baseUrl);

            var existing = data.Items.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existing is null)
                data.Items.Add(new ConnectionItem { Name = name, BaseUrl = baseUrl });
            else
                existing.BaseUrl = baseUrl;

            if (string.IsNullOrWhiteSpace(data.Selected))
                data.Selected = name;

            Save(data);
        }

        public static void Delete(string name)
        {
            var data = Load();
            data.Items.RemoveAll(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (data.Selected != null && data.Selected.Equals(name, StringComparison.OrdinalIgnoreCase))
                data.Selected = data.Items.FirstOrDefault()?.Name;

            Save(data);
        }

        public static void Select(string name)
        {
            var data = Load();
            if (data.Items.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                data.Selected = name;
                Save(data);
            }
        }

        public static ConnectionItem? GetSelected()
        {
            var data = Load();
            if (string.IsNullOrWhiteSpace(data.Selected)) return null;
            return data.Items.FirstOrDefault(x => x.Name.Equals(data.Selected, StringComparison.OrdinalIgnoreCase));
        }

        private static string NormalizeBaseUrl(string baseUrl)
        {
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri))
                throw new ArgumentException("Base URL is not a valid absolute URL.");

            var s = uri.ToString();
            if (!s.EndsWith("/")) s += "/";
            return s;
        }
    }
}
