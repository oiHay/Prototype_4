using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody _rb;
    private GameObject _focalPoint;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        float _forwordlInput = Input.GetAxis("Vertical");
        _rb.AddForce(_focalPoint.transform.forward * (_moveSpeed * _forwordlInput));
    }
}
