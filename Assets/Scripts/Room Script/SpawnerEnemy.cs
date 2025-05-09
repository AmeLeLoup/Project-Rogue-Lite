using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnerEnemy : MonoBehaviour
{
    private EnemyManager _enemyManager;
    
    [SerializeField] private int numberEnemyMax = 1;
    [SerializeField] private float coolDownSpawn;
    [SerializeField] private List<Enemy> enemies;
    private float _timerSpawn = 0;
    private int _numberEnemies = 0;

    [Serializable]
    public struct Enemy
    {
        public GameObject enemyObject;

        public float weight;
    }

    public void SetEnemyManager(EnemyManager enemyManager)
    {
      _enemyManager = enemyManager;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyManager != null)
        {
            if (enemies is { Count: > 0 })
            {
                if (_timerSpawn >= coolDownSpawn && numberEnemyMax > _numberEnemies)
                {
                    var localEnemies = enemies.OrderByDescending(e => e.weight);


                    var allWeight = localEnemies.Sum(enemy => enemy.weight);

                    var enemyRandom = Random.Range(0, allWeight);

                    var weightForRand = 0f;

                    foreach (var enemy in localEnemies)
                    {
                        if (enemy.weight + weightForRand >= enemyRandom)
                        {
                            //Debug.Log(enemy.enemyObject.name);
                            _enemyManager.AddEnemy(Instantiate(enemy.enemyObject, transform.position, Quaternion.identity, _enemyManager.transform));
                            _numberEnemies++;
                            break;
                        }

                        weightForRand += enemy.weight;
                    }

                    _timerSpawn = 0;
                }

                _timerSpawn += Time.deltaTime;
            }
        }
    }
}