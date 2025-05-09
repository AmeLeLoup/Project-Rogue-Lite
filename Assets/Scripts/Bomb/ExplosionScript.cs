using System;
using ProceduralLevelGenerator.Unity.Examples.Common;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private bool _isdamage = false;
    private Player_Movement _player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isdamage)
        {
            _player = other.gameObject.GetComponent<Player_Movement>();
            if (_player != null)
            {
                if(!_player.isInvincible)
                {
                    var lifePlayer = other.GetComponent<PlayerHealth>();
                    if (lifePlayer != null)
                    {
                        Debug.Log("Explosion");
                        _isdamage = true;
                        lifePlayer.Damage(1);
                
                    }
                }
            }
           
        }
            
    }
}
