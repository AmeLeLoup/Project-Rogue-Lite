using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 3f;

    public Rigidbody2D rigidbody;

   // public Animator animator;

    public SpriteRenderer spriteRenderer;

    private Vector2 movement;

    private Coroutine dashCoroutine;

    private bool _canDash = true;

    [SerializeField] private float dashDistance = 3f;

    [SerializeField] private float dashDuration = 0.2f;

    [SerializeField] private float dashCooldown = 2f;

    [SerializeField] private bool isInvincible = false;
    
    private bool dashPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        dashPressed=Input.GetKeyDown(KeyCode.Space);
       
        OnMove();
        if (dashPressed && _canDash)
        {
            Debug.Log("Space Pressed");
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            StartCoroutine(Dash(direction));
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        _canDash = false;
        isInvincible = true;

        float elapsed = 0f;
        Vector2 startPosition = rigidbody.position;
        Vector2 targetPosition = startPosition + direction * dashDistance;

        while (elapsed < dashDuration)
        {
            rigidbody.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsed / dashDuration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        rigidbody.MovePosition(targetPosition);

        isInvincible = false;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }

    void FixedUpdate()
    {
        rigidbody.linearVelocity = movement * speed;
    }

    void OnMove()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        
        //animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }
    }
}