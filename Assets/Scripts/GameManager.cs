using UnityEngine;
public enum BallType
{
    BlueBall, RedBall
}
public enum GameOverConditions
{
    BlueInLava, RedInWater, FellIntoAcid
}
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController[] players;
    [SerializeField] CanvasController canvasController;
    bool BlueFinished = false;
    bool RedFinished = false;
    BallType ballType;
    int coins;
    void Start()
    {
        ballType = BallType.BlueBall;
        PlayerSelect();
        coins = 0;
        SetCoinCount();
    }
    void DisableAllPlayers()
    {
        foreach (var item in players)
        {
            item.LightDisable();
            item.enabled = false;
        }
    }
    void PlayerSelect()
    {
        DisableAllPlayers();
        foreach (var item in players)
        {
            if (item.ballType == ballType)
            {
                if (item.enabled == false)
                {
                    item.enabled = true;
                    item.LightEnable();
                }
            }
        }
    }
    public void PickedCoinUp()
    {
        AudioManager.Instance.PlaySound(SoundType.Coin);
        coins++;
        SetCoinCount();
    }
    void SetCoinCount()
    {
        canvasController.SetCoinCount(coins);
    }
    public void PlayerFinished(BallType ballType)
    {
        if (ballType == BallType.BlueBall)
        {
            BlueFinished = true;
        }
        if (ballType == BallType.RedBall)
        {
            RedFinished = true;
        }
        if (BlueFinished && RedFinished)
        {
            Debug.Log("Level Complete!");
            FinishLevel();
            AudioManager.Instance.PlaySound(SoundType.FinishLevel);
        }
        else
            SwitchPlayer();
    }
    [ContextMenu("Switch Player")]
    void SwitchPlayer()
    {
        if (ballType == BallType.BlueBall)
            ballType = BallType.RedBall;
        else
            ballType = BallType.BlueBall;
        PlayerSelect();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchPlayer();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        canvasController.EnablePause();
        AudioManager.Instance.PauseMusic();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        canvasController.DisablePause();
        AudioManager.Instance.ResumeMusic();
    }
    public void GameOver(GameOverConditions gameOverConditions)
    {
        string gameOverReason = GetReason(gameOverConditions);
        Time.timeScale = 0;
        canvasController.GameOverEnable(gameOverReason);
        AudioManager.Instance.PlaySound(SoundType.GameOver);
    }
    public void FinishLevel()
    {
        Time.timeScale = 0;
        canvasController.FinishLevelEnable();
        AudioManager.Instance.PlaySound(SoundType.FinishLevel);
    }
    string GetReason(GameOverConditions gameOverConditions)
    {
        string reason = "";
        switch (gameOverConditions)
        {
            case GameOverConditions.BlueInLava: reason = "Blue ball dies in lava!"; break;
            case GameOverConditions.RedInWater: reason = "Red ball dies in water!"; break;
            case GameOverConditions.FellIntoAcid: reason = "Do not fall into acid!"; break;
        }
        return reason;
    }
}
