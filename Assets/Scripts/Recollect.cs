using UnityEngine;

public class Recollect : MonoBehaviour
{
    public AudioClip sonidoRecolectar;
    private int puntajeTotal = 0;

    void OnTriggerEnter(Collider other)
    {
        Coleccionable item = other.GetComponent<Coleccionable>();

        if (item != null)
        {
            puntajeTotal += item.puntos;
            Debug.Log("Recogiste " + item.puntos + " puntos | Puntaje total: " + puntajeTotal);

            if (sonidoRecolectar != null)
                AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);

            Destroy(other.gameObject);
        }
    }
}