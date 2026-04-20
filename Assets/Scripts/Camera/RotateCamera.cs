using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, _horizontalInput * _rotationSpeed * Time.deltaTime);
    }
}
