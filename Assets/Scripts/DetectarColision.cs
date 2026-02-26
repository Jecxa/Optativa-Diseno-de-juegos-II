using UnityEngine;

public class DetectarColision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("¡Ítem recogido! Colisión detectada.");
        }
    }
}