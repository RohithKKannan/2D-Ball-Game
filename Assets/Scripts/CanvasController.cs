using UnityEngine;
using TMPro;
public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject PauseGamePanel;
    [SerializeField] GameObject FinishLevelPanel;
    [SerializeField] TMP_Text gameOverReason;
    public void EnablePause()
    {
        PauseGamePanel.SetActive(true);
    }
    public void DisablePause()
    {
        if (PauseGamePanel.activeInHierarchy)
            PauseGamePanel.SetActive(false);
    }
    public void GameOverEnable(string reason)
    {
        gameOverReason.text = reason;
        GameOverPanel.SetActive(true);
    }
    public void FinishLevelEnable()
    {
        FinishLevelPanel.SetActive(true);
    }
}
