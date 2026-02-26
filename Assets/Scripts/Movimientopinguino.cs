using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoPinguino : MonoBehaviour
{
    public float velocidad = 5f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 movimiento;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Update()
    {
        
        movimiento.x = 0f;
        movimiento.y = 0f;

        if (Keyboard.current.rightArrowKey.isPressed) movimiento.x = 1f;
        if (Keyboard.current.leftArrowKey.isPressed) movimiento.x = -1f;
        if (Keyboard.current.upArrowKey.isPressed) movimiento.y = 1f;
        if (Keyboard.current.downArrowKey.isPressed) movimiento.y = -1f;

     
        movimiento = movimiento.normalized;

       
        if (animator != null)
        {
            bool estaMoviendose = (movimiento.x != 0 || movimiento.y != 0);
            animator.SetBool("Moviendose", estaMoviendose);
        }

      
        if (movimiento.x > 0) spriteRenderer.flipX = false;
        if (movimiento.x < 0) spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
      
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            transform.Translate(movimiento * velocidad * Time.deltaTime);
        }
        else
        {
            rb.linearVelocity = movimiento * velocidad;
        }
    }
}