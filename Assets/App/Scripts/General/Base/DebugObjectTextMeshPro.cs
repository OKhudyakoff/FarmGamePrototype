using TMPro;
using UnityEngine;

public class DebugObjectTMP: MonoBehaviour
{
    [SerializeField] private float _destroyTime = 10f;
    [SerializeField] private TMP_Text _text;
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        gameObject.SetActive(false);
    }

    // Метод для активации объекта
    public void ActivateObject(string text)
    {
        if(_text == null)
        {
            _text = GetComponent<TMP_Text>();
            if(text == null)
            {
                Debug.Log(text);
                return;
            }
        }
        _text.text = text;
        // Активируем объект
        gameObject.SetActive(true);
        // Создаем таймер на 5 секунд
        Invoke("DeactivateObject", _destroyTime);
    }

    // Метод для деактивации объекта через 5 секунд
    private void DeactivateObject()
    {
        // Деактивируем объект
        gameObject.SetActive(false);
    }
}
