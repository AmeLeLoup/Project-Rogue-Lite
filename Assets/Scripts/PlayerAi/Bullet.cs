using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon Weapon;
    private Rigidbody2D rb;
    public float Speed = 3f;
    private Animator animator;
   

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemy to take damage
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.Damage(1);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}