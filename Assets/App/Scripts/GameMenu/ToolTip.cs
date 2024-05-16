using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour, IService
{
    [SerializeField] private TMP_Text _headerTMP;
    [SerializeField] private TMP_Text _descriptionTMP;
    [SerializeField] private Image _image;
    [SerializeField] private int _characterWrapLimit;
    [SerializeField] private LayoutElement _layout;
    private InputHandler _inputHandler;
    private RectTransform _rectTransform;

    public void Show(string info, string header = "", Sprite spr = null)
    {
        int headerLenght = header.Length;
        int infoLenght = info.Length;


        _descriptionTMP.text = info;
        _layout.enabled = (headerLenght > _characterWrapLimit || infoLenght > _characterWrapLimit) ? true : false;

        if(string.IsNullOrEmpty(header))
        {
            _headerTMP.gameObject.SetActive(false);
        }
        else
        {
            _headerTMP.gameObject.SetActive(true);
            _headerTMP.text = header;
        }

        if(spr != null)
        {
            _image.sprite = spr;
            _image.gameObject.SetActive(true);
        }
        else
        {
            _image.gameObject.SetActive(false);
        }

        UpdatePosition();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void Init()
    {
        _inputHandler = ServiceLocator.Current.Get<InputHandler>();
        _rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 position = _inputHandler.MousePosition;
        var normalizedPosition = new Vector2(position.x / Screen.width, position.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        _rectTransform.pivot = pivot;
        transform.position = position;
    }

    private Vector2 CalculatePivot(Vector2 normalizedPosition)
    {
        var pivotTopLeft = new Vector2(-0.05f, 1.05f);
        var pivotTopRight = new Vector2(1.05f, 1.05f);
        var pivotBottomLeft = new Vector2(-0.05f, -0.05f);
        var pivotBottomRight = new Vector2(1.05f, -0.05f);

        if (normalizedPosition.x < 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopLeft;
        }
        else if (normalizedPosition.x > 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopRight;
        }
        else if (normalizedPosition.x <= 0.5f && normalizedPosition.y < 0.5f)
        {
            return pivotBottomLeft;
        }
        else
        {
            return pivotBottomRight;
        }
    }
}
