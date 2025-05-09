using UnityEngine;
using UnityEngine.Serialization;

public class RoomGenerator : MonoBehaviour
{
    
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private float hSpace;
    [SerializeField] private float vSpace;
    [SerializeField] private int totalOfRooms;
    [SerializeField] private int amountOfRoomsPerRow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RestartGeneration()
    {
        StopGeneration();
        for (int i = 0; i < totalOfRooms; i++)
        {
            GameObject r = Instantiate(roomPrefab, transform);
            r.transform.position = new Vector3(i % amountOfRoomsPerRow * hSpace, -(i / amountOfRoomsPerRow * vSpace), 0);
            r.name = "Generated Room " + i.ToString();
        }        
    }

    public void StopGeneration()
    {
        int count = transform.childCount - 1;
        for (int i = 0; i <= count; i++)
        {
            DestroyImmediate(transform.GetChild(count - i).gameObject);
        }
    }
}
