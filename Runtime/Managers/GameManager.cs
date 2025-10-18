using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : SingletonGlobalMono<GameManager>
{
    #region > PROPERTIES
    
    public ManagerType ManagerType => ManagerType.GAME;
    public bool IsInitialized { get; private set; }
 
    #endregion
    
    #region > FIELD

    private List<IManager> _managers = new();

    #endregion
    
    #region  > UNITY

    protected override void Awake()
    {
        base.Awake();
        
        CreateManagers();
        
        StartCoroutine(InitManagers());
    }
    
    #endregion
    
    #region  > INIT

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PreInit() => _ = Instance;
    
    private IEnumerator InitManagers()
    {
        _managers = _managers.OrderBy(manager => manager.ManagerType).ToList();

        foreach (var manager in _managers)
        {
            manager.Init();

            yield return new WaitUntil(() => manager.IsInitialized);
            DebugUtil.Log(" INIT COMPLETE ", $" {manager.ManagerType} ");
        }
        
        IsInitialized = true;
        DebugUtil.Log("", " All Managers INIT COMPLETE ");
    }

    #endregion
    
    #region  > CREATE

    /// <summary>
    /// 생성할 매니저들 등록
    /// </summary>
    private void CreateManagers()
    {
        CreateManager<GameSceneManager>();
        CreateManager<UIManager>();
    }
    
    private void CreateManager<T>() where T : MonoBehaviour, IManager
    {
        GameObject go = new GameObject(typeof(T).Name);
        var createdManager = go.AddComponent<T>();
        createdManager.transform.SetParent(transform);
        
        _managers.Add(createdManager);
        
        Global.Register(createdManager);
    }
    
    #endregion
}