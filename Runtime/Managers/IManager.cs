public interface IManager
{
    public GlobalEnums.ManagerType ManagerType { get; }
    public bool IsInitialized { get; }
    public void Init();
}