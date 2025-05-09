using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [field: SerializeField] public float MaxHealthPlayer { get; set; }
    public float CurrentHealthPlayer { get; set; }

    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealthPlayer = MaxHealthPlayer;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
    }
    public void Damage(float damage)
    {
        CurrentHealthPlayer -= damage;
        animator.SetBool("Damage", true);
        if (CurrentHealthPlayer <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Destroy(player.gameObject);
        playerMovement.enabled = false;
        animator.SetBool("Dead", true);
    }
    
}
