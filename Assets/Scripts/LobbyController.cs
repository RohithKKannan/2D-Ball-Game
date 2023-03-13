using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LobbyController : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Options;
    [SerializeField] GameObject LevelSelection;
    [SerializeField] TMP_Text musicText;
    [SerializeField] TMP_Text soundText;
    float musicVol;
    float effectsVol;
    void Start()
    {
        musicVol = AudioManager.Instance.GetMusicVolume();
        effectsVol = AudioManager.Instance.GetSfxVolume();
        Debug.Log(musicVol * 100);
        Debug.Log(effectsVol * 100);
        musicVol *= 100;
        effectsVol *= 100;
        UpdateEffectsVolText();
        UpdateMusicVolText();
    }
    public void StartGame()
    {
        MainMenu.SetActive(false);
        LevelSelection.SetActive(true);
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        Options.SetActive(true);
        AudioManager.Instance.PlaySound(SoundType.ButtonClick);
    }
    public void BackToMenu()
    {
        AudioManager.Instance.PlaySound(SoundType.BackButton);
        Options.SetActive(false);
        LevelSelection.SetActive(false);
        MainMenu.SetActive(true);
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
}
