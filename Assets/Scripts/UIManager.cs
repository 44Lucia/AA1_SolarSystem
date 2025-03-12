using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currentTargetText;


    public void SetTargetText(string p_text) { currentTargetText.text = p_text; }
}
