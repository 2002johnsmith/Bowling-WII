using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float launchForce = 500f;

    private Rigidbody rb;
    private InputSystem_Actions controls;
    private Vector2 moveInput;
    private bool launched = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls= new InputSystem_Actions();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Attack.performed += ctx => LaunchBall();
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    private void Update()
    {
        if (!launched)
        {
            Vector3 move = new Vector3(moveInput.x, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);  
        }
    }
    void LaunchBall()
    {
        if (!launched)
        {
            launched = true;
            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
        }
    }
}
