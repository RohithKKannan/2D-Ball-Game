using UnityEngine;
using UnityEngine.Rendering.Universal;
public class SwitchController : MonoBehaviour
{
    [SerializeField] Light2D lightComponent;
    [SerializeField] PlatformController movingPlatform;
    bool switchOn = false;
    void Start()
    {
        lightComponent.color = Color.white;
        switchOn = false;
    }
    public void TriggerSwitch(BallType ballType)
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (!switchOn)
        {
            if (ballType == BallType.BlueBall)
            {
                lightComponent.color = Color.blue;
                movingPlatform.MovePlatform();
            }
            else if (ballType == BallType.RedBall)
            {
                lightComponent.color = Color.red;
                movingPlatform.MovePlatform();
            }
            switchOn = !switchOn;
        }
    }
}
