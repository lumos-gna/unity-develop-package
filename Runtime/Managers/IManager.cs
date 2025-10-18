public interface IManager
{
    public ManagerType ManagerType { get; }
    public bool IsInitialized { get; }
    public void Init();
}