using UnityEngine;

public class UIManager : MonoBehaviour, IManager
{
    #region > PROPERTIES
    
    public GlobalEnums.ManagerType ManagerType => GlobalEnums.ManagerType.UI;
    public bool IsInitialized { get; private set; }
    
    #endregion

    #region INIT
    
    public void Init()
    {
        IsInitialized = true;
    }
    
    #endregion
}