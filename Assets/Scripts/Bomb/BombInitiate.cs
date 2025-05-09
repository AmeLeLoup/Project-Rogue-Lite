using UnityEngine;

public class BombInitiate : MonoBehaviour
{
    [SerializeField] private float cooldownDropBomb = 1.5f;
    private float timerSetBomb = 0f;


    [SerializeField] private GameObject bomb;

    public void DropBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }

    void Update()
    {
        timerSetBomb -= Time.deltaTime;
        if (timerSetBomb <= 0)
        {
            DropBomb();
            timerSetBomb = cooldownDropBomb;
        }
    }
}