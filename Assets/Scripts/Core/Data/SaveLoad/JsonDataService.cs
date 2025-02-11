using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Data.SaveLoad
{
    public class JsonDataService : IDataService
    {
        private const string Key = "";
        private const string IV = "";
        
        public bool SaveData<T>(string relativePath, T data, bool encrypted = false)
        {
            var dataPath = Application.persistentDataPath + relativePath;

            try
            {
                if (File.Exists(dataPath))
                {
                    Debug.Log("Data already exists. Deleting old file and creating new.");
                    File.Delete(dataPath);
                }
                else
                {
                    Debug.Log("Writing file for the first time.");
                }

                using var stream = File.Create(dataPath);
                
                if (encrypted)
                {
                    WriteEncryptedData(data, stream);
                }
                else
                {
                    stream.Close();
                    File.WriteAllText(dataPath, JsonConvert.SerializeObject(data, Formatting.Indented));
                }
                
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Unable to save data: {e.Message} {e.StackTrace}");
                
                return false;
            }
        }

        private void WriteEncryptedData<T>(T data, FileStream stream)
        {
            using var aesProvider = Aes.Create();
            
            aesProvider.Key = Convert.FromBase64String(Key);
            aesProvider.IV = Convert.FromBase64String(IV);
            
            using var encryptor = aesProvider.CreateEncryptor();
            using var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
            
            cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data)));
        }

        public T LoadData<T>(string relativePath, bool encrypted = false)
        {
            var dataPath = Application.persistentDataPath + relativePath;

            if (File.Exists(dataPath) == false)
            {
                Debug.LogError($"Unable to load data at: {dataPath}. File does not exist.");
                throw new FileNotFoundException($"{dataPath} does not exist.");
            }

            try
            {
                return encrypted 
                    ? ReadEncryptedData<T>(dataPath) 
                    : JsonConvert.DeserializeObject<T>(File.ReadAllText(dataPath));
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
                throw;
            }
        }

        private T ReadEncryptedData<T>(string path)
        {
            var fileBytes = File.ReadAllBytes(path);
            using var aesProvider = Aes.Create();
            
            aesProvider.Key = Convert.FromBase64String(Key);
            aesProvider.IV = Convert.FromBase64String(IV);
            
            using var cryptoTransform  = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
            using var decryptionStream = new MemoryStream(fileBytes);
            using var cryptoStream = new CryptoStream(decryptionStream, cryptoTransform, CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream);

            var result = reader.ReadToEnd();
            
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}