using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButtonUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text text;

    public Button InteractionButton => _button;
    public TMP_Text InteractionText => text;
}
