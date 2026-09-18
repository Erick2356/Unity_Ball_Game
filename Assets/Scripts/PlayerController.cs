using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;


    public Vector3 mazeForwardDirection = new Vector3(-1, 0, 0);

    public AudioClip sonidoChoque;

    private InputSystem_Actions controls;
    private Rigidbody rb;
    private AudioSource audioSource;
    private Vector2 moveInput;

    private Vector3 forwardAxis;
    private Vector3 rightAxis;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        RecalculateAxes();
    }

    void RecalculateAxes()
    {
        forwardAxis = mazeForwardDirection.normalized;
        rightAxis = Vector3.Cross(Vector3.up, forwardAxis).normalized;
    }

    void FixedUpdate()
    {

        Vector3 movement = forwardAxis * moveInput.y + rightAxis * moveInput.x;
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            audioSource.PlayOneShot(sonidoChoque);
        }
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