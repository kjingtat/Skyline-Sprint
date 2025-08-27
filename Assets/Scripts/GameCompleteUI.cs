using UnityEngine;

public class GameCompleteUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;         
    public Transform player;
    public Transform mainCamera;

    [Header("Original Start Points")]
    public Vector3 originalPlayerStart = new Vector3(-6.34f, -1.44f, -1f);
    public Vector3 originalCameraStart = new Vector3(-5.95f, 0f, -10f);

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowPanel();
        }
    }

    public void ShowPanel()
    {
        if (panel != null)
            panel.SetActive(true);

        Time.timeScale = 0f;

        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
                pm.controlsEnabled = false;
        }
    }

    public void ContinueGame()
    {
        if (panel != null)
            panel.SetActive(false);

        Time.timeScale = 1f;

        CheckpointManager.playerCheckpoint = originalPlayerStart;

        if (player != null)
        {
            player.position = originalPlayerStart;

            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
                pm.controlsEnabled = true;
        }

        if (mainCamera != null)
        {
            Vector3 cameraOffset = originalCameraStart - originalPlayerStart;
            mainCamera.position = player.position + cameraOffset;

            CheckpointManager.cameraCheckpoint = mainCamera.position;
        }
    }

}
