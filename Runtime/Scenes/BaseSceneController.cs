using System.Collections;
using UnityEngine;

public abstract class BaseSceneController : MonoBehaviour
{
    #region > UNITY
    protected virtual void Awake()
    {
        StartCoroutine(RegisterManager());
    }

    protected virtual void OnDestroy()
    {
        Global.Scene?.UnregisterScene(this);
    }
    
    #endregion
    
    #region > INIT
    
    public abstract void Init();

    private IEnumerator RegisterManager()
    {
        yield return new WaitUntil(() => GameManager.Instance != null && GameManager.Instance.IsInitialized);
        
        Global.Scene.RegisterScene(this);
        
        Init();
    }
    
    #endregion
}