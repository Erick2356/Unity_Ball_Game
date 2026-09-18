using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    // Dirección hacia la que avanzas al presionar "arriba" (debe apuntar hacia adentro del laberinto)
    public Vector3 mazeForwardDirection = new Vector3(-1, 0, 0);

    private InputSystem_Actions controls;
    private Rigidbody rb;
    private Vector2 moveInput;

    private Vector3 forwardAxis;
    private Vector3 rightAxis;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RecalculateAxes();
    }

    void RecalculateAxes()
    {
        forwardAxis = mazeForwardDirection.normalized;
        rightAxis = Vector3.Cross(Vector3.up, forwardAxis).normalized;
    }

    void FixedUpdate()
    {
        // moveInput.y = arriba/abajo -> avanza/retrocede en la dirección del laberinto
        // moveInput.x = izquierda/derecha -> se mueve perpendicular (a los lados)
        Vector3 movement = forwardAxis * moveInput.y + rightAxis * moveInput.x;
        rb.AddForce(movement * speed);
    }

    void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Enable();
        RecalculateAxes();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}