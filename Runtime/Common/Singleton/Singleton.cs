using UnityEngine;

public abstract class Singleton<T> where T : class, new()
{
    public static T Instance
    {
        get
        {
            return _instance ??= new T();
        }
    }

    private static T _instance;
}





