using UnityEngine;

public class QuitaPlataformasFlotantes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlataformaFlot"))
        {
            // Desactiva todos los renderers (incluye hijos, por si la plataforma
            // tiene varias partes o materiales separados)
            Renderer[] renderers = other.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers)
            {
                rend.enabled = false;
            }
        }
    }
}