using System;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    #region  > FIELD
    
    public static UIManager UI => Get<UIManager>();
    
    private static readonly Dictionary<Type, MonoBehaviour> _managers = new();

    #endregion
    
    #region  > GET
    
    private static T Get<T>() where T : MonoBehaviour
    {
        if (_managers.TryGetValue(typeof(T), out var manager))
        {
            return manager as T;    
        }

        return null;
    }

    #endregion

    #region REGISTER

    public static void Register<T>(T manager) where T : MonoBehaviour
    {
        _managers.TryAdd(typeof(T), manager);
    }

    #endregion
}