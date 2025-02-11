namespace Core.Data.SaveLoad
{
    public interface IDataService
    {
        bool SaveData<T>(string relativePath, T data, bool encrypted = false);
        T LoadData<T>(string relativePath, bool encrypted = false);
    }
}