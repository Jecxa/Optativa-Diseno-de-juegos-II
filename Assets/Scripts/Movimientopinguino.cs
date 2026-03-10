using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPinguino : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 15f;

    [Header("Detección de Suelo")]
    public Transform puntoDeteccionSuelo;
    public float radioDeteccion = 0.2f;
    public LayerMask capaSuelo;


    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
  
    private float movimientoHorizontal;
    private bool enSuelo;

    void Start()
    {
        
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
     
        if (animator == null) Debug.LogError("❌ No hay Animator en el pingüino");
        if (spriteRenderer == null) Debug.LogError("❌ No hay SpriteRenderer en el pingüino");
        if (rb == null) Debug.LogError("❌ No hay Rigidbody2D en el pingüino");
    }

    void Update()
    {
        
        movimientoHorizontal = 0f;
        
        if (Keyboard.current.rightArrowKey.isPressed) movimientoHorizontal = 1f;
        if (Keyboard.current.leftArrowKey.isPressed) movimientoHorizontal = -1f;

       
        bool estaCaminando = movimientoHorizontal != 0;
        animator.SetBool("Moviendose", estaCaminando);

       
        DetectarSuelo();

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            animator.SetTrigger("Atacar");
            Debug.Log("⚔️ ¡Ataque!");
        }

       
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("🔹 Tecla ESPACIO detectada");
            Debug.Log($"🔹 ¿Está en suelo? {enSuelo}");
            
            if (enSuelo)
            {
               
                animator.SetTrigger("Saltar");
                
               
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
                
                Debug.Log($"🔹 ¡Salto! Fuerza aplicada: {fuerzaSalto}");
            }
            else
            {
                Debug.Log("🔹 No puede saltar: no está en el suelo");
            }
        }

       
        if (movimientoHorizontal > 0) spriteRenderer.flipX = false; 
        if (movimientoHorizontal < 0) spriteRenderer.flipX = true;  
    }

    void FixedUpdate()
    {
       
        rb.linearVelocity = new Vector2(movimientoHorizontal * velocidad, rb.linearVelocity.y);
    }

    void DetectarSuelo()
    {
        if (puntoDeteccionSuelo == null)
        {
            Debug.LogWarning("⚠️ PuntoDeteccionSuelo no asignado");
            return;
        }
        
        enSuelo = Physics2D.OverlapCircle(puntoDeteccionSuelo.position, radioDeteccion, capaSuelo);
    }

  
    void OnDrawGizmosSelected()
    {
        if (puntoDeteccionSuelo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoDeteccionSuelo.position, radioDeteccion);
        }
    }
}