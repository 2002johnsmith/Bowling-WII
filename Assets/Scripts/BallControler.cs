using UnityEngine;

public class BallControler : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float launchForce = 80f;
    private Rigidbody rb;
    private InputSystem_Actions controls;
    private Vector2 moveInput;
    private bool launched = false;

    [Header("Respawn")]
    [SerializeField] private Transform ballSpawn; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new InputSystem_Actions();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Fire.performed += ctx => LaunchBall();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void FixedUpdate()
    {
        if (!launched && rb != null)
        {
            Vector3 targetVelocity = new Vector3(moveInput.x * moveSpeed * Time.deltaTime, 0, 0);
            Vector3 velocityChange = targetVelocity - rb.linearVelocity;
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    void LaunchBall()
    {
        if (!launched)
        {
            launched = true;
            rb.isKinematic = false;
            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
        }
    }

    public void ResetMovimiento()
    {
        launched = false;
        moveInput = Vector2.zero;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (ballSpawn != null)
        {
            rb.position = ballSpawn.position;
            rb.rotation = ballSpawn.rotation;
        }

        rb.isKinematic = false; 
    }
}
