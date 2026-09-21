using UnityEngine;

public class QuitaPlataformasFlotantes : MonoBehaviour
{
public AudioClip sonidoDesaparecer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlataformaFlot"))
        {
            if (sonidoDesaparecer != null)
            {
                AudioSource.PlayClipAtPoint(sonidoDesaparecer, other.transform.position);
            }
            Renderer[] renderers = other.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
            {
                rend.enabled = false;
            }
        }
    }
}