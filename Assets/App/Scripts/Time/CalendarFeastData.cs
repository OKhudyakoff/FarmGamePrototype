using UnityEngine;
using Utilities;

[CreateAssetMenu(menuName = "Calendar/Feast", fileName = "FeastData")]
public class CalendarFeastData : ScriptableObject
{
    [SerializeField] private string _feastName;
    [SerializeField] private DateTime _feastDate;
    [SerializeField] private Sprite _feastIcon;

    public string FeastName => _feastName;
    public DateTime FeastDate => _feastDate;
    public Sprite FeastIcon => _feastIcon;
}
