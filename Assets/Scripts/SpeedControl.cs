using UnityEngine;

public class SpeedControl : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement playerMovement;   
    public CameraScroller cameraScroller;    

    [Header("Scroll Speeds")]
    public float playerScrollSpeed;        
    public float cameraScrollSpeed;    

    void Update()
    {
        if (playerMovement != null)
            playerMovement.scrollSpeed = playerScrollSpeed;

        if (cameraScroller != null)
            cameraScroller.scrollSpeed = cameraScrollSpeed;
    }
}
