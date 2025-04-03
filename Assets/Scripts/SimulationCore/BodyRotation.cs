using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Vector3Int axis = new(0, 0, -1);

    private void Update() { transform.Rotate(axis, rotationSpeed * Time.deltaTime); }
}