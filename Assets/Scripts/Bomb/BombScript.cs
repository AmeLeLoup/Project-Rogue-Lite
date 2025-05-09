using System;
using Unity.VisualScripting;
using UnityEngine;

public class BombScript : MonoBehaviour
{
   // private Coroutine _coroutine;
    //private Collider2D _collider;
    [SerializeField] private Animator _animatorBomb;
    public bool IsActiveted=false;
    public bool IsDead=false;
    //private GameObject _player;
    [SerializeField] private CircleCollider2D _explotionCollider;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_animatorBomb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActiveted)
        {
            _animatorBomb.SetBool("Explode", true);
        }
    }

    private void ActiveExplode()
    {
        _explotionCollider.enabled = true;
    }
    
    private void OnExplosion()
    {
        //_player.life-=1;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in area");
            IsActiveted = true;
            
            //other.gameObject.GetComponent<PlayerHealth>().Die();
            //_player = other.gameObject.GetComponent<GameObject>();
        }
        else if(other.CompareTag("Enemy"))
        {
            IsActiveted = false;
            //Find methode
            
        }
    }
}
