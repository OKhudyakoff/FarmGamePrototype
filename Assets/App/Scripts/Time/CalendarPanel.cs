using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalendarPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text dateTMP;
    [SerializeField] private GameObject hilightGO;

    public void Init(int date)
    {
        dateTMP.text = date.ToString();
    }

    public void SetHilight()
    {
        hilightGO.SetActive(true);
    }

    public void RemoveHilight()
    {
        hilightGO.SetActive(false);
    }
}
