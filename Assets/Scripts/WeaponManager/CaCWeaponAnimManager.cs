using UnityEngine;

public class CaCWeaponAnimManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public bool canAttack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            animator.SetBool("EnemyAttack", true);
        }
        else
        {
            animator.SetBool("EnemyAttack", false);
        }
        
    }
}