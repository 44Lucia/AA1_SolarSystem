using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_possibleTargets;
    private int m_targetIndex;

    [Header("Movement")]
    [SerializeField, Min(0)] private Vector2 distanceLimits = new(1.5f, 50f);
    [SerializeField, Min(0)] private float m_cameraLerp = 20f;
    private float m_targetDistance;
    private float rotationX, rotationY;

    private UIManager m_ui;

    private void Awake()
    {
        m_targetDistance = distanceLimits.y / 2;
    }

    private void Start()
    {
        m_ui = FindObjectOfType<UIManager>();

        if (m_ui == null) { Debug.LogError("UI not found :("); return; }
        m_ui.SetTargetText(m_possibleTargets[m_targetIndex].name);
    }

    private void LateUpdate()
    {
        HandleRotation();
        HandleMovement();

        HandleTarget();
        HandleZoom();
    }

    private void HandleRotation()
    {
        // WASD
        rotationX += Input.GetAxis("Vertical") / 3;
        rotationY -= Input.GetAxis("Horizontal") / 3;

        // MOUSE DELTA
        if (Input.GetMouseButton(0)) // drag screen
        {
            rotationX -= Input.GetAxis("Mouse Y") * 2.0f;
            rotationY += Input.GetAxis("Mouse X") * 2.0f;
        }

        rotationX = Mathf.Clamp(rotationX, -40, 50f); // -40,50 --> camera angle limits
        transform.eulerAngles = new(rotationX, rotationY, 0);
    }

    private void HandleMovement()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            m_possibleTargets[m_targetIndex].transform.position - transform.forward * m_targetDistance,
            m_cameraLerp * Time.deltaTime
        );
    }

    private void HandleZoom()
    {
        m_targetDistance -= Input.mouseScrollDelta.y;

        if (Input.GetKey(KeyCode.DownArrow)) { m_targetDistance += 0.1f; } // v = -zoom
        else if (Input.GetKey(KeyCode.UpArrow)) { m_targetDistance -= 0.1f; } // ^ = +zoom

        m_targetDistance = Mathf.Clamp(m_targetDistance, distanceLimits.x, distanceLimits.y); // zoom limits
    }

    private void HandleTarget()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow)) // Q or <- = prev planet
        {
            m_targetIndex--;
            m_targetIndex = Mathf.Clamp(m_targetIndex, 0, m_possibleTargets.Length - 1);

            m_ui.SetTargetText(m_possibleTargets[m_targetIndex].name);
        }
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow)) // E or -> = next planet
        {
            m_targetIndex++;
            m_targetIndex = Mathf.Clamp(m_targetIndex, 0, m_possibleTargets.Length - 1);

            m_ui.SetTargetText(m_possibleTargets[m_targetIndex].name);
        }

        // right click
        if (Input.GetMouseButtonDown(1))
        {
            if(++m_targetIndex == m_possibleTargets.Length) { m_targetIndex = 0; }
            m_ui.SetTargetText(m_possibleTargets[m_targetIndex].name);
        }
    }
}