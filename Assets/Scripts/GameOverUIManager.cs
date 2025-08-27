using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    public GameObject panel;
    public Transform player;
    public Transform mainCamera;

    [Header("Level Complete Triggers")]
    public LevelCompleteTrigger[] levelCompleteTriggers;

    public void ShowGameOver()
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

    public void RestartGame()
    {
        Time.timeScale = 1f;

        if (player != null)
        {
            player.position = CheckpointManager.playerCheckpoint;

            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
                pm.controlsEnabled = true;
        }

        if (mainCamera != null)
            mainCamera.position = CheckpointManager.cameraCheckpoint;

        foreach (var t in levelCompleteTriggers)
        {
            if (t != null)
                t.ResetTrigger();
        }

        panel.SetActive(false);
    }
}
