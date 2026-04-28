using System;
using UnityEngine;


public class FacingCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 150f;
    
    private void Update()
    {

        transform.Rotate(0, rotationSpeed * Time.deltaTime,0, Space.Self);
    }
}
