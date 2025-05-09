using System;
using System.Collections;
using ProceduralLevelGenerator.Unity.Examples.Common;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public bool _cooldown = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Player") && _cooldown)
       {
           var player = other.GetComponent<PlayerHealth>();
           player.Damage(1);
           _cooldown = false;
           StartCoroutine("CoolDownAttack");
       }
    }

    private IEnumerator CoolDownAttack()
    {
        yield return new WaitForSeconds(1);
        _cooldown = true;
    }
}
