using UnityEngine;

namespace New_Folder.Bomb
{
    public class ShootInititate : MonoBehaviour
    {
        [SerializeField] private float cooldownDropBomb = 1.5f;
        private float _timerSetBomb = 0f;


        [SerializeField] private BombScript bomb;

        public void DropBomb()
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
        }

        void Update()
        {
            _timerSetBomb -= Time.deltaTime;
            if (_timerSetBomb <= 0)
            {
                DropBomb();
                _timerSetBomb = cooldownDropBomb;
            }
        }
    }
}