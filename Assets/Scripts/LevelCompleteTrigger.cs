using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    public GameObject levelCompleteUI;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            levelCompleteUI.SetActive(true);
            triggered = true;
        }
    }

    public void ResetTrigger()
    {
        triggered = false;
    }
}
