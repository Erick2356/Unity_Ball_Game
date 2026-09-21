using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    public int puntos = 10;
    public AudioClip sonidoRecolectar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduce el sonido en la posición del objeto antes de destruirlo
            AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);

            Destroy(gameObject);
        }
    }
}
