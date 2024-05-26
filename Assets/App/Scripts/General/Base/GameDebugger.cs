using TMPro;
using UnityEngine;

public class GameDebugger : MonoBehaviour
{
    private static GameDebugger _instance;
    private MonoObjectPool<DebugObjectTMP> _pool;
    [SerializeField] private DebugObjectTMP _textPrefab;
    [SerializeField] private Transform _container;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        _pool = new MonoObjectPool<DebugObjectTMP>(_textPrefab,15,true,_container);
    }

    public static void ShowInfo(string message)
    {
        var text = _instance._pool.GetFreeElement();
        text.transform.SetSiblingIndex(0);
        text.ActivateObject(message);
    }
}
