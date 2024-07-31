using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager controller;

    public AudioClip[] BgMusics;
    public AudioClip PanelSoundAudioClip;
    public AudioClip clickSoundAudioClip;

    public AudioSource backgroundSource;
    public AudioSource clickSource;
    public AudioSource completionSource;
    public AudioSource failSource;
    public AudioSource StarSource;
    public AudioSource PickSource;
    public AudioSource DropSource;

    void Start()
    {
        controller = this;
        backgroundSource.clip = BgMusics[Random.Range(0, 2)];
        backgroundSource.gameObject.SetActive(true);
        music(GlobalValues.isMusic == 0 ? true : false);
        sound(GlobalValues.isSound == 0 ? true : false);
    }
    public void music(bool value) => backgroundSource.mute = value;
    public void sound(bool value)
    {
        clickSource.mute = value;
        completionSource.mute = value;
        StarSource.mute = value;
        PickSource.mute = value;
        DropSource.mute = value;
    }
    public void click()
    {
        clickSource.clip = clickSoundAudioClip;
        clickSource.Play();
    }
    public void panelSound()
    {
        clickSource.volume = 0.7f;
        clickSource.clip = PanelSoundAudioClip;
        clickSource.Play();
    }
    public void completionSound() => completionSource.Play();
    public void LevelFailed() => failSource.Play();
    public void StarSound() => StarSource.Play();
    public void PickSound() => PickSource.Play();
    public void DropSound() => DropSource.Play();
}