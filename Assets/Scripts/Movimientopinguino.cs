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

    // Componentes
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    // Estados
    private float movimientoHorizontal;
    private bool enSuelo;

    void Start()
    {
        // Obtener referencias a los componentes
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        // Verificaciones
        if (animator == null) Debug.LogError("❌ No hay Animator en el pingüino");
        if (spriteRenderer == null) Debug.LogError("❌ No hay SpriteRenderer en el pingüino");
        if (rb == null) Debug.LogError("❌ No hay Rigidbody2D en el pingüino");
    }

    void Update()
    {
        // --- MOVIMIENTO HORIZONTAL (FLECHAS IZQUIERDA/DERECHA) ---
        movimientoHorizontal = 0f;
        
        if (Keyboard.current.rightArrowKey.isPressed) movimientoHorizontal = 1f;
        if (Keyboard.current.leftArrowKey.isPressed) movimientoHorizontal = -1f;

        // --- ANIMACIÓN DE CAMINAR ---
        bool estaCaminando = movimientoHorizontal != 0;
        animator.SetBool("Moviendose", estaCaminando);

        // --- DETECCIÓN DE SUELO ---
        DetectarSuelo();

        // --- ATAQUE (TECLA F) ---
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            animator.SetTrigger("Atacar");
            Debug.Log("⚔️ ¡Ataque!");
        }

        // --- SALTO (TECLA ESPACIO) - CORREGIDO ---
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("🔹 Tecla ESPACIO detectada");
            Debug.Log($"🔹 ¿Está en suelo? {enSuelo}");
            
            if (enSuelo)
            {
                // ACTIVAR EL TRIGGER DE SALTO (DEBE COINCIDIR CON EL ANIMATOR)
                animator.SetTrigger("Saltar");
                
                // APLICAR FUERZA DE SALTO
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
                
                Debug.Log($"🔹 ¡Salto! Fuerza aplicada: {fuerzaSalto}");
            }
            else
            {
                Debug.Log("🔹 No puede saltar: no está en el suelo");
            }
        }

        // --- VOLTEAR SPRITE SEGÚN DIRECCIÓN ---
        if (movimientoHorizontal > 0) spriteRenderer.flipX = false; // Derecha
        if (movimientoHorizontal < 0) spriteRenderer.flipX = true;  // Izquierda
    }

    void FixedUpdate()
    {
        // Aplicar movimiento horizontal (respetando la gravedad)
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

    // Visualizar el círculo de detección en el Editor
    void OnDrawGizmosSelected()
    {
        if (puntoDeteccionSuelo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoDeteccionSuelo.position, radioDeteccion);
        }
    }
}