using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemies= new List<GameObject>();
    [SerializeField] private Transform player;
    [SerializeField] private Player_Movement player_generate;
    public Transform Player => player;
    private bool isGenerated = false;

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && isGenerated)
        {
            isGenerated = false;
        }
            
        if (enemies.Count > 0)
        {
            enemies.RemoveAll(item => item == null);
        }
        else if (enemies.Count == 0 && !isGenerated)
        {
            player_generate.Generate();
            isGenerated = true;
        }
        
    }
}
