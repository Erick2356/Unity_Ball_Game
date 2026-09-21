using UnityEngine;

public class Trampa : MonoBehaviour
{
    public Transform spawnPoint; // arrastra aquí el punto de inicio del laberinto
    public int puntos = -20; //lE resta puntos al
    public AudioClip sonidoRecolectar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);

            Destroy(gameObject);

            // Resetea velocidad y posición angular para que no siga con el impulso previo
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            // Teletransporta al punto de inicio
            other.transform.position = spawnPoint.position;
        }
    }
}