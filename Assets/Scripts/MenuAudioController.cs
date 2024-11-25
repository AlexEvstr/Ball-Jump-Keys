using UnityEngine;

public class MenuAudioController : MonoBehaviour
{
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    [SerializeField] private GameObject _musicOn;
    [SerializeField] private GameObject _musicOff;

    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private AudioClip _tapButtonSound;

    private void Start()
    {
        _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        if (_musicSource.volume == 1)
        {
            _musicOff.SetActive(false);
            _musicOn.SetActive(true);
        }
        else
        {
            _musicOn.SetActive(false);
            _musicOff.SetActive(true);
        }
        _soundSource.volume = PlayerPrefs.GetFloat("SoundsVolume", 1);
        if (_soundSource.volume == 1)
        {
            _soundOff.SetActive(false);
            _soundOn.SetActive(true);
        }
        else
        {
            _soundOn.SetActive(false);
            _soundOff.SetActive(true);
        }
    }

    public void DisableSounds()
    {
        _soundOn.SetActive(false);
        _soundOff.SetActive(true);
        _soundSource.volume = 0;
        PlayerPrefs.SetFloat("SoundsVolume", 0);
        TapButtonSound();
    }

    public void EnableSounds()
    {
        _soundOff.SetActive(false);
        _soundOn.SetActive(true);
        _soundSource.volume = 1;
        PlayerPrefs.SetFloat("SoundsVolume", 1);
        TapButtonSound();
    }

    public void DisableMusic()
    {
        _musicOn.SetActive(false);
        _musicOff.SetActive(true);
        _musicSource.volume = 0;
        PlayerPrefs.SetFloat("MusicVolume", 0);
        TapButtonSound();
    }

    public void EnableMusic()
    {
        _musicOff.SetActive(false);
        _musicOn.SetActive(true);
        _musicSource.volume = 1;
        PlayerPrefs.SetFloat("MusicVolume", 1);
        TapButtonSound();
    }

    public void TapButtonSound()
    {
        _soundSource.PlayOneShot(_tapButtonSound);
    }
}
