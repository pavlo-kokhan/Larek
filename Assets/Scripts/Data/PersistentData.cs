namespace Data
{
    public class PersistentData : IPersistentData
    {
        public PlayerData PlayerData { get; set; }
        public RefrigeratorData RefrigeratorData { get; set; }
    }
}