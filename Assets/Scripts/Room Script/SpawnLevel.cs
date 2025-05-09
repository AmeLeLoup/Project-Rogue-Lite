using ProceduralLevelGenerator.Unity.Examples.Common;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = null;
        do
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = transform.position;
            }
        } while (player==null);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
