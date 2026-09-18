using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float height = 1.5f;
    public float distance = 3f;
    public float smoothSpeed = 8f;
    public LayerMask collisionMask;
    public float cameraRadius = 0.4f;
    public float minDistance = 0.5f;

    private PlayerController playerController;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        Vector3 forward = playerController.mazeForwardDirection.normalized;
        Vector3 pivotPoint = player.transform.position + Vector3.up * height;

        float finalDistance = minDistance;
        int steps = 12;

        // Prueba desde la distancia máxima hacia la mínima, y usa la primera posición libre de paredes
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