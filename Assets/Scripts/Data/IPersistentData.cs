namespace Data
{
    public interface IPersistentData
    {
        PlayerData PlayerData { get; set; }
        RefrigeratorData RefrigeratorData { get; set; }
    }
}