using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;      
    public Transform player;           
    public float nudgeThreshold = 1f;   
    public float nudgeSpeed = 0.5f;     

    void Update()
    {
        float moveSpeed = scrollSpeed;

        if (player.position.x < transform.position.x - nudgeThreshold)
        {
            moveSpeed += nudgeSpeed;
        }

        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
