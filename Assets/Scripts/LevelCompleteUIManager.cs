using UnityEngine;

public class LevelCompleteUIManager : MonoBehaviour
{
    public GameObject panel;  
    public Transform player;
    public Transform mainCamera;

    [Header("Level Complete Triggers")]
    public LevelCompleteTrigger[] triggers;  

    private Vector3 playerStartPos = new Vector3(-6.34f, -1.44f, -1f);
    private Vector3 cameraStartPos = new Vector3(-5.95f, 0f, -10f);

    public void ShowPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;

        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
                pm.controlsEnabled = false;
        }
    }

    public void ContinueLevel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;

        if (player != null)
        {
            Vector3 nextLevelPos = new Vector3(
                CheckpointManager.playerCheckpoint.x,
                CheckpointManager.playerCheckpoint.y - 30f,
                CheckpointManager.playerCheckpoint.z
            );
            player.position = nextLevelPos;

            CheckpointManager.playerCheckpoint = nextLevelPos;

            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
                pm.controlsEnabled = true;
        }

        if (mainCamera != null)
        {
            Vector3 nextCameraPos = new Vector3(
                CheckpointManager.cameraCheckpoint.x,
                CheckpointManager.cameraCheckpoint.y - 30f,
                CheckpointManager.cameraCheckpoint.z
            );
            mainCamera.position = nextCameraPos;

            CheckpointManager.cameraCheckpoint = nextCameraPos;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        if (player != null)
            player.position = playerStartPos;

        if (mainCamera != null)
            mainCamera.position = cameraStartPos;

        foreach (var t in triggers)
        {
            if (t != null)
                t.ResetTrigger();
        }

        panel.SetActive(false);
    }
}
