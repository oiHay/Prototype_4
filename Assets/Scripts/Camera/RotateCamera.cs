using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        float _rotationlInput = Input.GetAxis("Camera Rotation");
        transform.Rotate(Vector3.up, _rotationlInput * _rotationSpeed * Time.deltaTime);
    }
}
