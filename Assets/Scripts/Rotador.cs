using UnityEngine;

public class Rotador : MonoBehaviour
{
    public float velocidad = 60f;

    void Update()
    {
        transform.Rotate(new Vector3(0, velocidad, 0) * Time.deltaTime);
    }
}
