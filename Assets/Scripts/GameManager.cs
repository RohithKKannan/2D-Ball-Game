using UnityEngine;
public enum BallType
{
    BlueBall, RedBall
}
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController[] players;
    int length;
    BallType ballType;
    void Start()
    {
        length = players.Length;
        ballType = BallType.BlueBall;
        PlayerSelect();
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
            if (item.ballType == BallType.BlueBall)
            {
                item.enabled = true;
                item.LightEnable();
            }
        }
    }
    [ContextMenu("Switch Player")]
    public void SwitchPlayer()
    {
        if (ballType == BallType.BlueBall)
            ballType = BallType.RedBall;
        else
            ballType = BallType.BlueBall;
        PlayerSelect();
    }
}
