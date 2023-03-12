using UnityEngine;
using System.Collections;
using TMPro;
public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject LevelSelection;
    [SerializeField] private TMP_Text musicText;
    [SerializeField] private TMP_Text soundText;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator titleAnimator;
    float musicVol;
    float effectsVol;
    void Start()
    {
        musicVol = AudioManager.Instance.GetMusicVolume();
        effectsVol = AudioManager.Instance.GetSfxVolume();
        musicVol *= 100;
        effectsVol *= 100;
        UpdateEffectsVolText();
        UpdateMusicVolText();
        StartCoroutine(TitleLoopAnim());
    }
    public void StartGame()
    {
        animator.SetTrigger("LevelSelectOn");
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void OpenOptions()
    {
        animator.SetTrigger("SettingsOn");
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void SettingsBackToMenu()
    {
        animator.SetTrigger("SettingsOff");
        AudioManager.Instance.PlaySound(SoundType.BackButton);
    }
    public void LevelSelectBackToMenu()
    {
        animator.SetTrigger("LevelSelectOff");
        AudioManager.Instance.PlaySound(SoundType.BackButton);
    }
    public void QuitGame()
    {
        LevelManager.Instance.QuitGame();
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void SetSfxVolume(float _volume)
    {
        AudioManager.Instance.SetSfxVolume(_volume);
    }
    public void SetMusicVolume(float _volume)
    {
        AudioManager.Instance.SetMusicVolume(_volume);
    }
    void UpdateMusicVolText()
    {
        musicText.text = musicVol.ToString();
    }
    void UpdateEffectsVolText()
    {
        soundText.text = effectsVol.ToString();
    }
    public void IncreaseMusic()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (musicVol < 100)
            musicVol++;
        Debug.Log(musicVol);
        Debug.Log(Mathf.Clamp(musicVol, 0f, 1f));
        SetMusicVolume(Mathf.Clamp(musicVol, 0f, 1f));
        UpdateMusicVolText();
    }
    public void DecreaseMusic()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (musicVol > 0)
            musicVol--;
        Debug.Log(musicVol);
        Debug.Log(Mathf.Clamp(musicVol, 0f, 1f));
        SetMusicVolume(Mathf.Clamp(musicVol, 0f, 1f));
        UpdateMusicVolText();
    }
    public void IncreaseEffects()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (effectsVol < 100)
            effectsVol++;
        SetSfxVolume(Mathf.Clamp(effectsVol, 0f, 1f));
        UpdateEffectsVolText();
    }
    public void DecreaseEffects()
    {
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
        if (effectsVol > 0)
            effectsVol--;
        SetSfxVolume(Mathf.Clamp(effectsVol, 0f, 1f));
        UpdateEffectsVolText();
    }
    IEnumerator TitleLoopAnim()
    {
        yield return new WaitForSeconds(2.5f);
        titleAnimator.SetTrigger("StartLoop");
    }
}
