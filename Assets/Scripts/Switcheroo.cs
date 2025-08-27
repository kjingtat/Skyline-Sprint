using UnityEngine;

public class Switcheroo : MonoBehaviour
{
    [Header("Objects to Switch")]
    public GameObject objectToHide;  
    public GameObject objectToShow; 
    public GameObject textObject;   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (objectToHide != null)
                objectToHide.SetActive(false);

            if (objectToShow != null)
                objectToShow.SetActive(true);

            if (textObject != null)
                textObject.SetActive(true);
        }
    }
}
