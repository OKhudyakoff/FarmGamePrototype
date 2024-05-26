using System;
using System.Collections.Generic;
using UnityEngine;

public class MonoObjectPool<T> where T : MonoBehaviour
{
    public T prefab { get; private set; }
    public bool autoExpand;
    public Transform container {  get; private set; }
    private List<T> pool;
    public MonoObjectPool(T prefab, int count, bool autoExpand, Transform container)
    {
        this.prefab = prefab;
        this.autoExpand = autoExpand;
        this.container = container;
        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var item in pool)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if(this.HasFreeElement(out var element))
        {
            return element;
        }
        if(this.autoExpand)
        {
            return this.CreateObject(true);
        }
        throw new Exception($"There is no free elements in pool of type {typeof(T)}");
    }
}
