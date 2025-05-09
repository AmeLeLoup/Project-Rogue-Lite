using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [field: SerializeField] public float MaxHealthPlayer { get; set; }
    public float CurrentHealthPlayer { get; set; }

    [SerializeField] private GameObject player;
    [SerializeField ]private Player_Movement playerMovement;
    [SerializeField ]private GameObject panel;
    [SerializeField ]private GameObject loading;
    [SerializeField ]private GameObject gameOver;
    [SerializeField]private TextMeshProUGUI lifeText;
    private Animator animator;
    private float currentlife=0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealthPlayer = MaxHealthPlayer;
         
    }

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
        if (CurrentHealthPlayer != currentlife)
        {
            lifeText.text = CurrentHealthPlayer.ToString();
            currentlife = CurrentHealthPlayer;
        }
        
    }
    public void Damage(float damage)
    {
        CurrentHealthPlayer -= damage;
        //animator.SetBool("Damage", true);
        if (CurrentHealthPlayer <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Destroy(player.gameObject);
        playerMovement.enabled = false;
        //animator.SetBool("Dead", true);
        panel.SetActive(true);
        loading.SetActive(false);
        gameOver.SetActive(true);
    }
    
}
