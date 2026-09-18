using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform cameraTransform; // arrastra la Main Camera aquí

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
    }

    void RecalculateAxes()
    {
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        forwardAxis = camForward.normalized;

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        rightAxis = camRight.normalized;
    }

    void FixedUpdate()
    {
        RecalculateAxes(); // la cámara orbita, así que recalculamos cada frame físico

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
    }

    void OnDisable()
    {
        controls.Disable();
    }
}