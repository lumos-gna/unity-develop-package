using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : SingletonGlobalMono<GameManager>, IManager
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
        
        _managers.Add(Instance);

        CreateManagers();
        
        StartCoroutine(InitManagers());
    }
    
    #endregion
    
    #region  > INIT

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PreInit()
    {
        var _ = Instance;
    }
    
    public void Init()
    {
        IsInitialized = true;
    }

    private IEnumerator InitManagers()
    {
        _managers = _managers.OrderBy(manager => manager.ManagerType).ToList();

        foreach (var manager in _managers)
        {
            manager.Init();

            yield return new WaitUntil(() => manager.IsInitialized);
            DebugUtil.Log(" INIT COMPLETE ", $" {manager.ManagerType} ");
        }
    }

    #endregion
    
    #region  > CREATE

    /// <summary>
    /// 생성할 매니저들 등록
    /// </summary>
    private void CreateManagers()
    {
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