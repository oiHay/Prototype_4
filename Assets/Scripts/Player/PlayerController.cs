using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private Rigidbody rb;
    private GameObject focalPoint;
    private SmashBehavior smashBehavior;
    private PlayerCollision playerCollision;

    public static event Action PlayerOutOfBounder;
    public static GameObject FocalPoint { get; private set; }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        FocalPoint = focalPoint;
        smashBehavior = GetComponent<SmashBehavior>();
        playerCollision = GetComponent<PlayerCollision>();
        smashBehavior.OnSmashComplete += HandleSmashCompleted;
    }

    private void OnDestroy()
    {
        smashBehavior.OnSmashComplete -= HandleSmashCompleted;
    }

    private void HandleSmashCompleted()
    {
        playerCollision.ForceEndPowerUp();
    }

    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float sidewaysInput = Input.GetAxis("Horizontal");
        
        rb.AddForce(focalPoint.transform.forward * (moveSpeed * forwardInput));
        rb.AddForce(focalPoint.transform.right * (moveSpeed * sidewaysInput));

        HandleOutOfBounder();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            smashBehavior.TrySmash();
        }
    }
    
    private void HandleOutOfBounder()
    {
        if (transform.position.y < -8)
        {
            transform.position = new Vector3(0, 0.5f, 0);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            PlayerOutOfBounder?.Invoke();
        }
    }
}
