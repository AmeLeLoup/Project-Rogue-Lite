using UnityEngine;

public class MinimapLimitCam : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -1);
    }
  
}
