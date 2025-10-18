using System;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour, IManager
{
    #region > PROPERTIES
    
    public ManagerType ManagerType => ManagerType.SCENE;
    public bool IsInitialized { get; private set; }
    
    #endregion
    
    #region > FIELDS
    
    private Dictionary<Type, BaseSceneController> _activeScenes = new();
    
    #endregion    

    #region > INIT
    
    public virtual void Init()
    {
        IsInitialized = true;
    }
    
    #endregion
    
    #region > GET / SET
    
    public T GetScene<T>() where T : BaseSceneController
    {
        if (_activeScenes.TryGetValue(typeof(T), out var scene))
        {
            return scene as T;
        }

        return null;
    }
    
    #endregion
    
    #region > REGISTER

    public void RegisterScene<T>(T scene) where T : BaseSceneController
    {
        _activeScenes[scene.GetType()] = scene;
    }
    
    public void UnregisterScene<T>(T scene) where T : BaseSceneController
    {
        _activeScenes.Remove(scene.GetType());
    }
    
    #endregion
}