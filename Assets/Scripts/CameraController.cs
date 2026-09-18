using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Transform playerDirection; 
    public float height = 1.5f;
    public float distance = 3f;
    public float smoothSpeed = 8f;
    public LayerMask collisionMask;
    public float cameraRadius = 0.4f;
    public float minDistance = 0.5f;
    public float rotationSpeed = 50f;

    private InputSystem_Actions controls;
    private Vector2 lookInput;
    private float orbitAngle = 0f;

    void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void LateUpdate()
    {
        orbitAngle += lookInput.x * rotationSpeed * Time.deltaTime;

        Vector3 baseForward = playerDirection.forward;
        baseForward.y = 0f;
        baseForward.Normalize();

        Quaternion orbitRotation = Quaternion.AngleAxis(orbitAngle, Vector3.up);
        Vector3 forward = orbitRotation * baseForward;

        Vector3 pivotPoint = player.transform.position + Vector3.up * height;

        float finalDistance = minDistance;
        int steps = 12;

        for (int i = 0; i <= steps; i++)
        {
            float t = (float)i / steps;
            float testDistance = Mathf.Lerp(distance, minDistance, t);
            Vector3 testPos = pivotPoint - forward * testDistance;

            if (!Physics.CheckSphere(testPos, cameraRadius, collisionMask))
            {
                finalDistance = testDistance;
                break;
            }
        }

        Vector3 desiredPosition = pivotPoint - forward * finalDistance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(player.transform.position + Vector3.up * 0.5f);
    }
}