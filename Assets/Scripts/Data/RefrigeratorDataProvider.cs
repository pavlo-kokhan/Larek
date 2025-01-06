using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public class RefrigeratorDataProvider : IDataProvider
    {
        private const string FileName = "RefrigeratorData";
        private const string FileExtension = ".json";

        private readonly IPersistentData _persistentData;
        
        private string SavePath => Application.persistentDataPath;
        private string FullDataPath => Path.Combine(SavePath, $"{FileName}{FileExtension}");
        
        public RefrigeratorDataProvider(IPersistentData persistentData)
        {
            _persistentData = persistentData;
        }
        
        public void Save()
        {
            var playerDataObject = JsonConvert.SerializeObject(_persistentData.RefrigeratorData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.All
            });
            
            File.WriteAllText(FullDataPath, playerDataObject);
        }

        public bool TryLoad()
        {
            if (FileAlreadyExists() == false) return false;

            var json = File.ReadAllText(FullDataPath);
            _persistentData.RefrigeratorData = JsonConvert.DeserializeObject<RefrigeratorData>(json);
            return true;
        }
        
        private bool FileAlreadyExists() => File.Exists(FullDataPath);
    }
}