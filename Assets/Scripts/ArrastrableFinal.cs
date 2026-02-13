using UnityEngine;
using UnityEngine.InputSystem;

public class ArrastradorFinal : MonoBehaviour
{
    private Camera cam;
    private bool arrastrando = false;
    private Vector3 offset;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 mundoPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
            
            RaycastHit2D hit = Physics2D.Raycast(mundoPos, Vector2.zero);
            
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                arrastrando = true;
                offset = transform.position - mundoPos;
                Debug.Log("🟢 Arrastre iniciado - Offset: " + offset);
            }
        }

       
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            arrastrando = false;
            Debug.Log("🔴 Arrastre terminado");
        }

      
        if (arrastrando)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 mundoPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
            mundoPos.z = 0;
            
           
            transform.position = mundoPos + offset;
        }
    }
}