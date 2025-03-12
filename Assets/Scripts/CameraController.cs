using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_possibleTargets;
    private int m_targetIndex;

    [Header("Movement")]
    [SerializeField, Min(0)] private Vector2 distanceLimits = new(10f, 100f);
    private float m_targetDistance;
    [SerializeField, Min(0)] private float m_cameraLerp = 20f;
    private float rotationX, rotationY;

    private UIManager m_ui;

    private void Awake()
    {
        m_targetDistance = distanceLimits.x;
    }

    private void Start()
    {
        m_ui = FindObjectOfType<UIManager>();
        if (m_ui == null) { Debug.LogError("UI not found :("); }
        else { m_ui.SetTargetText(m_possibleTargets[m_targetIndex].name); }
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
        rotationX += Input.GetAxis("Vertical") / 3;
        rotationY -= Input.GetAxis("Horizontal") / 3;

        rotationX = Mathf.Clamp(rotationX, -40, 50f);
        transform.eulerAngles = new(rotationX, rotationY, 0);
    }

    private void HandleMovement()
    {
        Vector3 finalPosition = Vector3.Lerp(transform.position, m_possibleTargets[m_targetIndex].transform.position - transform.forward * m_targetDistance, m_cameraLerp * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Linecast(m_possibleTargets[m_targetIndex].transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
        }

        transform.position = finalPosition;
    }

    private void HandleZoom()
    {
        m_targetDistance -= Input.mouseScrollDelta.y;
        m_targetDistance = Mathf.Clamp(m_targetDistance, distanceLimits.x, distanceLimits.y); // zoom limits
    }

    private void HandleTarget()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_targetIndex--;
            m_targetIndex = Mathf.Clamp(m_targetIndex, 0, m_possibleTargets.Length - 1);

            m_ui.SetTargetText(m_possibleTargets[m_targetIndex].name);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_targetIndex++;
            m_targetIndex = Mathf.Clamp(m_targetIndex, 0, m_possibleTargets.Length - 1);

            m_ui.SetTargetText(m_possibleTargets[m_targetIndex].name);
        }
    }

    public Vector2 camRotation { get { return new Vector2(rotationX, rotationY); } }
}