﻿using System;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    #region  > PROPERTIES

    public static UIManager UI => Get<UIManager>();
    public static GameSceneManager Scene => Get<GameSceneManager>();
    
    #endregion

    #region > FIELDS

    private static readonly Dictionary<Type, MonoBehaviour> _instances = new();

    #endregion
    
    #region  > GET
    
    private static T Get<T>() where T : MonoBehaviour
    {
        if (_instances.TryGetValue(typeof(T), out var instance))
        {
            return instance as T;    
        }

        return null;
    }

    #endregion

    #region REGISTER

    public static void Register<T>(T instance) where T : MonoBehaviour
    {
        _instances.TryAdd(typeof(T), instance);
    }
    
    public static void Unregister<T>(T instance) where T : MonoBehaviour
    {
        _instances.Remove(instance.GetType());
    }

    #endregion
}