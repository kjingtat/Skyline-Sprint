using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameObject gameOverUI;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioSource != null)
                audioSource.Play();

            Time.timeScale = 0f;

            if (gameOverUI != null)
                gameOverUI.SetActive(true);
        }
    }
}
