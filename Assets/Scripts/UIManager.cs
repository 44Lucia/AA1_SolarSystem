using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currentTargetText;
    private CameraController controller;

    private void Awake() { controller = Camera.main.GetComponent<CameraController>(); }

    public void SetTargetText(string p_text) { currentTargetText.text = p_text; }

    // Buttons
    public void PreviousTarget() { controller.PreviousTarget(); }
    public void NextTarget() { controller.NextTarget(); }
}