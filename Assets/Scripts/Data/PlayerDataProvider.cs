using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public class PlayerDataProvider : IDataProvider
    {
        private const string FileName = "PlayerData";
        private const string FileExtension = ".json";

        private readonly IPersistentData _persistentData;

        private string SavePath => Application.persistentDataPath;
        private string FullDataPath => Path.Combine(SavePath, $"{FileName}{FileExtension}");
        
        public PlayerDataProvider(IPersistentData persistentData)
        {
            _persistentData = persistentData;
        }

        public void Save()
        {
            var playerDataObject = JsonConvert.SerializeObject(_persistentData.PlayerData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.All
            });
            
            File.WriteAllText(FullDataPath, playerDataObject);
        }

        public bool TryLoad()
        {
            if (AlreadyExists() == false) return false;

            var json = File.ReadAllText(FullDataPath);
            _persistentData.PlayerData = JsonConvert.DeserializeObject<PlayerData>(json);
            return true;
        }
        
        private bool AlreadyExists() => File.Exists(FullDataPath);
    }
}