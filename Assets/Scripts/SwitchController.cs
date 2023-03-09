using UnityEngine;
using UnityEngine.Rendering.Universal;
public class SwitchController : MonoBehaviour
{
    [SerializeField] Light2D lightComponent;
    [SerializeField] Animator movingPlatform;
    bool switchOn = false;
    void Start()
    {
        lightComponent.color = Color.white;
        switchOn = false;
    }
    public void TriggerSwitch(BallType ballType)
    {
        if (!switchOn)
        {
            if (ballType == BallType.BlueBall)
            {
                lightComponent.color = Color.blue;
                movingPlatform.SetTrigger("Move");
            }
            else if (ballType == BallType.RedBall)
            {
                lightComponent.color = Color.red;
                movingPlatform.SetTrigger("Move");
            }
            switchOn = !switchOn;
        }
    }
}
