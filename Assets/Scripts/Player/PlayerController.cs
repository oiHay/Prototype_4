using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private Rigidbody _rb;
    private GameObject _focalPoint;

    public static event Action PlayerOutOfBounder;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        float _forwordlInput = Input.GetAxis("Vertical");
        float _sidewaysInput = Input.GetAxis("Horizontal");
        
        _rb.AddForce(_focalPoint.transform.forward * (_moveSpeed * _forwordlInput));
        _rb.AddForce(Vector3.right * (_moveSpeed * _sidewaysInput));

        HandleOutOfBounder();
    }
    
    private void HandleOutOfBounder()
    {
        if (transform.position.y < -8)
        {
            transform.position = new Vector3(0, 0.5f, 0);
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            PlayerOutOfBounder?.Invoke();
        }
    }
}
