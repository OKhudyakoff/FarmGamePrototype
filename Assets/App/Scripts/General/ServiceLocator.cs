using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private ServiceLocator()
    {
        
    }
    public static ServiceLocator Current { get; private set; }

    private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();

    public static void Initialize()
    {
        Current = new ServiceLocator();
    }

    public void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if(_services.ContainsKey(key) )
        {
            Debug.Log($"Сервис {key} уже зарегистрирован");
            return;
        }
        _services.Add(key, service);
    }

    public void Unregister<T>() where T : IService
    {
        string key = typeof(T).Name;
        if(!_services.ContainsKey(key) )
        {
            Debug.Log($"Сервис {key} не зарегистрирован");
            return;
        }
        _services.Remove(key);
    }

    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;
        if(! _services.ContainsKey(key) )
        {
            Debug.Log($"Сервис {key} не зарегистрирован");
            throw new InvalidOperationException();
        }

        return (T)_services[key];
    }
}
