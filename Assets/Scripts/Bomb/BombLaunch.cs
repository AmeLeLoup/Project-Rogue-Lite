using UnityEngine;

namespace New_Folder.Bomb
{
    public class BombLaunch : MonoBehaviour
    {
        [SerializeField] private float cooldownDropBomb = 1f;
        [SerializeField] private Transform target;
        private float timerSetBomb = 0f;


        [SerializeField] private GameObject bomb;

        void Start()
        {
            var enemyManager=GetComponentInParent<EnemyManager>();
            target = enemyManager.Player;
        }
        public void LaunchBomb()
        {
            Instantiate(bomb, target.position, Quaternion.identity);
        }

        void Update()
        {
            timerSetBomb -= Time.deltaTime;
            if (timerSetBomb <= 0)
            {
                LaunchBomb();
                timerSetBomb = cooldownDropBomb;
            }
        }
    }
}