using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Vector3 _mousePos;
    private Vector3 rotation;
    private Player_Movement playerMovement;
    private float rotZ;
    [SerializeField] private Camera _mainCam;

    private void Aim()
    {
        //Aiming
        // if (playerInput.currentControlScheme == "Keyboard&Mouse")
        // {
        //     _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        //     rotation = (_mousePos - transform.position).normalized;
        // }
        // else if (playerInput.currentControlScheme == "Gamepad")
        // {
        //     rotation = characterInput.gamepadAimInput.normalized;
        // }

        //Switch aim side depending on the character scale
        if (playerMovement.transform.localScale.x > 0)
        {
            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }
        else
        {
            rotZ = Mathf.Atan2(-rotation.y, -rotation.x) * Mathf.Rad2Deg;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
