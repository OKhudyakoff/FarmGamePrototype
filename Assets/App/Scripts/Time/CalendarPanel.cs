using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalendarPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _dateTMP;
    [SerializeField] private GameObject _hilightGO;
    [SerializeField] private Image _feastIcon;
    private Calendar _calendar;
    private CalendarFeastData _feastData;

    public void Init(Calendar calendar, int date)
    {
        _calendar = calendar;
        _dateTMP.text = date.ToString();
        _feastData = null;
        _feastIcon.sprite = null;
        _feastIcon.gameObject.SetActive(false);
    }

    public void SetHilight()
    {
        _hilightGO.SetActive(true);
    }

    public void RemoveHilight()
    {
        _hilightGO.SetActive(false);
    }

    public void ClearFeastData()
    {
        _feastData = null;
        _feastIcon.sprite = null;
        _feastIcon.gameObject.SetActive(false);
    }

    public void AssignFeastData(CalendarFeastData data)
    {
        _feastData = data;
        _feastIcon.sprite = data.FeastIcon;
        _feastIcon.gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_feastData != null) _calendar.OnPointerEnter(_feastData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _calendar.OnPointerExit();
    }
}
