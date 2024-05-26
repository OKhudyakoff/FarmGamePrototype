using TMPro;
using UnityEngine;

public class GameDebugger : MonoBehaviour, IService
{
    private static GameDebugger _instance;
    private MonoObjectPool<DebugObjectTMP> _pool;
    [SerializeField] private DebugObjectTMP _textPrefab;
    [SerializeField] private Transform _container;

    public void Init()
    {
        _instance = ServiceLocator.Current.Get<GameDebugger>();
        _pool = new MonoObjectPool<DebugObjectTMP>(_textPrefab,15,true,_container);
    }

    public static void ShowInfo(string message)
    {
        var text = _instance._pool.GetFreeElement();
        text.transform.SetSiblingIndex(0);
        text.ActivateObject(message);
    }
}
